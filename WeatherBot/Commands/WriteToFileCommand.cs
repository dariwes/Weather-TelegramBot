using System;
using System.IO;

namespace WeatherBot.Commands
{
    class WriteToFileCommand
    {
        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = $"{e?.Message?.Text} от {e?.Message?.From?.FirstName} {e?.Message?.From?.LastName}.";
            var writePath = @"[path].TXT";

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    await streamWriter.WriteLineAsync(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
