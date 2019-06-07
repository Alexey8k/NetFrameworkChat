using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Mapping
{
    public static class EventArgsExtension
    {
        public static T Mapping<T>(this EventArgs obj)
        {
            return Mapper.Map<EventArgs, T>(obj);
        }
    }
}
