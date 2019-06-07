using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLevel.Mapping;
using DataLevel.Abstract;

namespace DataLevel.Concrete
{
    public class UserDataManager : IUserDataManager
    {
        private byte[] _hash;

        private byte[] Hash
        {
            set { if (_hash == null) _hash = value; }
        }

        private UserPartialDataModel _currentUser;

        private UserCollectionDataModel _onlineUsers;

        private UserPartialDataModel CurrentUser => _currentUser
            ?? (_currentUser = new Func<UserPartialDataModel>(() =>
            {
                using (var _chatDb = new ChatDbEntities())
                {
                    return _chatDb.GetCurrentUser(_hash).FirstOrDefault().Mapping();
                }
            })());

        private UserCollectionDataModel OnlineUsers 
            => _onlineUsers ?? (_onlineUsers = new Func<UserCollectionDataModel>(() =>
        {
            using (var _chatDb = new ChatDbEntities())
            {
                return new UserCollectionDataModel()
                {
                    Users = _chatDb.GetOnlineUsers(CurrentUser.Id).ToArray()
                    .Mapping<OnlineUserDataModel>()
                };
            }
        })());

        public UserPartialDataModel GetCurrentUser(LoginDataModel obj)
        {
            Hash = obj.Hash;
            return CurrentUser;
        }

        public UserCollectionDataModel GetOnlineUsers(LoginDataModel obj)
        {
            Hash = obj.Hash;
            return OnlineUsers;
        }
    }
}
