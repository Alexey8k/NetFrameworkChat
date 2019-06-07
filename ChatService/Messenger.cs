using ChatService.DataContract;
using ChatService.Mapping;
using DataLevel.Abstract;
using DataLevel.Mapping;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatService
{
    public class Messenger : IMessenger
    {
        private readonly IChatDb _chatDb;

        private readonly Dictionary<int, IChatCallback> _onlineUserCallbacks
            = new Dictionary<int, IChatCallback>();

        public Messenger(IChatDb chatDb)
        {
            _chatDb = chatDb;
        }

        public void UserJoin(LoginTransportModel obj)
        {
            //var callbacks = _onlineUsers.Values.Select(u => u.Callback).ToList();
            var currentUser = _chatDb.GetCurrentUser(obj.Mapping<LoginDataModel>()).Mapping<UserPartialTransportModel>();
            var currentCallback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

            currentCallback.OnlineUsers(_chatDb.GetOnlineUsers(obj.Mapping<LoginDataModel>()).Mapping<UserCollectionTransportModel>());
            currentCallback.CurrentUser(currentUser);
            currentCallback
                .UnreadMessages(_chatDb.GetUnreadMessages(currentUser.Mapping<LoginSuccessDataModel>())
                .Mapping<UnreadMessagesTransportModel>());

            //_onlineUserCallbacks.Values.ForEach(u => u.UserJoin(currentUser));
            foreach (var pair in _onlineUserCallbacks)
            {
                var joinUser = currentUser.Mapping<OnlineUserTransportModel>();
                joinUser.Relation = (UserRelationTransport)_chatDb
                    .GetUserRelation(new UserRelationDataModel
                {
                    CurrentUserId = pair.Key,
                    RelationUserId = currentUser.Id
                }).Relation;
                pair.Value.UserJoin(joinUser);
            }

            _onlineUserCallbacks.Add(currentUser.Id, currentCallback);
        }

        public void UserLeave(UserLeavedTransportModel obj)
        {
            _onlineUserCallbacks.Remove(obj.UserId);
            foreach (var callback in _onlineUserCallbacks.Values) callback.UserLeave(obj);
        }

        public void SendMessage(MessagePartialTransportModel obj)
        {
            if (obj.Status == MessageStatusTransport.Share)
                _onlineUserCallbacks
                    .Where(pair => pair.Key != obj.UserId)
                    .Select(pair => pair.Value)
                    .ToList()
                    .ForEach(c => c.MessageReceived(obj));
            _onlineUserCallbacks
                .Where(pair => obj.UsersId.Contains(pair.Key))
                .Select(pair => pair.Value)
                .ToList()
                .ForEach(c => c.MessageReceived(obj));
        }

        public void AddBlackListByUser(RelationTransportModel obj)
        {
            _onlineUserCallbacks
                .Where(pair => obj.RelationUsersId.Contains(pair.Key))
                .Select(pair => pair.Value)
                .ToList().ForEach(c 
                => c.AddBlackListByUser(obj.Mapping<BlackListByUserTransportModel>()));
        }

        public void RemoveBlackListByUser(RelationTransportModel obj)
        {
            _onlineUserCallbacks
                .Where(pair => obj.RelationUsersId.Contains(pair.Key))
                .Select(pair => pair.Value)
                .ToList().ForEach(c
                => c.RemoveBlackListByUser(obj.Mapping<BlackListByUserTransportModel>()));
        }
    }
}
