using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public abstract class BaseSecureModel : BaseModel
    {
        public string Login { get; set; }

        public SecureString SecurePassword { get; set; } 
    }
}
