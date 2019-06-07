using ChatService.DataContract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class UserPartialTransportModel : BaseTransportModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public Color Color { get; set; }

        [DataMember]
        public byte RoleId { get; set; }
    }
}
