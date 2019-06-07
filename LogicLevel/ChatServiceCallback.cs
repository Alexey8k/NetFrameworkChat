using LogicLevel.ChatServiceReference;
using LogicLevel.EventArg;
using LogicLevel.Mapping;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatServiceCallback : IChatServiceCallback, IChatServiceCallbackEvent
    {
        public event EventHandler<UserJoinedEventArgs> OnUserJoined;

        public event EventHandler<UserLeavedEventArgs> OnUserLeave;

        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        public event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived;

        public event EventHandler<CurrentUserEventArgs> CurrentUserReceived;

        public event EventHandler<UnreadMessagesEventArgs> UnreadMessagesReceived;

        public event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser;

        public event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser;

        public void UserJoin(OnlineUserTransportModel obj)
        {
            OnUserJoined?.Invoke(this, obj.Mapping<UserJoinedEventArgs>());
        }

        public void UserLeave(UserLeavedTransportModel obj)
        {
            OnUserLeave?.Invoke(this, obj.Mapping<UserLeavedEventArgs>());
        }

        public void MessageReceived(MessagePartialTransportModel obj)
        {
            OnMessageReceived?.Invoke(this, new MessageReceivedEventArgs
            {
                Message = obj.Mapping<MessagePartialModel>()
            });
        }

        public void OnlineUsers(UserCollectionTransportModel obj)
        {
            OnlineUsersReceived?.Invoke(this, obj.Mapping<OnlineUsersEventArgs>());
        }

        public void CurrentUser(UserPartialTransportModel obj)
        {
            CurrentUserReceived?.Invoke(this, obj.Mapping<CurrentUserEventArgs>());
        }

        public void UnreadMessages(UnreadMessagesTransportModel obj)
        {
            UnreadMessagesReceived?.Invoke(this, obj.Mapping<UnreadMessagesEventArgs>());
        }

        public void AddBlackListByUser(BlackListByUserTransportModel obj)
        {
            OnAddBlackListByUser?.Invoke(this, obj.Mapping<BlackListByUserEventArgs>());
        }

        public void RemoveBlackListByUser(BlackListByUserTransportModel obj)
        {
            OnRemoveBlackListByUser?.Invoke(this, obj.Mapping<BlackListByUserEventArgs>());
        }
    }
}
