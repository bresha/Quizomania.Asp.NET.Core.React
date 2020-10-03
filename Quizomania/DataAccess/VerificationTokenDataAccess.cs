using Dapper;
using Quizomania.Enums;
using Quizomania.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Quizomania.DataAccess
{
    public class VerificationTokenDataAccess : IVerificationTokenDataAccess
    {
        private readonly ConnectionString _connectionString;

        public VerificationTokenDataAccess(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<VerificationToken> GetVerificationTokenAsync(int userId, string token, TokenPurposeEnum tokenPurpose)
        {
            VerificationToken output;

            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();

                p.Add("@UserId", userId);
                p.Add("@Token", token);
                p.Add("@TokenPurpose", tokenPurpose);

                output = await connection
                    .QueryFirstOrDefaultAsync<VerificationToken>("dbo.spVerificationToken_Get", p, commandType: CommandType.StoredProcedure);
            }

            return output;
        }

        public async Task InsertVerificationTokenAsync(VerificationToken token)
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();

                p.Add("@UserId", token.UserId);
                p.Add("@Token", token.Token);
                p.Add("@CreatedAt", token.CreatedAt);
                p.Add("@TokenPurpose", token.TokenPurpose);

                await connection
                   .ExecuteAsync("dbo.spVerificationTokens_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
