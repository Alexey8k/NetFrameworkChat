using ChatService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void UserJoin(OnlineUserTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void UserLeave(UserLeavedTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void MessageReceived(MessagePartialTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void OnlineUsers(UserCollectionTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void CurrentUser(UserPartialTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void UnreadMessages(UnreadMessagesTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void AddBlackListByUser(BlackListByUserTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void RemoveBlackListByUser(BlackListByUserTransportModel obj);
    }
}
