using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class RegistrationModel : BaseSecureModel
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public int Color32 { get; set; }
    }
}
