using Quizomania.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Verification token model
    /// </summary>
    public class VerificationToken
    {
        /// <summary>
        /// Id of a user that is using this token
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 5 digit verification token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Purpose of a token
        /// </summary>
        public TokenPurposeEnum TokenPurpose { get; set; }

        /// <summary>
        /// Time of token creation
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
