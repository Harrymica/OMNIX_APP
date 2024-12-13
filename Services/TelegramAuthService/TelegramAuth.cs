using OMNIX_App.Models;
using OMNIX_App.Services.TelegramAuthService;
using Polly;
using Supabase.Interfaces;

namespace OMNIX_APP.Services.TelegramAuthService
{
    public class TelegramAuth : ITelegramAuth
    {
        
        public async Task<TelegramUser> SignInUser(TelegramUser user)
        {
            var existingUser = await GetUser(user.TelegramId);

            if (existingUser != null)
            {
                return existingUser;
            }

            return await SignUpUser(user);
        }

        public async Task<TelegramUser> SignUpUser(TelegramUser user)
        {
            var existingUser = await GetUser(user.TelegramId);
            var url = "https://ltrbohzosutyfqyyqkfi.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imx0cmJvaHpvc3V0eWZxeXlxa2ZpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzMwODMyNDQsImV4cCI6MjA0ODY1OTI0NH0._h2Z1_WnhaPyQpZcUFoWRKgANkTLBo7eCE6xD1KkGIQ";// Environment.GetEnvironmentVariable("");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };
            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
            var insertedUser = await supabase.From<TelegramUser>().Insert(user);//.ExecuteAsync();
              return insertedUser.Model;//.Data;

            }

        }

        public async Task<TelegramUser> GetUser(string userId)
        {
            var url = "https://ltrbohzosutyfqyyqkfi.supabase.co";//Environment.GetEnvironmentVariable("https://vwrdiqboqifvqcqejvzl.supabase.co");
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imx0cmJvaHpvc3V0eWZxeXlxa2ZpIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MzMwODMyNDQsImV4cCI6MjA0ODY1OTI0NH0._h2Z1_WnhaPyQpZcUFoWRKgANkTLBo7eCE6xD1KkGIQ";// Environment.GetEnvironmentVariable("");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };
            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            var user = await supabase.From<TelegramUser>().Where(x => x.TelegramId == userId).Get();//Select(x => new object[] { x.Id == userId}).Get();//.Get().Match({ username });//.ExecuteAsync();
            //Console.WriteLine(user.Model.FirstName);
            return user.Model;
        }
    }

}
