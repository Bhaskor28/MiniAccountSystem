using MiniAccountSystem.Models;

namespace MiniAccountSystem.Services.Vouchers
{
    public interface IVoucherService
    {
        Task<List<AccountChart>> GetAccountsAsync();
        Task SaveVoucherAsync(Voucher voucher);
        Task<List<Voucher>> GetAllVouchersAsync();
        Task<Voucher?> GetVoucherByIdAsync(int id);
        Task DeleteVoucherAsync(int id);
        Task UpdateVoucherAsync(Voucher voucher);
    }
}
