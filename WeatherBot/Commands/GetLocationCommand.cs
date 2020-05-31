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

            string response = null;

            switch (e?.Message?.Text)
            {
                case "Сейчас":      response = GetForecast(0);  break;
                case "На 12 часов": response = GetForecast(4);  break;
                case "На день":     response = GetForecast(8);  break;
                case "На три дня":  response = GetForecast(16); break;
            }

            if (String.IsNullOrEmpty(response))
            {
                var replyKeyboard = new ReplyKeyboardMarkup
                    (new KeyboardButton("Отправить геолокацию") { RequestLocation = true });

                await Bot.client.SendTextMessageAsync(e?.Message?.From?.Id, "Не могу показать тебе погоду, так как я не знаю, " +
                    "где ты находишься <ins>(отправь геолокацию)</ins>", ParseMode.Html, replyMarkup: replyKeyboard);

                return;
            }

            await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, response, ParseMode.Html);
        }

        public static string GetForecast(int count)
        {
            string response;

            if (Bot.Latitude is null || Bot.Longitude is null)
            {
                return null;
            }

            var url = "https://api.openweathermap.org/data/2.5/forecast?lat=" + Convert.ToString(Bot.Latitude) + 
                "&lon=" + Convert.ToString(Bot.Longitude) + "&units=metric&appid=" + Bot.ReadTokenOWM(@"[path].json");

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

            Func<WeatherInfo, string> answer = delegate (WeatherInfo data)
            {
                return $"{data.Dt_txt}: <em>температура</em> <b>{data.Main.Temp} ℃</b>, " +
                       $"<em>скорость ветра</em> <b>{data.Wind.Speed}м/с</b>, " +
                       $"<em>облачность</em> <b>{data.Clouds.All}%</b>\n\n";
            };

            string forecast = null;

            for (var i = 0; i <= count; i++)
            {
                forecast += answer(forecastResponse.List[i]);
            }

            return forecast;
        }
    }
}
