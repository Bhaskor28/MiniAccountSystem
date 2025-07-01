namespace MiniAccountSystem.Models
{
    public class Voucher
    {
        public int VoucherId { get; set; }
        public string VoucherType { get; set; } = string.Empty;
        public DateTime VoucherDate { get; set; }
        public string ReferenceNo { get; set; } = string.Empty;
        public List<VoucherEntry> Entries { get; set; } = new();
    }
}
