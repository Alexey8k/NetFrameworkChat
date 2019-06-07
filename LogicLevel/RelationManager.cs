using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLevel.ChatServiceReference;
using LogicLevel.EventArg;
using LogicLevel.Mapping;
using LogicLevel.Model;

namespace LogicLevel
{
    public class RelationManager : IRelationManager
    {
        private readonly IChatTransport _chatTransport;

        public RelationManager(IChatTransport chatTransport)
        {
            _chatTransport = chatTransport;
        }

        public event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser
        {
            add { _chatTransport.OnRemoveBlackListByUser += value; }
            remove { _chatTransport.OnRemoveBlackListByUser -= value; }
        }

        public event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser
        {
            add { _chatTransport.OnAddBlackListByUser += value; }
            remove { _chatTransport.OnAddBlackListByUser -= value; }
        }

        public void AddBlackList(RelationModel obj)
        {
            _chatTransport.AddBlackList(obj.Mapping<RelationTransportModel>());
        }

        public void RemoveBlackList(RelationModel obj)
        {
            _chatTransport.RemoveBlackList(obj.Mapping<RelationTransportModel>());
        }
    }
}
