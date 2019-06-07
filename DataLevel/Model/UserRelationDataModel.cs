using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class UserRelationDataModel : BaseDataModel
    {
        public int CurrentUserId { get; set; }

        public int RelationUserId { get; set; }
    }
}
