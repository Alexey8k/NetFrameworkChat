using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Model
{
    public class MessageDataModel : BaseDataModel
    {
        public string Login { get; set; }
        public string Text { get; set; }
        public MessageStatusData Status { get; set; }
        public DateTime Date { get; set; }
    }
}
