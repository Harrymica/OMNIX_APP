using OMNIX_App.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace OMNIX_App.Models
{


    [Table("telegram_users")]
    public class TelegramUser : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }
        [Column("telegramid")]
        public string TelegramId { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("referralcode")]
        public string ReferralCode { get; set; }
        [Column("referalbonus")]
        public int ReferalBonus { get; set; }
        [Column("numberofreferal")]
        public int NumberOfReferal { get; set; }
        [Column("subreferal")]
        public int SubReferal { get; set; }
        [Column("referredby")]
        public string ReferredBy { get; set; }
        [Column("lastreferral")]
        public string LastReferral { get; set; }
        [Column("token")]
        public decimal Token { get; set; }
        [Column("dateofregistration")]
        public DateTime DateOfRegistration { get; set; }
        [Column("starttime")]
        public DateTime StartTime { get; set; }
        [Column("endtime")]
        public DateTime EndTime { get; set; }
    }

    /*public class TelegramUsers
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        [Required]

        public string username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ReferralCode { get; set; }
        public int? ReferalBonus { get; set; } = 0;
        public int? NumberofReferal { get; set; } = 0;
        public int? SubReferal { get; set; } = 0;
        public string? referredBy { get; set; }
        //public string? PhoneNumber { get; set; }

        public string? lastreferral { get; set; }
        public Wallet? UserWallet { get; set; }
        public int UserWalletId { get; set; }
        public decimal Token { get; set; }

        //public UserCryptoModel userCryptoModel {get; set;}

        //[ForeignKey(nameof(UserCryptoModel))]
        //public string? userCryptoModelId {get; set;}

        public DateTime DateofRegisteration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }*/
}
