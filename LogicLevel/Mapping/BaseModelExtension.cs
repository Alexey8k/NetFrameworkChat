using AutoMapper;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Mapping
{
    public static class BaseModelExtension
    {
        public static T Mapping<T>(this BaseModel obj)
        {
            return Mapper.Map<BaseModel, T>(obj);
        }
    }
}
