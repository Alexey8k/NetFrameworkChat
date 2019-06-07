using DataLevel.Abstract;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Concrete
{
    public class ChatDb : IChatDb
    {
        private readonly IDataManagerFactory _dataManagerFactory;

        public ChatDb(IDataManagerFactory dataManagerFactory)
        {
            _dataManagerFactory = dataManagerFactory;
        }

        public void AddBlackList(RelationDataModel obj)
        {
            _dataManagerFactory.CreateRelationDataManager().AddBlackList(obj);
        }

        public void AddFriend(RelationDataModel obj)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(MessagePartialDataModel obj)
        {
            _dataManagerFactory.CreateMessageDataManager().AddMessage(obj);
        }

        public UserPartialDataModel GetCurrentUser(LoginDataModel obj)
        {
            return _dataManagerFactory.CreateUserDataManager().GetCurrentUser(obj);
        }

        public UserCollectionDataModel GetOnlineUsers(LoginDataModel obj)
        {
            return _dataManagerFactory.CreateUserDataManager().GetOnlineUsers(obj);
        }

        public UnreadMessagesResultDataModel GetUnreadMessages(LoginSuccessDataModel obj)
        {
            return _dataManagerFactory.CreateMessageDataManager().GetUnreadMessages(obj);
        }

        public UserRelationResultDataModel GetUserRelation(UserRelationDataModel obj)
        {
            return _dataManagerFactory.CreateRelationDataManager().GetUserRelation(obj);
        }

        public LoginResultDataModel Login(LoginDataModel obj)
        {
            return _dataManagerFactory.CreateAuthorizationDataManager().Login(obj);
        }

        public void Logout(LogoutDataModel obj)
        {
            _dataManagerFactory.CreateAuthorizationDataManager().Logout(obj);
        }

        public RegistrationResultDataModel Registration(RegistrationDataModel obj)
        {
            return _dataManagerFactory.CreateAuthorizationDataManager().Registration(obj);
        }

        public void RemoveBlackList(RelationDataModel obj)
        {
            _dataManagerFactory.CreateRelationDataManager().RemoveBlackList(obj);
        }

        public void RemoveFriend(RelationDataModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
