using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Login user data model
    /// Contains all data that will come from login form
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Email address of a user
        /// This is used for registration and login
        /// </summary>
        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// This is user's login pasword
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
