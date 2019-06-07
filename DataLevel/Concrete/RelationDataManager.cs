using DataLevel.Abstract;
using DataLevel.Mapping;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Concrete
{
    public class RelationDataManager : IRelationDataManager
    {
        public void AddBlackList(RelationDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                chatDb.AddBlackList(obj.InitiatorUserId, 
                    string.Join(",", obj.RelationUsersId));
            }
        }

        public UserRelationResultDataModel GetUserRelation(UserRelationDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                return chatDb.GetUserRelation(obj.CurrentUserId, obj.RelationUserId)
                    .FirstOrDefault().Mapping<UserRelationResultDataModel>();
            }
        }

        public void AddFriend(RelationDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                chatDb.AddFriend(obj.InitiatorUserId, 
                    string.Join(",", obj.RelationUsersId));
            }
        }

        public void RemoveBlackList(RelationDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                chatDb.RemoveBlackList(obj.InitiatorUserId, 
                    string.Join(",", obj.RelationUsersId));
            }
        }

        public void RemoveFriend(RelationDataModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
