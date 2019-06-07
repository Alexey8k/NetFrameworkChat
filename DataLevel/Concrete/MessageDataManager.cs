using DataLevel.Abstract;
using DataLevel.Mapping;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Concrete
{
    public class MessageDataManager : IMessageDataManager
    {
        public void AddMessage(MessagePartialDataModel message)
        {
            using (var chatDb = new ChatDbEntities()) 
            {
                chatDb.AddMessage(message.Text,
                    message.UserId,
                    (int)message.Status,
                    string.Join(",", message.UsersId));
            }
        }

        public UnreadMessagesResultDataModel GetUnreadMessages(LoginSuccessDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                return new UnreadMessagesResultDataModel
                {
                    Messages = chatDb.GetUnreadMessages(obj.UserId).ToArray()
                    .Mapping<MessageDataModel>()
                };
            }
        }
    }
}
