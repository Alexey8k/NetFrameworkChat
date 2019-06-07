using ChatService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    [ServiceContract(CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false)]
        LoginResultTransportModel Login(LoginTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void Logout(LogoutTransportModel obj);

        [OperationContract(IsOneWay = false)]
        RegistrationResultTransportModel Registration(RegistrationTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void SendMessage(MessagePartialTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void AddBlackList(RelationTransportModel obj);

        [OperationContract(IsOneWay = true)]
        void RemoveBlackList(RelationTransportModel obj);
    }
}
