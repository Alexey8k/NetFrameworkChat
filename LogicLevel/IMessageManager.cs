using LogicLevel.EventArg;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IMessageManager
    {
        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        event EventHandler<UnreadMessagesEventArgs> UnreadMessagesReceived;

        void SendMessage(MessagePartialModel obj);
    }
}
