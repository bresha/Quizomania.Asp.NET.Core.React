using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    /// <summary>
    /// Refresh tokens data access
    /// Contains names, return types and inputs for methods that are inpelmented
    /// </summary>
    public interface IRefreshTokensDataAccess
    {
        /// <summary>
        /// Inserts refresh token into database
        /// </summary>
        /// <param name="refreshToken">Refresh token model</param>
        /// <returns>void</returns>
        Task InsertRefreshTokenAsync(RefreshToken refreshToken);
    }
}
