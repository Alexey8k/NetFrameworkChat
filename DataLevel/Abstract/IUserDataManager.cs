using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Abstract
{
    public interface IUserDataManager
    {
        UserPartialDataModel GetCurrentUser(LoginDataModel obj);

        UserCollectionDataModel GetOnlineUsers(LoginDataModel obj);

    }
}
