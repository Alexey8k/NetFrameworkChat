using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Abstract
{
    public interface IMessageDataManager
    {
        void AddMessage(MessagePartialDataModel message);

        UnreadMessagesResultDataModel GetUnreadMessages(LoginSuccessDataModel obj);
    }
}
