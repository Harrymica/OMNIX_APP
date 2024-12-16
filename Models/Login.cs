using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMNIX_App.Models
{
    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserModel
    {
       public string userName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelegramId { get; set; }
       
    }
}
