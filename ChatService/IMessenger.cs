using ChatService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public interface IMessenger
    {
        void UserJoin(LoginTransportModel obj);

        void UserLeave(UserLeavedTransportModel obj);

        void SendMessage(MessagePartialTransportModel obj);

        void AddBlackListByUser(RelationTransportModel obj);

        void RemoveBlackListByUser(RelationTransportModel obj);
    }
}
