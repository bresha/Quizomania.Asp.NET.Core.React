using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Refresh token model
    /// Contains nessesary properties for refres tokens
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Token that is going to be used for refreshing
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Expiry date of a token
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Id of a user that is using this token
        /// </summary>
        public int UserId { get; set; }
    }
}
