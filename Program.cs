using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OMNIX_App.Services.TelegramAuthService;
using OMNIX_APP.Data;
using OMNIX_APP.Services.TelegramAuthService;
using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bots.Http;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace OMNIX_APP
{
    public class Program
    {
        private static ITelegramBotClient botClient;
        public static void Main(string[] args)
        {
            // Start the Telegram bot in a separate thread
            Task.Run(() => StartTelegramBot());

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ITelegramAuth, TelegramAuth>();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();


            

        }



        private static async Task StartTelegramBot()
        {
            //My Telegrambot goes here

            string token = "7324366764:AAE0FiQdBDSnHgZvnv2ZU3f5f9DAiGIMhx8"; // e.g., "123456789:ABCdefGhIJKlmnoPQRstuVWXyz"
            botClient = new TelegramBotClient(token);

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // Receive all update types
            };

            botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token
            );

            var me = botClient.GetMeAsync();
            Console.WriteLine($"Start listening for @{me.Result.Username}");
            //Console.ReadLine(); // Keep the application running until Enter is pressed

            cts.Cancel(); // Stop the bot

        }

        private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Type == MessageType.Text)
            {
                var chatId = update.Message.Chat.Id;
                var userMessage = update.Message.Text;

                Console.WriteLine($"Received a '{userMessage}' message in chat {chatId}.");

                if (userMessage.Equals("/start", StringComparison.OrdinalIgnoreCase))
                {
                    // Create an inline keyboard button that redirects to your website
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
            InlineKeyboardButton.WithUrl("Visit Our Website", "https://omnix-app.onrender.com/")
        });

                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Welcome! Click the button below to start mining.",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    // Reply back to the user with their message prefixed
                    await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: $"This is what you said: {userMessage}",
                        cancellationToken: cancellationToken);
                }
            }
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error occurred: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}