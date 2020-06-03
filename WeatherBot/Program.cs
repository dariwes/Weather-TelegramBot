﻿using System;
using WeatherBot.Commands;

namespace WeatherBot
{
    class Program
    {
        public static void Main()
        {
            Bot.Translate();
            Bot.BotSetUp();
            Bot.Start();
            Console.ReadKey();
            Bot.Stop();
        }
    }
}
