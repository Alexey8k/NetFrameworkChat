using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class BlackListByUserTransportModel : BaseTransportModel
    {
        [DataMember]
        public int InitiatorUserId { get; set; }
    }
}
