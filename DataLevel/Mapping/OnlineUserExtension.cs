using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public static class OnlineUserExtension
    {
        public static T[] Mapping<T>(this OnlineUser[] obj)
        {
            return Mapper.Map<OnlineUser[], T[]>(obj);
        }
    }
}
