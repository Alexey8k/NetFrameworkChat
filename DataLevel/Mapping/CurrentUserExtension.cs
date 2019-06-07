using AutoMapper;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public static class CurrentUserExtension
    {
        public static UserPartialDataModel Mapping(this CurrentUser obj)
        {
            return Mapper.Map<CurrentUser, UserPartialDataModel>(obj);
        }
    }
}
