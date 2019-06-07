using LogicLevel.ChatServiceReference;
using LogicLevel.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IChatTransport
    {
        event EventHandler<UserJoinedEventArgs> OnUserJoined;

        event EventHandler<UserLeavedEventArgs> OnUserLeave;

        event EventHandler<MessageReceivedEventArgs> OnMessageReceived;

        event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived;

        event EventHandler<CurrentUserEventArgs> CurrentUserReceived;

        event EventHandler<UnreadMessagesEventArgs> UnreadMessagesReceived;

        event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser;

        event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser;

        LoginResultTransportModel Login(LoginTransportModel obj);

        void Logout(LogoutTransportModel obj);

        RegistrationResultTransportModel Registration(RegistrationTransportModel obj);

        void SendMessage(MessagePartialTransportModel obj);

        void AddBlackList(RelationTransportModel obj);

        void RemoveBlackList(RelationTransportModel obj);
    }
}
