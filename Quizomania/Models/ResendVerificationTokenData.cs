using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Request model when verification token needs to be resent
    /// </summary>
    public class ResendVerificationTokenData
    {
        /// <summary>
        /// Email address of a user
        /// </summary>
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
