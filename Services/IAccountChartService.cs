using MiniAccountSystem.Models;

namespace MiniAccountSystem.Services
{
    public interface IAccountChartService
    {
        Task<List<AccountChart>> GetAllAsync();
        Task<AccountChart> GetByIdAsync(int id);
        Task AddAsync(AccountChart account);
        Task UpdateAsync(AccountChart account);
        Task DeleteAsync(int id);
    }
}
