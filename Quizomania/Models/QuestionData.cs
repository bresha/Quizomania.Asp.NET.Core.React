using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Main question model
    /// Contains all properties that is needed for a game
    /// </summary>
    public class QuestionData
    {
        /// <summary>
        /// Unique identifier of a question
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Question for players to answer
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// List of posible answers
        /// </summary>
        public List<string> Answers { get; set; }

        /// <summary>
        /// Correct answer for question
        /// </summary>
        public string CorrectAnswer { get; set; }
    }
}
