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
using OMNIX_APP.Services.BotService;
using OMNIX_App.Models;

namespace OMNIX_APP
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllers();

            builder.Services.AddHttpClient();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ITelegramAuth, TelegramAuth>();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<Tel_BotService>();
            var app = builder.Build();
          
            TelegramBotClient botClient = new TelegramBotClient("7324366764:AAE0FiQdBDSnHgZvnv2ZU3f5f9DAiGIMhx8");
        

            SetWebhook(botClient).GetAwaiter().GetResult();


            //over here goes the minimal api
            app.MapPost("/api/bot", async (Update update, ITelegramAuth telegramServ) =>
            {

                /*var userInfo =*/ await UpdataeHandler(botClient, update, telegramServ);
                //return Results.Ok(userInfo); // Ensure this returns a 200 OK response
                return Results.Ok();
            });

            // Define the webhook setting method
            async Task SetWebhook(TelegramBotClient bot)
            {
                var webhookUrl = "https://omnix-app.onrender.com/api/bot"; // Adjust this URL according to your deployment
                await bot.SetWebhookAsync(webhookUrl);
            }

            // Define the update handler method
            async Task/*<TelegramUser>*/ UpdataeHandler(TelegramBotClient client, Update update, ITelegramAuth telegramServ)
            {
                TelegramUser users = new TelegramUser();
                if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
                {
                    var text = update.Message.Text;
                    var chatid = update.Message.Chat.Id;
                    var invitationLink = await botClient.GetChat(chatid);
                    UserModel user = new UserModel
                    {
                        userName = update.Message.From!.Username,
                        TelegramId = update.Message.From.Id.ToString(),
                        FirstName = update.Message.Chat.FirstName,
                        LastName = update.Message.Chat.LastName,
                    };

                    //if(user != null)
                    //{


                    //Id = (long)Guid.NewGuid().GetHashCode(),
                    users.TelegramId = update.Message.From.Id.ToString();//getId.ToString(),
                    users.Username = user.userName;
                    users.FirstName = user.FirstName;
                    users.LastName = user.LastName;
                    users.DateOfRegistration = DateTime.UtcNow;
                        

                        

                    if (text.Equals("/start", StringComparison.OrdinalIgnoreCase))
                    {
                        // Create an inline keyboard button that redirects to your website
                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        InlineKeyboardButton.WithUrl("Visit Our Website", "https://omnix-app.onrender.com/")
                        });

                        await client.SendTextMessageAsync(
                            chatId: chatid,
                            text: $"Welcome {user.FirstName}! Click the button below to start mining, {user.FirstName}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        // Reply back to the user with their message prefixed
                        await client.SendTextMessageAsync(chatId: chatid,text: $"This is what you said: {text}, here is your invitationLink {invitationLink}");
                    }

                    Console.WriteLine($"{user.userName} | {chatid} | {text}, {user.FirstName}");
                    //await telegramServ.SignUpUser(users);

                    // }
                }
                    //return users;
            }
          


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

            app.MapControllers(); // Map the controllers
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();


        }  
    }
}