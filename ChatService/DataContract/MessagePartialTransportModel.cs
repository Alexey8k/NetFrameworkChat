using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class MessagePartialTransportModel : BaseTransportModel
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public MessageStatusTransport Status { get; set; }

        [DataMember]
        public int[] UsersId { get; set; }
    }
}
