using System;
using System.IO;

namespace WeatherBot.Commands
{
    class WriteToFileCommand
    {
        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var user = UserExistCommand.GetUser(e.Message.From.Id);
            var message = $"{DateTime.Now}. {e?.Message?.Text} от {user.FirstName} {user.LastName}.";
            var writePath = @"C:[path].TXT";

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
