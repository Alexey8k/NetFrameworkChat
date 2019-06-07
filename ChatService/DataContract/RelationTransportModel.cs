using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.DataContract
{
    [DataContract]
    public class RelationTransportModel : BaseTransportModel
    {
        [DataMember]
        public int InitiatorUserId { get; set; }

        [DataMember]
        public int[] RelationUsersId { get; set; }
    }
}
