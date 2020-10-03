using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    /// <summary>
    /// Users Data Access Interface
    /// Contains names, return types and inputs for methods that needs to be implemented
    /// </summary>
    public interface IUsersDataAccess
    {
        /// <summary>
        /// Implementation of this interface method should return user from database
        /// </summary>
        /// <param name="email">Email of a user</param>
        /// <returns>
        /// User from database
        /// </returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Implementation of this interface method should return user from database
        /// </summary>
        /// <param name="email">Username of a user</param>
        /// <returns>
        /// User from database
        /// </returns>
        Task<User> GetUserByUserNameAsync(string username);

        /// <summary>
        /// Implementation of this interface should insert user in database
        /// </summary>
        /// <param name="user">User model</param>
        Task CreateUserAsync(User user);

        /// <summary>
        /// Updates users entry in database for IsVerified 
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns>Void</returns>
        Task UpdateUserWhenVerifiedAsync(User user);
    }
}
