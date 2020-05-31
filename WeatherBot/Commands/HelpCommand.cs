using Telegram.Bot.Types.Enums;

namespace WeatherBot.Commands
{
    class HelpCommand
    {
        public static string Name => "/help";

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!Name.Equals(e?.Message?.Text))
            {
                return;
            }

            var answer = "<i>Введи название города на русском/английском языке или отправь мне свою" +
                " геопозицию, я смогу подсказать температуру, скорость ветра и облачность в этой местности.</i>";

            await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, answer.ToString(), ParseMode.Html);
        }
    }
}
