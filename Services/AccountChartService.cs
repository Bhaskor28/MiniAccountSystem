using Microsoft.Data.SqlClient;
using MiniAccountSystem.Models;
using System.Data;

namespace MiniAccountSystem.Services
{
    public class AccountChartService : IAccountChartService
    {
        private readonly IConfiguration _config;
        private readonly string _connection;
        public AccountChartService(IConfiguration config)
        {
            _config = config;
            _connection = _config.GetConnectionString("Connection");
        }

        public async Task AddAsync(AccountChart account)
        {
            using SqlConnection con = new(_connection);
            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "INSERT");
            cmd.Parameters.AddWithValue("@AccountName", account.AccountName);
            cmd.Parameters.AddWithValue("@ParentAccountId", account.ParentAccountId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountType", account.AccountType);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using SqlConnection con = new(_connection);
            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@AccountId", id);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<AccountChart>> GetAllAsync()
        {
            var result = new List<AccountChart>();
            using SqlConnection con = new(_connection);
            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_ALL");

            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new AccountChart
                {
                    AccountId = (int)reader["AccountId"],
                    AccountName = reader["AccountName"].ToString(),
                    ParentAccountId = reader["ParentAccountId"] as int?,
                    AccountType = reader["AccountType"].ToString()
                });
            }

            return result;
        }

        public async Task<AccountChart> GetByIdAsync(int id)
        {
            using SqlConnection con = new(_connection);
            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_BY_ID");
            cmd.Parameters.AddWithValue("@AccountId", id);

            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AccountChart
                {
                    AccountId = (int)reader["AccountId"],
                    AccountName = reader["AccountName"].ToString(),
                    ParentAccountId = reader["ParentAccountId"] as int?,
                    AccountType = reader["AccountType"].ToString()
                };
            }

            return null!;
        }

        public async Task UpdateAsync(AccountChart account)
        {
            using SqlConnection con = new(_connection);
            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "UPDATE");
            cmd.Parameters.AddWithValue("@AccountId", account.AccountId);
            cmd.Parameters.AddWithValue("@AccountName", account.AccountName);
            cmd.Parameters.AddWithValue("@ParentAccountId", account.ParentAccountId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@AccountType", account.AccountType);

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
