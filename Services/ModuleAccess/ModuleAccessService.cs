
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniAccountSystem.Services.ModuleAccess
{
    public class ModuleAccessService : IModuleAccessService
    {
        private readonly IConfiguration _config;
        private readonly string _connection;

        public ModuleAccessService(IConfiguration config)
        {
            _config = config;
            _connection = _config.GetConnectionString("Connection");
        }
        public async Task<List<string>> GetModulesByRoleAsync(string role)
        {
            var modules = new List<string>();

            using SqlConnection con = new(_connection);
            using SqlCommand cmd = new("sp_GetModulesByRole", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@RoleName", role);

            await con.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                modules.Add(reader["ModuleName"].ToString()!);
            }

            return modules;
        }
    }
}
