using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class UserCollectionModel : BaseModel
    {
        public UserPartialModel[] Users { get; set; }
    }
}
