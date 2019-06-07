using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class RegistrationTransportModel : BaseHashModel
    {
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public byte? RoleId { get; set; }
        [DataMember]
        public int Color32 { get; set; }
    }
}
