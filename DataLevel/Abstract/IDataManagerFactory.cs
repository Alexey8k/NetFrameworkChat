using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Abstract
{
    public interface IDataManagerFactory
    {
        IAuthorizationDataManager CreateAuthorizationDataManager();
        IUserDataManager CreateUserDataManager();
        IMessageDataManager CreateMessageDataManager();
        IRelationDataManager CreateRelationDataManager();
    }
}
