using Dapper;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    /// <summary>
    /// Users Dapper(SQL) Data Access class
    /// Contains all methods that are needed for accessing database in application
    /// </summary>
    public class UsersDataAccess : IUsersDataAccess
    {
        private readonly ConnectionString _connectionString;

        public UsersDataAccess(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets user from the database
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>
        /// User from database
        /// </returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            User output;

            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();
                p.Add("@Email", email);

                output = await connection
                    .QueryFirstOrDefaultAsync<User>("dbo.spUsers_GetByEmail", p, commandType: CommandType.StoredProcedure);
            }

            return output;
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            User output;

            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();
                p.Add("@Username", username);

                output = await connection
                    .QueryFirstOrDefaultAsync<User>("dbo.spUsers_GetByUsername", p, commandType: CommandType.StoredProcedure);
            }

            return output;
        }

        /// <summary>
        /// Inserts user to database
        /// </summary>
        /// <param name="user">User model</param>
        public async Task CreateUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();
                p.Add("@UserName", user.Username);
                p.Add("@Email", user.Email);
                p.Add("@Password", user.Password);
                p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection
                    .ExecuteAsync("dbo.spUsers_Insert", p, commandType: CommandType.StoredProcedure);

                user.Id = p.Get<int>("@Id");
            }
        }

        
    }
}
