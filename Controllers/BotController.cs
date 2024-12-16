using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OMNIX_App.Models;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using OMNIX_App.Services.TelegramAuthService;

namespace OMNIX_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly string _botToken;
        private readonly IConfiguration _configuration;
        private readonly ITelegramAuth _telegramServ;
        private TelegramBotClient _bot = new TelegramBotClient("7324366764:AAE0FiQdBDSnHgZvnv2ZU3f5f9DAiGIMhx8");

        public BotController(IConfiguration configuration, ITelegramAuth telegramServ)
        {
            _configuration = configuration;
            _telegramServ = telegramServ;
            // Access the BotToken from appsettings.json
            _botToken = _configuration["Telegram:BotToken"];

            // Set the webhook when the bot is initialized
           // StartReceivingUpdates().GetAwaiter().GetResult();

            SetWebhook().GetAwaiter().GetResult();
        }

        /*[HttpPost]
        public async Task<IActionResult> Post([FromBody] JObject update)
        {
            await StartReceivingUpdates();
            return Ok();
        }*/

        private async Task SetWebhook()
        {
            var webhookUrl = "https://omnix-app.onrender.com/api/bot/start"; // Adjust this URL according to your deployment
            await _bot.SetWebhook(webhookUrl);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            await UpdataeHandler(_bot, update, CancellationToken.None);
            return Ok(); // Ensure this returns a 200 OK response
        }


        /*[HttpPost("start")]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                    {
                UpdateType.Message,
                UpdateType.EditedMessage,
                    }
            };
            // Start receiving updates
            _bot.StartReceiving(UpdataeHandler, ErrorHandler, receiverOptions, cancellationToken: cts.Token);
            return Ok();
        } */

        private async Task StartReceivingUpdates()
        {
            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                    {
                UpdateType.Message,
                UpdateType.EditedMessage,
                    }
            };
            await _bot.SetWebhook("https://omnix-app.onrender.com/api/bot");
            /*, allowedUpdates: new UpdateType[]
                    {
                UpdateType.Message,
                UpdateType.EditedMessage,
                    }*/

            // Start receiving updates
            _bot.StartReceiving(UpdataeHandler, ErrorHandler, receiverOptions, cancellationToken: cts.Token);
        }

        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {

        }

        private async Task UpdataeHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {

                    var text = update.Message.Text;
                    var chatid = update.Message.Chat.Id;
                    var Username = update.Message.From.Username;//Chat.Username;
                    var firstName = update.Message.From.FirstName;
                    var lastName = update.Message.Chat.LastName;
                    var getId = update.Message.From.Id;


                    TelegramUser users = new TelegramUser
                    {
                       // Id = (long)Guid.NewGuid().GetHashCode(),
                        TelegramId = getId.ToString(),
                        Username = Username,
                        FirstName = firstName,
                        LastName = lastName
                    };

                    if (text.Equals("/start", StringComparison.OrdinalIgnoreCase))
                    {
                        // Create an inline keyboard button that redirects to your website
                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                    InlineKeyboardButton.WithUrl("Visit Our Website", "https://omnix-app.onrender.com/")
                    });

                        await _bot.SendTextMessageAsync(
                            chatId: chatid,
                            text: $"Welcome! Click the button below to start mining, {firstName}",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: token);
                    }
                    else
                    {
                        // Reply back to the user with their message prefixed
                        await _bot.SendTextMessageAsync(
                            chatId: chatid,
                            text: $"This is what you said: {text}",
                            cancellationToken: token);
                    }
                    Console.WriteLine($"{Username} |{chatid} | {text}, {firstName}");
                    await _telegramServ.SignUpUser(users);
                }
            }
        }
    }
}




