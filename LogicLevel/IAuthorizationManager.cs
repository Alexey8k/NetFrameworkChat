using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public interface IAuthorizationManager
    {
        Task<LoginResultModel> Login(LoginModel obj);

        void Logout(LogoutModel obj);

        Task<RegistrationResultModel> Registration(RegistrationModel obj);
    }
}
