using Quizomania.Enums;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    /// <summary>
    /// Verification Token Data Access
    /// Contains all methods for inserting and retriving Verification Token from database
    /// </summary>
    public interface IVerificationTokenDataAccess
    {
        /// <summary>
        /// Insert verification token model into database 
        /// </summary>
        /// <param name="token">Verification token model</param>
        /// <returns>void</returns>
        Task InsertVerificationTokenAsync(VerificationToken token);

        /// <summary>
        /// Gets verification token model from database
        /// </summary>
        /// <param name="userId">int Users id</param>
        /// <param name="token">string token</param>
        /// <param name="tokenPurpose">Token purpose enum</param>
        /// <returns>Verification Token model</returns>
        Task<VerificationToken> GetVerificationTokenAsync(int userId, string token, TokenPurposeEnum tokenPurpose);
    }
}
