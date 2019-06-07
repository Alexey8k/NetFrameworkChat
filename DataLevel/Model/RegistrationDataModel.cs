using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class RegistrationDataModel : BaseDataModel
    {
        public byte[] Hash { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public byte? RoleId { get; set; }
        public int Color32 { get; set; }
    }
}
