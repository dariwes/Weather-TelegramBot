using Telegram.Bot.Types.Enums;

namespace WeatherBot.Commands
{
    class CapitalWeatherCommand
    {
        public static async void Execute(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            var message = CityWeatherCommand.GetCityWeather(e?.CallbackQuery?.Data);
            await Bot.client.SendTextMessageAsync(e?.CallbackQuery?.From?.Id, 
                $"<em>Температура в {e?.CallbackQuery?.Data} </em>{message}.", ParseMode.Html);
        }
    }
}
