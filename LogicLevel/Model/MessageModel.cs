﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Model
{
    public class MessageModel : BaseModel
    {
        public string Login { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public MessageStatus Status { get; set; }
    }
}
