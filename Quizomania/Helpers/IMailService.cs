using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.Helpers
{
    /// <summary>
    /// Mail service
    /// Contains methods to send email
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Sends verification token via email to user
        /// </summary>
        /// <param name="token">string verification token</param>
        /// <param name="user">User object</param>
        /// <returns>Task</returns>
        Task SendVerificationTokenAsync(string token, User user);
    }
}
