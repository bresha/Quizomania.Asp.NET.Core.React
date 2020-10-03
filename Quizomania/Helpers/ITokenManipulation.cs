using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    /// <summary>
    /// Token Manipulation Interface
    /// Contains names, return types and inputs for methods that needs to be implemented
    /// </summary>
    public interface ITokenManipulation
    {
        /// <summary>
        /// Implementation of this method should return access token
        /// </summary>
        /// <param name="userId">Unique identifier for user (from database)</param>
        /// <returns>String Access token</returns>
        string GenerateAccessToken(int userId);

        /// <summary>
        /// Implementation of this method should return User Id 
        /// </summary>
        /// <param name="accessToken">String Access token</param>
        /// <returns>int User id</returns>
        int GetUserIdFromAccessToken(string accessToken);

        /// <summary>
        /// Generates verification token for email verification
        /// </summary>
        /// <param name="userId">Id of a user</param>
        /// <returns>Verification token model</returns>
        VerificationToken GenerateVerificationToken(int userId);
    }
}
