using AutoMapper;
using ChatService.DataContract;
using DataLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Mapping
{
    public class ChatServiceProfile : Profile
    {
        public ChatServiceProfile()
        {
            CreateMap<LoginResultDataModel, LoginResultTransportModel>();
            CreateMap<LoginTransportModel, LoginDataModel>();
            CreateMap<OnlineUserDataModel, OnlineUserTransportModel>();
            CreateMap<LogoutTransportModel, LogoutDataModel>();
            CreateMap<UserPartialTransportModel, OnlineUserTransportModel>();
            CreateMap<UserPartialTransportModel, LoginSuccessDataModel>()
                .ForMember(to => to.UserId, opt => opt.MapFrom(from => from.Id));
            CreateMap<UserCollectionDataModel, UserCollectionTransportModel>();
            CreateMap<MessagePartialTransportModel, MessagePartialDataModel>();
            CreateMap<MessageDataModel, MessageTransportModel>();
            CreateMap<UnreadMessagesResultDataModel, UnreadMessagesTransportModel>();
            CreateMap<RelationTransportModel, RelationDataModel> ();
            CreateMap<RelationTransportModel, BlackListByUserTransportModel>();

        }
    }
}
