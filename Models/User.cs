using System.ComponentModel.DataAnnotations.Schema;

namespace OMNIX_App.Models
{
    public class User
    {

        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ReferralCode { get; set; }
        public int? ReferalBonus { get; set; } = 0;
        public int? NumberofReferal { get; set; } = 0;
        public int? SubReferal { get; set; } = 0;
        public string? referredBy { get; set; }
        public string? PhoneNumber { get; set; }

        public string? lastreferral { get; set; }
        public Wallet? UserWallet { get; set; }
        public int UserWalletId { get; set; }

        public DateTime DateofRegisteration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Token { get; set; }
        public PaymentData? Paymentdata { get; set; }
        [ForeignKey(nameof(PaymentData))]
        public string? PaymentdataId { get; set; }


    }
}
