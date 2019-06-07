using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Abstract
{
    public interface IRelationDataManager
    {
        void AddFriend(RelationDataModel obj);

        void RemoveFriend(RelationDataModel obj);

        void AddBlackList(RelationDataModel obj);

        void RemoveBlackList(RelationDataModel obj);

        UserRelationResultDataModel GetUserRelation(UserRelationDataModel obj);
    }
}
