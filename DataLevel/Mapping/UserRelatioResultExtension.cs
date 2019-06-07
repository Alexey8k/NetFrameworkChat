using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public static class UserRelatioResultExtension
    {
        public static T Mapping<T>(this UserRelatioResult obj)
        {
            return Mapper.Map<UserRelatioResult, T>(obj);
        }
    }
}
