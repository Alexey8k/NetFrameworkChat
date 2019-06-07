using LogicLevel.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public class UserManager : IUserManager
    {
        private readonly IChatTransport _chatTransport;

        public UserManager(IChatTransport chatTransport)
        {
            _chatTransport = chatTransport;
        }

        public event EventHandler<UserJoinedEventArgs> OnUserJoined
        {
            add { _chatTransport.OnUserJoined += value; }
            remove { _chatTransport.OnUserJoined -= value; }
        }

        public event EventHandler<UserLeavedEventArgs> OnUserLeave
        {
            add { _chatTransport.OnUserLeave += value; }
            remove { _chatTransport.OnUserLeave -= value; }
        }

        public event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived
        {
            add { _chatTransport.OnlineUsersReceived += value; }
            remove { _chatTransport.OnlineUsersReceived -= value; }
        }

        public event EventHandler<CurrentUserEventArgs> CurrentUserReceived
        {
            add { _chatTransport.CurrentUserReceived += value; }
            remove { _chatTransport.CurrentUserReceived -= value; }
        }
    }
}
