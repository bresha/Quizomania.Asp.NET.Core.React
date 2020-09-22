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
    public class RefreshTokensDataAccess : IRefreshTokensDataAccess
    {
        private readonly ConnectionString _connectionString;

        public RefreshTokensDataAccess(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task InsertRefreshTokenAsync(RefreshToken refreshToken)
        {
            using (var connection = new SqlConnection(_connectionString.Value))
            {
                var p = new DynamicParameters();
                p.Add("@Token", refreshToken.Token);
                p.Add("@ExpiryDate", refreshToken.ExpiryDate);
                p.Add("@UserId", refreshToken.UserId);

                await connection
                    .ExecuteAsync("dbo.spRefreshTokens_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
