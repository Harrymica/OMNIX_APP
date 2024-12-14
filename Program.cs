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

namespace OMNIX_APP
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            //Tel_BotService newBot = new Tel_BotService();
            //newBot.StartTelegramBot();


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllers();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ITelegramAuth, TelegramAuth>();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddSingleton<Tel_BotService>();
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

           // app.MapControllers(); // Map the controllers
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();


        }  
    }
}