namespace MiniAccountSystem.Models
{
    public class VoucherEntry
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string EntryType { get; set; } = "Debit";
    }
}
