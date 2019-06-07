using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class MessagePartialDataModel : BaseDataModel
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public MessageStatusData Status { get; set; }
        public int[] UsersId { get; set; }
    }
}
