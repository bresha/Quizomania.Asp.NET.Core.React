using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizomania.DataAccess;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quizomania.Helpers;

namespace Quizomania.Controllers
{
    /// <summary>
    /// Main users controler class
    /// Contains all api endpoints for users
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersDataAccess _usersDataAccess;
        private readonly ITokenManipulation _tokenManipulation;
        private readonly IRefreshTokensDataAccess _refreshTokensDataAccess;

        public UsersController(
            IUsersDataAccess usersDataAccess, 
            ITokenManipulation tokenManipulation,
            IRefreshTokensDataAccess refreshTokensDataAccess)
        {
            _usersDataAccess = usersDataAccess;
            _tokenManipulation = tokenManipulation;
            _refreshTokensDataAccess = refreshTokensDataAccess;
        }

        /// <summary>
        /// Register user api
        /// </summary>
        /// <param name="user">User model from body of request</param>
        /// <returns>User object with Id, Access token and Refresh token</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserData registerUserData)
        {
            //Encrypt user password
            registerUserData.Password = BC.HashPassword(registerUserData.Password);
                        
            try
            {
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

                if (ModelState.IsValid == false)
                {
                    return Conflict(ModelState);
                }
                
                //Create a user from register user data
                user = new User(registerUserData);

                //Insert user to db
                await _usersDataAccess.CreateUserAsync(user);

                //Add Refresh token to user
                user.RefreshToken = _tokenManipulation.GenerateRefreshToken(user.Id);

                //Insert refresh token into db
                await _refreshTokensDataAccess.InsertRefreshTokenAsync(user.RefreshToken);

                //Add access token to user
                user.AccessToken = _tokenManipulation.GenerateAccessToken(user.Id);

                //Return user
                return Ok(new
                {
                    user.Id,
                    user.Username,
                    user.AccessToken,
                    user.RefreshToken
                });

            }
            catch (Exception)
            {
                // TODO -- inject logger and log this error message to file or db
                return BadRequest(new { errors = "Server Error" });
            }
        }
    }
}
