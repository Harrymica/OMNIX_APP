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
            var existingUser = await GetUser(user.Id);

            if (existingUser != null)
            {
                return existingUser;
            }

            return await SignUpUser(user);
        }

        public async Task<TelegramUser> SignUpUser(TelegramUser user)
        {
            var existingUser = await GetUser(user.Id);
            var url = "https://vwrdiqboqifvqcqejvzl.supabase.co";//Environment.GetEnvironmentVariable("https://vwrdiqboqifvqcqejvzl.supabase.co");
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3cmRpcWJvcWlmdnFjcWVqdnpsIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgzNzUzNTMsImV4cCI6MjA0Mzk1MTM1M30.gglFnEhruAVUOcbCE6Us7BxVYnl1MHe9IFAwmWE9G5I";// Environment.GetEnvironmentVariable("");
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

        public async Task<TelegramUser> GetUser(long userId)
        {
            var url = "https://vwrdiqboqifvqcqejvzl.supabase.co";//Environment.GetEnvironmentVariable("https://vwrdiqboqifvqcqejvzl.supabase.co");
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3cmRpcWJvcWlmdnFjcWVqdnpsIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgzNzUzNTMsImV4cCI6MjA0Mzk1MTM1M30.gglFnEhruAVUOcbCE6Us7BxVYnl1MHe9IFAwmWE9G5I";// Environment.GetEnvironmentVariable("");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };
            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            var user = await supabase.From<TelegramUser>().Select(x => new object[] { x.Id == userId}).Get();//.Get().Match({ username });//.ExecuteAsync();
            Console.WriteLine(user.Model.FirstName);
            return user.Model;
        }
    }

}
