using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    /// <summary>
    /// Token  manipulation class
    /// Contains all nessesary methods for ganerating tokens and retriving data from them
    /// </summary>
    public class TokenManipulation : ITokenManipulation
    {
        private readonly JWTSettings _jwtsettings;

        public TokenManipulation(IOptions<JWTSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }

        /// <summary>
        /// Generates access token for the user
        /// </summary>
        /// <param name="userId">int user id</param>
        /// <returns>string access token</returns>
        public string GenerateAccessToken(int userId)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Convert.ToString(userId))
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates verification token for email verification
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>Verification token model</returns>
        public VerificationToken GenerateVerificationToken(int userId)
        {
            VerificationToken token = new VerificationToken();
            Random random = new Random();
            token.Token = random.Next(0, 100000).ToString("D5");
            token.CreatedAt = DateTime.UtcNow;
            token.UserId = userId;

            return token;
        }


        // TODO -- do I need this
        /// <summary>
        /// Retreives user id from access token
        /// </summary>
        /// <param name="accessToken">string access token</param>
        /// <returns>int user id</returns>
        public int GetUserIdFromAccessToken(string accessToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principle = tokenHandler.ValidateToken(
                accessToken, tokenValidationParameters, out SecurityToken securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                string userId = principle.FindFirst(ClaimTypes.Name)?.Value;

                return Convert.ToInt32(userId);
            }

            // TODO -- Think about this solution
            throw new ApplicationException("Token is invalid");
        }
    }
}
