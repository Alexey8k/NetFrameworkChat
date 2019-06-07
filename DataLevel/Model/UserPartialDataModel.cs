using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class UserPartialDataModel : BaseDataModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public Color Color { get; set; }
        public byte RoleId { get; set; }
    }
}
