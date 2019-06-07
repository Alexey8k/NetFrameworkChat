using ChatService.DataContract;
using ChatService.Mapping;
using DataLevel.Abstract;
using DataLevel.Mapping;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, 
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        private readonly IChatDb _chatDb;

        private readonly IMessenger _messenger;

        public ChatService(IChatDb chatDb, IMessenger messenger)
        {
            AutoMapperAppConfiguration.Configure();
            _chatDb = chatDb;
            _messenger = messenger;
        }

        public LoginResultTransportModel Login(LoginTransportModel obj)
        {
            var resultLogin = _chatDb.Login(obj.Mapping<LoginDataModel>())
                .Mapping<LoginResultTransportModel>();
            if (resultLogin.Result == LoginResultTransport.Ok)
            {
                _messenger.UserJoin(obj);
            }
            return resultLogin;
        }

        public void Logout(LogoutTransportModel obj)
        {
            _chatDb.Logout(obj.Mapping<LogoutDataModel>());
            _messenger.UserLeave(obj.Mapping<UserLeavedTransportModel>());
        }

        public void SendMessage(MessagePartialTransportModel obj)
        {
            _chatDb.AddMessage(obj.Mapping<MessagePartialDataModel>());
            _messenger.SendMessage(obj);
        }

        public RegistrationResultTransportModel Registration(RegistrationTransportModel obj)
        {
            return _chatDb.Registration(obj.Mapping<RegistrationDataModel>())
                .Mapping<RegistrationResultTransportModel>();
        }

        public void AddBlackList(RelationTransportModel obj)
        {
            _chatDb.AddBlackList(obj.Mapping<RelationDataModel>());
            _messenger.AddBlackListByUser(obj);
        }

        public void RemoveBlackList(RelationTransportModel obj)
        {
            _chatDb.RemoveBlackList(obj.Mapping<RelationDataModel>());
            _messenger.RemoveBlackListByUser(obj);
        }
    }
}
