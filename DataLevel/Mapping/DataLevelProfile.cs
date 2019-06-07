using AutoMapper;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel.Mapping
{
    public class DataLevelProfile : Profile
    {
        public DataLevelProfile()
        {
            CreateMap<CurrentUser, UserPartialDataModel>();
            CreateMap<OnlineUser, OnlineUserDataModel>();
            CreateMap<UnreadMessage, MessageDataModel>();
            CreateMap<UserRelatioResult, UserRelationResultDataModel>();
        }
    }
}
