using OMNIX_App.Models;

namespace OMNIX_App.Services.TelegramAuthService
{
    public interface ITelegramAuth
    {
        Task<TelegramUser> GetUser(string userId);
        Task/*<TelegramUser>*/ SignInUser(TelegramUser user);
        Task/*<TelegramUser>*/ SignUpUser(TelegramUser user);
    }
}