using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class UnreadMessagesResultDataModel : BaseDataModel
    {
        public MessageDataModel[] Messages { get; set; }
    }
}
