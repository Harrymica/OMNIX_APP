using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bots.Http;
using Telegram.Bot.Types;
using OMNIX_App.Models;
using OMNIX_App.Services.TelegramAuthService;

namespace OMNIX_APP.Services.BotService
{
    public class Tel_BotService
    {
        public Tel_BotService()
        {
            // Start the Telegram bot in a separate thread
            //Task.Run(() => StartTelegramBot());
            //_telegramAuth = telegramAuth;
        }

        private static ITelegramBotClient botClient;
        private static bool isRunning = false;

        // private readonly ITelegramAuth _telegramAuth;

        public async Task StartTelegramBot()
        {
            //My Telegrambot goes here

            string token = "7324366764:AAE0FiQdBDSnHgZvnv2ZU3f5f9DAiGIMhx8"; // e.g., "123456789:ABCdefGhIJKlmnoPQRstuVWXyz"
            botClient = new TelegramBotClient(token);

            using var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // Receive all update types
            };


            isRunning = true; // Set running state
            try
            {
                botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
            finally
            {
                isRunning = false; // Reset running state on exit
            }

            var me = await botClient.GetMeAsync();
             Console.WriteLine($"Start listening for @{me.Username}");
             Console.ReadLine(); // Keep the application running until Enter is pressed

             cts.Cancel(); // Stop the bot

        

    }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
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
                    //isRunning = false;
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

        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error occurred: {exception.Message}");
            return Task.CompletedTask;
        }

        public async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {

        }

        public async Task UpdataeHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var updates = await botClient.GetUpdatesAsync();
            var userUpdate = updates.FirstOrDefault(u => u.Type == UpdateType.Message);

            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {

                    var text = update.Message.Text;
                    var id = update.Message.Chat.Id;
                    var Username = update.Message.Chat.Username;
                    var firstName = update.Message.Chat.FirstName;
                    var lastName = update.Message.Chat.LastName;
                    //var location = update.Message.Chat.Location;
                    //var Invitation = update.Message.Chat.InviteLink;

                    //Console.WriteLine($"{Username} |{id} | {text}, : from {location}, invited by {Invitation}");

                    TelegramUser users = new TelegramUser
                    {
                        Username = Username,
                        FirstName = firstName,
                        LastName = lastName
                    };

                    await SignInUser(users);
                    // await lStorage.SetItemAsStringAsync("username", users.username);
                    //await CheckUser(users.Username);

                   
                }
            }
        }

        public async Task<TelegramUser> GetUserTelegramInfo()
        {
            Console.WriteLine("Bot started");
            if (botClient == null)
            {
                await StartTelegramBot();

            }
            TelegramUser Duser = new TelegramUser();
                var updates = await botClient.GetUpdatesAsync();
                var userUpdate = updates.FirstOrDefault(u => u.Type == UpdateType.Message);
                if (userUpdate != null)
                {
                    var user = userUpdate.Message.From;
                    //{
                    Duser.TelegramId = user.Id.ToString();
                    Duser.Username = user.Username;
                    Duser.FirstName = user.FirstName;
                    Duser.LastName = user.LastName;
                    //};

                    await SignInUser(Duser);
                }
                

               

            
            return Duser;


        }
        public async Task<TelegramUser> SignInUser(TelegramUser user)
        {
            var existingUser = await GetUser(user.Id.ToString());

            if (existingUser != null)
            {
                return existingUser;
            }

            return await SignUpUser(user);
        }

        public async Task<TelegramUser> SignUpUser(TelegramUser user)
        {
            var url = "https://vwrdiqboqifvqcqejvzl.supabase.co";//Environment.GetEnvironmentVariable("https://vwrdiqboqifvqcqejvzl.supabase.co");
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3cmRpcWJvcWlmdnFjcWVqdnpsIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgzNzUzNTMsImV4cCI6MjA0Mzk1MTM1M30.gglFnEhruAVUOcbCE6Us7BxVYnl1MHe9IFAwmWE9G5I";// Environment.GetEnvironmentVariable("");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };
            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            var insertedUser = await supabase.From<TelegramUser>().Insert(user);//.ExecuteAsync();
            return insertedUser.Model;//.Data;
        }

        public async Task<TelegramUser> GetUser(string userId)
        {
            var url = "https://vwrdiqboqifvqcqejvzl.supabase.co";//Environment.GetEnvironmentVariable("https://vwrdiqboqifvqcqejvzl.supabase.co");
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZ3cmRpcWJvcWlmdnFjcWVqdnpsIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjgzNzUzNTMsImV4cCI6MjA0Mzk1MTM1M30.gglFnEhruAVUOcbCE6Us7BxVYnl1MHe9IFAwmWE9G5I";// Environment.GetEnvironmentVariable("");
            var options = new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            };
            var supabase = new Supabase.Client(url, key, options);
            await supabase.InitializeAsync();

            var user = await supabase.From<TelegramUser>().Select(x => new object[] { x.Id }).Get();//.Get().Match({ username });//.ExecuteAsync();
            return user.Model;
        }
        /*private async Task AuthenticateUser(TelegramUser user)
        {
           // var bot = new TelegramBotClient("7324366764:AAE0FiQdBDSnHgZvnv2ZU3f5f9DAiGIMhx8");

            var updates = await botClient.GetUpdatesAsync();
            var userUpdate = updates.FirstOrDefault(u => u.Type == UpdateType.Message);

            if (userUpdate != null)
            {
                var user = userUpdate.Message.From;
                _username = user.Username;
                _userId = user.Id;
                _firstName = user.FirstName;

                await _telegramAuth.SignInUser(new TelegramUser
                {
                    Username = _username,
                    Id = _userId,
                    FirstName = _firstName
                });
            }
            else
            {
                // Handle the case where the user is not authenticated
            }
        }*/
    }
}
