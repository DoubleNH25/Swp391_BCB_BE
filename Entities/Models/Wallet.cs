namespace Entities.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            HistoryWallets = new HashSet<HistoryWallet>();
        }

        public int Id { get; set; }
        public int? IdUser { get; set; }
        public decimal? Balance { get; set; }

        public virtual User? IdUserNavigation { get; set; }
        public virtual ICollection<HistoryWallet> HistoryWallets { get; set; }
    }
}
