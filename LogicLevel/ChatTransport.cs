using LogicLevel.ChatServiceReference;
using LogicLevel.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public class ChatTransport : IChatTransport
    {
        private readonly IChatService _proxyChat;
        private readonly IChatServiceCallbackEvent _chatServiceCallbackEvent;

        public ChatTransport(IChatService chatService, IChatServiceCallbackEvent chatServiceCallbackEvent)
        {
            _proxyChat = chatService;
            _chatServiceCallbackEvent = chatServiceCallbackEvent;
        }

        public event EventHandler<UserJoinedEventArgs> OnUserJoined
        {
            add { _chatServiceCallbackEvent.OnUserJoined += value; }
            remove { _chatServiceCallbackEvent.OnUserJoined -= value; }
        }

        public event EventHandler<UserLeavedEventArgs> OnUserLeave
        {
            add { _chatServiceCallbackEvent.OnUserLeave += value; }
            remove { _chatServiceCallbackEvent.OnUserLeave -= value; }
        }

        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived
        {
            add { _chatServiceCallbackEvent.OnMessageReceived += value; }
            remove { _chatServiceCallbackEvent.OnMessageReceived -= value; }
        }

        public event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived
        {
            add { _chatServiceCallbackEvent.OnlineUsersReceived += value; }
            remove { _chatServiceCallbackEvent.OnlineUsersReceived -= value; }
        }

        public event EventHandler<CurrentUserEventArgs> CurrentUserReceived
        {
            add { _chatServiceCallbackEvent.CurrentUserReceived += value; }
            remove { _chatServiceCallbackEvent.CurrentUserReceived -= value; }
        }

        public event EventHandler<UnreadMessagesEventArgs> UnreadMessagesReceived
        {
            add { _chatServiceCallbackEvent.UnreadMessagesReceived += value; }
            remove { _chatServiceCallbackEvent.UnreadMessagesReceived -= value; }
        }

        public event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser
        {
            add { _chatServiceCallbackEvent.OnAddBlackListByUser += value; }
            remove { _chatServiceCallbackEvent.OnAddBlackListByUser -= value; }
        }

        public event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser
        {
            add { _chatServiceCallbackEvent.OnRemoveBlackListByUser += value; }
            remove { _chatServiceCallbackEvent.OnRemoveBlackListByUser -= value; }
        }


        public LoginResultTransportModel Login(LoginTransportModel obj)
        {
            return _proxyChat.Login(obj);
        }

        public void Logout(LogoutTransportModel obj)
        {
            _proxyChat.Logout(obj);
        }

        public RegistrationResultTransportModel Registration(RegistrationTransportModel obj)
        {
            return _proxyChat.Registration(obj);
        }

        public void SendMessage(MessagePartialTransportModel obj)
        {
            _proxyChat.SendMessage(obj);
        }

        public void AddBlackList(RelationTransportModel obj)
        {
            _proxyChat.AddBlackList(obj);
        }

        public void RemoveBlackList(RelationTransportModel obj)
        {
            _proxyChat.RemoveBlackList(obj);
        }
    }
}
