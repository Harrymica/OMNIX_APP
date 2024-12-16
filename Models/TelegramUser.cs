using OMNIX_App.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;


namespace OMNIX_App.Models
{


    [Table("telegram_users")]
    public class TelegramUser : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }
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
        [Column("number_of_referal")]
        public int NumberOfReferal { get; set; }
        [Column("sub_referal")]
        public int SubReferal { get; set; }
        [Column("referred_by")]
        public string ReferredBy { get; set; }
        [Column("last_referral")]
        public string LastReferral { get; set; }
        [Column("token")]
        public decimal Token { get; set; }
        [Column("date_of_registration")]
        public DateTime DateOfRegistration { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
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
