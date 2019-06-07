using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class RelationDataModel : BaseDataModel
    {
        public int InitiatorUserId { get; set; }

        public int[] RelationUsersId { get; set; }
    }
}
