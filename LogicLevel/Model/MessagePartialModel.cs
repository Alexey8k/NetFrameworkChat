using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class MessagePartialModel : BaseModel
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public MessageStatus Status { get; set; }
        public int[] UsersId { get; set; }
    }
}
