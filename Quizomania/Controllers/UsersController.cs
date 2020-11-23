using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using Quizomania.DataAccess;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizomania.Helpers;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Quizomania.Enums;
using Microsoft.Extensions.Logging;

namespace Quizomania.Controllers
{
    /// <summary>
    /// Main users controler class
    /// Contains all api endpoints for users
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersDataAccess _usersDataAccess;
        private readonly ITokenManipulation _tokenManipulation;
        private readonly IMailService _mailService;
        private readonly IVerificationTokenDataAccess _verificationTokenDataAccess;
        private readonly ILogger _logger;

        public UsersController(
            IUsersDataAccess usersDataAccess, 
            ITokenManipulation tokenManipulation,
            IMailService mailService,
            IVerificationTokenDataAccess verificationTokenDataAccess,
            ILogger<UsersController> logger)
        {
            _usersDataAccess = usersDataAccess;
            _tokenManipulation = tokenManipulation;
            _mailService = mailService;
            _verificationTokenDataAccess = verificationTokenDataAccess;
            _logger = logger;
        }

      
        /// <summary>
        /// Registration of a new member.
        /// </summary>
        /// <param name="registerUserData">RegisterUserData from client application</param>
        /// <returns>IActionResult</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserData registerUserData)
        {       
            try
            {
                //Encrypt user password
                registerUserData.Password = BC.HashPassword(registerUserData.Password);

                //Check is email in use
                User user = await _usersDataAccess.GetUserByEmailAsync(registerUserData.Email);
                if (user != null)
                {
                    ModelState.AddModelError("Email", "Email address is taken");
                }

                //Check is username in use
                user = await _usersDataAccess.GetUserByUserNameAsync(registerUserData.Username);
                if (user != null)
                {
                    ModelState.AddModelError("Username", "Username is taken");
                }

                //If there are errors send them to client app
                if (ModelState.IsValid == false)
                {
                    return Conflict(ModelState);
                }
                
                //Create a user from register user data
                user = new User(registerUserData);

                //Set user as unverified
                user.IsVerified = false;

                //Insert user to db
                await _usersDataAccess.CreateUserAsync(user);

                //Create verification token
                VerificationToken token = _tokenManipulation.GenerateVerificationToken(user.Id);

                //Set token purpose
                token.TokenPurpose = TokenPurposeEnum.EmailVerification;

                //Insert verification token in database
                await _verificationTokenDataAccess.InsertVerificationTokenAsync(token);

                //Send email with verification token
                await _mailService.SendVerificationTokenAsync(token.Token, user);

                return Ok(new { message = $"Thank you for registering! Verification token has been sent to {user.Email}" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Email verification after registration.
        /// </summary>
        /// <param name="verifyEmailData">Receives email and the verification token.</param>
        /// <returns>IActionResult</returns>
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailData verifyEmailData)
        {
            try
            {
                // Get user from email
                User user = await _usersDataAccess.GetUserByEmailAsync(verifyEmailData.Email);

                // Get verification token model from token that comes from the body and user id
                VerificationToken token = await _verificationTokenDataAccess
                    .GetVerificationTokenAsync(user.Id, verifyEmailData.VerificationToken, TokenPurposeEnum.EmailVerification);
                
                // If there is a token update user model and database for IsVerified
                if (token != null)
                {
                    user.IsVerified = true;

                    await _usersDataAccess.UpdateUserWhenVerifiedAsync(user);

                    //Add access token to user
                    user.AccessToken = _tokenManipulation.GenerateAccessToken(user.Id);

                    //Return user data
                    return Ok(new
                    {
                        user.Id,
                        user.Username,
                        user.AccessToken
                    });
                }
                else
                {
                    //Return bad request 
                    return BadRequest(new
                    {
                        errors = "Invalid verification token"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");

                return StatusCode(500);
            }
        }
        
        /// <summary>
        /// Users login to receive access token
        /// </summary>
        /// <param name="loginData">Email and password</param>
        /// <returns>IActionResult</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginData loginData)
        {
            try
            {
                //Get user from database by email
                User user = await _usersDataAccess.GetUserByEmailAsync(loginData.Email);

                //If no user return 401 "Invalid credentials"
                if (user == null)
                {
                    return Unauthorized(new { errors = "Invalid credentials" });
                }

                //Check if the passwords match
                bool verified = BC.Verify(loginData.Password, user.Password);

                //If paswords doesnt match return 401 "Invalid credentials"
                if (verified == false)
                {
                    return Unauthorized(new { errors = "Invalid credentials" });
                }

                //If user is verified return user info
                if (user.IsVerified)
                {
                    //Add access token to user
                    user.AccessToken = _tokenManipulation.GenerateAccessToken(user.Id);

                    //If everything matches return user without email and password
                    return Ok(new
                    {
                        user.Id,
                        user.Username,
                        user.AccessToken
                    });
                }
                else
                {
                    return Conflict(new
                    {
                        errors = "User email is not verified"
                    });
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");

                return StatusCode(500);
            }
        }

        /// <summary>
        /// Resend token for email verification
        /// </summary>
        /// <param name="resendVerificationTokenData"></param>
        /// <returns></returns>
        [HttpPost("resend-verification-email")]
        public async Task<IActionResult> ResendVerificationToken([FromBody] ResendVerificationTokenData resendVerificationTokenData)
        {
            try
            {
                User user = await _usersDataAccess.GetUserByEmailAsync(resendVerificationTokenData.Email);

                if (user == null)
                {
                    return NotFound(new
                    {
                        errors = "No such user."
                    });
                }

                VerificationToken token =_tokenManipulation.GenerateVerificationToken(user.Id);
                token.TokenPurpose = TokenPurposeEnum.EmailVerification;

                await _verificationTokenDataAccess.InsertVerificationTokenAsync(token);

                await _mailService.SendVerificationTokenAsync(token.Token, user);

                return Ok(new
                {
                    message = $"New verification token has been sent to {user.Email}"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Server Error");

                return StatusCode(500);
            }
        }
    }
}
