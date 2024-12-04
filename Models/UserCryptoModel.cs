using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNIX_App.Models
{
    public class UserCryptoModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 8);
        public decimal Token { get; set; }
        public User? User { get; set; }
        public string? UserId { get; set; }
        
        public DateTime StartTime { get; set; } =   DateTime.UtcNow;
    }
}
