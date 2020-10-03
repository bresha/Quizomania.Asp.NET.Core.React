using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Verify email data.
    /// Contains data that will come from client when verifying email.
    /// </summary>
    public class VerifyEmailData
    {
        /// <summary>
        /// Email address of a user.
        /// This is used for registration and login.
        /// </summary>
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Verification token that has been sent to users email
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string VerificationToken { get; set; }
    }
}
