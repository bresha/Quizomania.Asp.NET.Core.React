using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    /// <summary>
    /// This is helper class for geting connection string
    /// It is added to service container as singleton service
    /// </summary>
    public class ConnectionString
    {
        /// <summary>
        /// Value of connection string
        /// </summary>
        public string Value { get; }
        public ConnectionString(string value)
        {
            Value = value;
        }
    }
}
