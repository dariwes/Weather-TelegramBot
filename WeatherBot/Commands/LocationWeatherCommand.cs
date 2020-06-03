using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Commands
{
    class LocationWeatherCommand 
    {
        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e?.Message?.Type != MessageType.Location)
            {
                return;
            }

            var user = UserExistCommand.GetUser(e.Message.Chat.Id);

            user.Latitude = e?.Message?.Location?.Latitude;
            user.Longitude = e?.Message?.Location?.Longitude;

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

            await Bot.client.SendTextMessageAsync(e?.Message?.From?.Id, "Узнать погоду или " +
                "получить график на 5 дней ( /chart ):", replyMarkup: replyKeyboard);
        }
    }
}
