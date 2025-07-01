namespace MiniAccountSystem.Models
{
    public class AccountChart
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountId { get; set; }
        public string AccountType { get; set; }
    }
}
