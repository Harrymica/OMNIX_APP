using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace OMNIX_App.Models
{
    [Table("payment_data")]
    public class PaymentData : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 20);
        [Column("price_id")]
        public string PriceId { get; set; }
        [Column("user_id")]
       
        public long UserId { get; set; }

        public TelegramUser User { get; set; }
        [Column("status")]
        public string Status { get; set; } = "Not Paid";
        [Column("level")]
        public string Level { get; set; }
        [Column("date_paid")]
        public DateTime DatePaid { get; set; }
    }

}
