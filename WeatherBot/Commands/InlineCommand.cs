using System.IO;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace WeatherBot.Commands
{
    class InlineCommand
    {
        public static string Name => "/inline";

        public static async void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!Name.Equals(e?.Message?.Text))
            {
                return;
            }
            using (StreamReader streamReader = new StreamReader(@"C:[path].TXT", Encoding.Default))
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     },
                     new[]
                     {
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync()),
                         InlineKeyboardButton.WithCallbackData(await streamReader.ReadLineAsync())
                     }
                });

                await Bot.client.SendTextMessageAsync(e?.Message?.Chat?.Id, "Прогноз погоды в столицах Европы:",
                    replyMarkup: inlineKeyboard);
            }
        }
    }
}
