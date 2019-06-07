using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.EventArg
{
    public class BlackListByUserEventArgs : EventArgs
    {
        public int InitiatorUserId { get; set; }
    }
}
