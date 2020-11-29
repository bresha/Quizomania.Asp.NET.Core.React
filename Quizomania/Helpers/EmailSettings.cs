using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    /// <summary>
    /// Mail settings class
    /// It is a hellper class to recive mail settings
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Email from which emails is going to be sent
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Display name in the email
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Password of account
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// SMTP host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port number
        /// </summary>
        public int Port { get; set; }
    }
}
