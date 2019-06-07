using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Abstract
{
    public interface IAuthorizationDataManager
    {
        LoginResultDataModel Login(LoginDataModel obj);

        void Logout(LogoutDataModel obj);

        RegistrationResultDataModel Registration(RegistrationDataModel obj);
    }
}
