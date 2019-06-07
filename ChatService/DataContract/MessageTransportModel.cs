using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
   [DataContract]
   public class MessageTransportModel : BaseTransportModel
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public MessageStatusTransport Status { get; set; }
    }
}
