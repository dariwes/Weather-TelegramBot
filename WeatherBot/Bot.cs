using Newtonsoft.Json;
using System;
using System.IO;
using Telegram.Bot;
using System.Net;
using WeatherBot.Commands;

namespace WeatherBot
{
    class Bot
    {
        public static readonly TelegramBotClient client = new TelegramBotClient(ReadTokenBot(@"[path].json"));

        public static float? Latitude = null;
        public static float? Longitude = null;

        private static string ReadTokenBot(string filePath)
        {
            TokenInfo jsonObj = JsonConvert.DeserializeObject<TokenInfo>(File.ReadAllText(filePath));
            return jsonObj.TokenBot;
        } 

        public static string ReadTokenOWM(string filePath)
        {
            TokenInfo jsonObj = JsonConvert.DeserializeObject<TokenInfo>(File.ReadAllText(filePath));
            return jsonObj.TokenOWM;
        }

        public static void Start()
        {
            client.StartReceiving();
        }
        public static void Stop()
        {
            client.StopReceiving();
        }

        public static void BotSetUp()
        {
            client.OnMessage += StartCommand.Execute;
            client.OnMessage += HelpCommand.Execute;
            client.OnMessage += KeyboardCommand.Execute;
            client.OnMessage += LocationWeatherCommand.Execute;
            client.OnMessage += CityWeatherCommand.Execute;
            client.OnMessage += GetLocationCommand.Execute;
            client.OnMessage += WriteToFileCommand.Execute;
        }

        public static void Translate()
        {
            string credentialPath = @"[path].json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
        }

        public static bool CheckUrl(string url)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string HTMLSource = wc.DownloadString(url);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
