using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.EventArg
{
    public class OnlineUsersEventArgs : EventArgs
    {
        public OnlineUserModel[] Users { get; set; }
    }
}
