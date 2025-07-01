using Microsoft.Data.SqlClient;
using MiniAccountSystem.Models;
using System.Data;

namespace MiniAccountSystem.Services.Vouchers
{
    public class VoucherService : IVoucherService
    {
        private readonly IConfiguration _config;
        private readonly string _connection;

        public VoucherService(IConfiguration config)
        {
            _config = config;
            _connection = _config.GetConnectionString("Connection");
        }
        public async Task<List<AccountChart>> GetAccountsAsync()
        {
            using SqlConnection con = new(_connection);
            var accounts = new List<AccountChart>();

            SqlCommand cmd = new("sp_ManageChartOfAccounts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "SELECT_ALL");

            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                accounts.Add(new AccountChart
                {
                    AccountId = (int)reader["AccountId"],
                    AccountName = reader["AccountName"].ToString()
                });
            }
            return accounts;
        }

        public async Task SaveVoucherAsync(Voucher voucher)
        {
            using SqlConnection con = new(_connection);
            using SqlCommand cmd = new("sp_SaveVoucher", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@VoucherType", voucher.VoucherType);
            cmd.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
            cmd.Parameters.AddWithValue("@ReferenceNo", voucher.ReferenceNo);

            // Table-valued parameter
            var table = new DataTable();
            table.Columns.Add("AccountId", typeof(int));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("EntryType", typeof(string));

            foreach (var entry in voucher.Entries)
            {
                table.Rows.Add(entry.AccountId, entry.Amount, entry.EntryType);
            }

            var tvpParam = cmd.Parameters.AddWithValue("@Entries", table);
            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "TVP_VoucherEntry";

            await con.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
