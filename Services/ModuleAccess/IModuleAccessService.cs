namespace MiniAccountSystem.Services.ModuleAccess
{
    public interface IModuleAccessService
    {
        Task<List<string>> GetModulesByRoleAsync(string role);
        Task<bool> HasAccessAsync(string role, string moduleName);
    }
}
