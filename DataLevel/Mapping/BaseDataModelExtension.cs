using AutoMapper;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public static class BaseDataModelExtension
    {
        public static T Mapping<T>(this BaseDataModel obj)
        {
            return Mapper.Map<BaseDataModel, T>(obj);
        }
    }
}
