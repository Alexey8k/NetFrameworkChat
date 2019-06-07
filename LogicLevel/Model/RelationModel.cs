using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class RelationModel : BaseModel
    {
        public int InitiatorUserId { get; set; }

        public int[] RelationUsersId { get; set; }
    }
}
