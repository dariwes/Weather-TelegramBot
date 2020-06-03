using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Commands
{
    class GetLocationCommand
    {
        public static List<string> keyboardButton = new List<string>()
        {
            "Сейчас", "На 12 часов", "На день", "На три дня"
        };

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!keyboardButton.Contains(e.Message.Text))
            {
                return;
            }

            ForecastResponse response = null;
            var count = 0;

            switch (e?.Message?.Text)
            {
                case "Сейчас":      
                    response = GetForecast(e.Message.Chat.Id);
                    count = 0;
                break;

                case "На 12 часов":
                    response = GetForecast(e.Message.Chat.Id);
                    count = 4; 
                break;

                case "На день":     
                    response = GetForecast(e.Message.Chat.Id);
                    count = 8;
                break;

                case "На три дня":  
                    response = GetForecast(e.Message.Chat.Id);
                    count = 24;
                break;
            }

            if (response is null)
            {
                SetLocation(e.Message.From.Id);
                return;
            }

            Func<WeatherInfo, string> answer = delegate (WeatherInfo data)
            {
                return $"{data.Dt_txt}: <em>температура</em> <b>{data.Main.Temp} ℃</b>, " +
                       $"<em>скорость ветра:</em> <b>{data.Wind.Speed}м/с</b>, " +
                       $"<em>облачность:</em> <b>{data.Clouds.All}%</b>\n\n";
            };

            string forecast = null;

            for (var i = 0; i <= count; i++)
            {
                forecast += answer(response.List[i]);
            }

            await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, forecast, ParseMode.Html);
        }

        public static ForecastResponse GetForecast(long id)
        {
            var user = UserExistCommand.GetUser(id);

            if (user.Latitude is null || user.Longitude is null)
            {
                return null;
            }

            string response;
            var url = "https://api.openweathermap.org/data/2.5/forecast?lat=" + 
                Convert.ToString(user.Latitude) + "&lon=" + Convert.ToString(user.Longitude) + 
                "&units=metric&appid=" +  Bot.ReadTokenOWM(@"[path].json");

            if (!Bot.CheckUrl(url))
            {
                return null;
            }

            HttpWebRequest httpWebRequest   = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            ForecastResponse forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(response);

            return forecastResponse;
        }

        public static async void SetLocation(long id)
        {
            var replyKeyboard = new ReplyKeyboardMarkup
                    (new KeyboardButton("Отправить геолокацию") { RequestLocation = true });

            await Bot.client.SendTextMessageAsync(id, "Не могу показать тебе погоду, так как я не знаю, " +
                "где ты находишься <ins>(отправь геолокацию)</ins>", ParseMode.Html, replyMarkup: replyKeyboard);
        }
    }
}
