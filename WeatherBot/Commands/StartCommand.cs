﻿using Telegram.Bot.Types.Enums;


namespace WeatherBot.Commands
{
    class StartCommand 
    {
        public static string Name => "/start";

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!Name.Equals(e?.Message?.Text))
            {
                return;
            }
            
            var answer = "<b>Привет!</b>\n<i>С моей помощью ты можешь узнать погоду в любом городе. " +
                         "Для этого необходимо написать мне его название.\nЕще, отправив мне команду " +
                         "/inline , я покажу тебе погоду любой столицы Европы.\nТакже ты можешь узнать погоду <b>сейчас</b>, " +
                         "на <b>12 часов</b>, на <b>день</b> и на <b>три дня</b> с периодичностью <b>3 часа</b>, " +
                         "воспользовавшись командой /keyboard или температуру на <b>пять дней</b> в виде графика " +
                         "с помощью команды /chart \nНу, и просто можешь пообщаться со мной, если тебе скучно:)</i>";

            await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, answer, ParseMode.Html);
        }
    }
}
