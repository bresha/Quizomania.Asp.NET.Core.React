using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Register user data model
    /// Contains data that will come from the register form
    /// </summary>
    public class RegisterUserData
    {
        /// <summary>
        /// Represents user in application
        /// It will be visiable to other users
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

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
