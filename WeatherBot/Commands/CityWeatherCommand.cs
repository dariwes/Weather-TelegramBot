using ApiAiSDK;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Telegram.Bot.Types.Enums;

namespace WeatherBot.Commands
{
    class CityWeatherCommand
    {
        public static List<string> Commands = new List<string>()
        {
            StartCommand.Name,
            KeyboardCommand.Name,
            HelpCommand.Name,
            InlineCommand.Name,
            ChartCommand.Name
        };

        private static readonly TranslationClient client = TranslationClient.Create();

        private static readonly ApiAi apiAi = new ApiAi(new AIConfiguration(
            ReadTokenApiAi(@"[path].json"), SupportedLanguage.Russian));

        public static string ReadTokenApiAi(string filePath)
        {
            TokenInfo jsonObj = JsonConvert.DeserializeObject<TokenInfo>(File.ReadAllText(filePath));
            return jsonObj.TokenApiAi;
        }
        
        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e?.Message?.Type != MessageType.Text || Commands.Contains(e?.Message?.Text)
                || GetLocationCommand.keyboardButton.Contains(e?.Message?.Text))
            {
                return;
            }

            var transate = client.TranslateText(e?.Message?.Text, LanguageCodes.English, LanguageCodes.Russian);
            var message = GetCityWeather(transate?.TranslatedText);

            if (!String.IsNullOrEmpty(message))
            {
                await Bot.client.SendTextMessageAsync(e?.Message?.From?.Id, 
                    $"<em>Температура в {e?.Message?.Text}</em> {message}.", ParseMode.Html);
            }
            else
            {
                var response = apiAi.TextRequest(e?.Message?.Text);
                message = response.Result.Fulfillment.Speech;

                if (String.IsNullOrEmpty(message))
                {
                    message = "Не понимаю.";
                }

                await Bot.client.SendTextMessageAsync(e?.Message?.From?.Id, message);
            }
        }

        public static string GetCityWeather(string city)
        {
            string response;
            var url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + 
                "&units=metric&appid=" + Bot.ReadTokenOWM(@"[path].json");

            if (!Bot.CheckUrl(url))
            {
                return null;
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            
            return $"<b>{weatherResponse.Main.Temp} ℃</b>, " +
                   $"<em>скорость ветра:</em> <b>{weatherResponse.Wind.Speed}м/с</b>, " +
                   $"<em>облачность:</em> <b>{weatherResponse.Clouds.All}%</b>";
        }
    }
}
