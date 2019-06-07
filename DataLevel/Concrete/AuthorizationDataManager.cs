using DataLevel.Abstract;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Concrete
{
    public class AuthorizationDataManager : IAuthorizationDataManager
    {
        public LoginResultDataModel Login(LoginDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                return new LoginResultDataModel
                {
                    Result = (LoginResaltData)chatDb.Login(obj.Hash).FirstOrDefault()
                };
            }            
        }
        public void Logout(LogoutDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                chatDb.Logout(obj.UserId);
            }
        }
        public RegistrationResultDataModel Registration(RegistrationDataModel obj)
        {
            using (var chatDb = new ChatDbEntities())
            {
                return new RegistrationResultDataModel
                {
                    Result = (RegistrationResultData)chatDb
                        .Registration(obj.Login, obj.Hash, obj.Email, obj.Color32, obj.RoleId)
                        .FirstOrDefault()
                };
            }
        }
    }
}
