using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Models
{
    /// <summary>
    /// Main tournament model 
    /// Contains all properties for a tournament
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Unique identifier for tournament
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the tournament
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Number of questions for tournament
        /// </summary>
        public int NumberOfQuestions { get; set; }

        /// <summary>
        /// Tournament category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Type of questions
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Question difficulty
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// Tournament questions
        /// </summary>
        public List<QuestionData> Questions { get; set; }

        /// <summary>
        /// Time available for answering question
        /// </summary>
        public int SecondsPerQuestion { get; set; }

        /// <summary>
        /// Represents index of current question in a list of questions
        /// </summary>
        public int IndexOfCurrentQuestion { get; set; } = 0;

        /// <summary>
        /// Start time of a tournament
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time of a tournament
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Players registered in a tournament
        /// </summary>
        public List<User> Players { get; set; }

        /// <summary>
        /// Represents has the tournament finished
        /// </summary>
        public bool Finished { get; set; } = false;

        /// <summary>
        /// Represents is the tournament public or private
        /// </summary>
        public bool PublicTournament { get; set; }
    }
}
