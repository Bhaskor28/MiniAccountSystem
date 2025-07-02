namespace MiniAccountSystem.Services.ModuleAccess
{
    public interface IModuleAccessService
    {
        Task<List<string>> GetModulesByRoleAsync(string role);
    }
}
