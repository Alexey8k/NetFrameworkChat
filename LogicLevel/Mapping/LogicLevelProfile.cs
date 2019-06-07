using AutoMapper;
using LogicLevel.ChatServiceReference;
using LogicLevel.EventArg;
using LogicLevel.Infrastructure;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Mapping
{
    public class LogicLevelProfile : Profile
    {
        public LogicLevelProfile()
        {
            CreateMap<LoginModel, LoginTransportModel>();
            CreateMap<LoginResultTransportModel, LoginResultModel>();
            CreateMap<LogoutModel, LogoutTransportModel>();
            CreateMap<MessagePartialModel, MessagePartialTransportModel>();
            CreateMap<MessagePartialTransportModel, MessagePartialModel>();

            CreateMap<UserPartialTransportModel, CurrentUserEventArgs>();
            CreateMap<OnlineUserTransportModel, OnlineUserModel>();
            CreateMap<UserCollectionTransportModel, OnlineUsersEventArgs>();

            CreateMap<RegistrationModel, RegistrationTransportModel>().AfterMap<SecureAfterMapAction>();  //.ForMember(
                //arg => arg.Hash, options => options.MapFrom(model => new HashSHA1().GetHash(model.Login, model.Password)));
            CreateMap<RegistrationResultTransportModel, RegistrationResultModel>();

            CreateMap<MessageTransportModel, MessageModel>();
            CreateMap<UnreadMessagesTransportModel, UnreadMessagesEventArgs>();

            CreateMap<UserLeavedTransportModel, UserLeavedEventArgs>();
            CreateMap<UserLeavedTransportModel, CurrentUserEventArgs>();
            CreateMap<BlackListByUserTransportModel, BlackListByUserEventArgs>();
            CreateMap<RelationModel, RelationTransportModel>();
        }
    }
}
