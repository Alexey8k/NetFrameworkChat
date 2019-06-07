using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public enum MessageStatusTransport
    {
        [EnumMember]
        Private = 1,

        [EnumMember]
        Friend,

        [EnumMember]
        Share
    }
}
