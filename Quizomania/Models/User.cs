using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Main User model
    /// Contains all properties for User that is needed in a game
    /// </summary>
    public class User
    {
        public User()
        {

        }

        public User(RegisterUserData registerUserData)
        {
            this.Username = registerUserData.Username;
            this.Email = registerUserData.Email;
            this.Password = registerUserData.Password;
        }
        /// <summary>
        /// The unique identifier for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents user in application
        /// It will be visiable to other users
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email address of a user
        /// This is used for registration and login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// This is user's login pasword
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Access token for user when it is registered
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Id of tournament that user is registered in
        /// </summary>
        public int RegisteredInTournament { get; set; }

        /// <summary>
        /// Number of correct answers for particular tournament
        /// </summary>
        public int NumberOfCorrectAnswers { get; set; }

        /// <summary>
        /// Cumulative time that user gave for answers in milliseconds
        /// </summary>
        public int CumulativeTimeForAnswers { get; set; }
    }
}
