using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.EventArg
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessagePartialModel Message { get; set; }
    }
}
