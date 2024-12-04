
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace OMNIX_App.Models
{

    [Table("wallets")]
    public class Wallet : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("uid")]
        public string Uid { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("memo")]
        public string Memo { get; set; }
        [Column("exchange_name")]
        public string ExchangeName { get; set; }
        [Column("user_id")]
        public long UserId { get; set; }
       
        public TelegramUser User { get; set; }
    }

    /*public class Wallet
    {
        public int Id { get; set; }
        public string? UID { get; set; }
        public string? Address { get; set; }
        public string? Memo { get; set; }
        public string? ExchangeName { get; set; }
        public User user { get; set; }

        public string userId { get; set; }
    }*/

}
