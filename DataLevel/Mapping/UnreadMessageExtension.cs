using AutoMapper;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public static class UnreadMessageExtension
    {
        public static T[] Mapping<T>(this UnreadMessage[] obj)
        {
            return Mapper.Map<UnreadMessage[], T[]>(obj);
        }
    }
}
