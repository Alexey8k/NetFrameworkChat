using LogicLevel.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IUserManager
    {
        event EventHandler<UserJoinedEventArgs> OnUserJoined;

        event EventHandler<UserLeavedEventArgs> OnUserLeave;

        event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived;

        event EventHandler<CurrentUserEventArgs> CurrentUserReceived;
    }
}
