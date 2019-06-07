using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class UserModel : BaseModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Hash { get; set; }
        public string Email { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastVisitDate { get; set; }
    }
}
