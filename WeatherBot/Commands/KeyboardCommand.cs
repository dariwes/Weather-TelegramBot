using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Commands
{
    class KeyboardCommand 
    {
        public static string Name => "/keyboard";

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!Name.Equals(e?.Message?.Text))
            {
                return;
            }

            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                 new[]
                 {
                     new KeyboardButton("Сейчас"),
                     new KeyboardButton("На 12 часов")
                 },
                 new[]
                 {
                     new KeyboardButton("На день"),
                     new KeyboardButton("На три дня")
                 }
            });

            await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, "Узнать погоду:",
                replyMarkup: replyKeyboard);
        }
    }
}
