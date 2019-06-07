using AutoMapper;
using ChatService.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Mapping
{
    public static class BaseTransportModelExtension
    {
        public static T Mapping<T>(this BaseTransportModel obj)
        {
            return Mapper.Map<BaseTransportModel, T>(obj);
        }
    }
}
