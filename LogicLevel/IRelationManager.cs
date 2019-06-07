using LogicLevel.EventArg;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IRelationManager
    {
        event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser;

        event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser;

        void AddBlackList(RelationModel obj);

        void RemoveBlackList(RelationModel obj);
    }
}
