using LogicLevel.EventArg;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IChatClient : 
        IAuthorizationManager, IUserManager, IMessageManager, IRelationManager
    {
    }
}
