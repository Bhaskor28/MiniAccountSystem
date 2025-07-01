using MiniAccountSystem.Models;

namespace MiniAccountSystem.Services.Vouchers
{
    public interface IVoucherService
    {
        Task<List<AccountChart>> GetAccountsAsync();
        Task SaveVoucherAsync(Voucher voucher);
    }
}
