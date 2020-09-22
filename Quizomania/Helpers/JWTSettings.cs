using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    /// <summary>
    /// This is helper class for getting JWT secret key
    /// </summary>
    public class JWTSettings
    {
        /// <summary>
        /// Value of JWT secret key
        /// </summary>
        public string SecretKey { get; set; }
    }
}
