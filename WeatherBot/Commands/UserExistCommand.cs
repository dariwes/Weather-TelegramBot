using System.Collections.Generic;

namespace WeatherBot.Commands
{
    class UserExistCommand
    {
        public static List<User> users = new List<User>();

        public static void Execute(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            foreach (var user in users)
            {
                if (e?.Message?.Chat?.Id == user.Id)
                {
                    return;
                }
            }

            users.Add(new User
                    {
                        Id = e.Message.Chat.Id,
                        FirstName = e.Message.Chat.FirstName,
                        LastName = e.Message.Chat.LastName
                    });
        }

        public static User GetUser(long id)
        {
            foreach (var user in users)
            {
                if (id == user.Id)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
