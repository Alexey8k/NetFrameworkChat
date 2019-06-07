using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.EventArg
{
    public class CurrentUserEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Login { get; set; }
    }
}
