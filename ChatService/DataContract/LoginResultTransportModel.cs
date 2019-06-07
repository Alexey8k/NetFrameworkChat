using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class LoginResultTransportModel : BaseTransportModel
    {
        [DataMember]
        public LoginResultTransport Result { get; set; }
    }
}
