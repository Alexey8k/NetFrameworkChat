using AutoMapper;
using ChatClient.ViewModel;
using LogicLevel.EventArg;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Mapping
{
    public class ChatClientProfile : Profile
    {
        public ChatClientProfile()
        {
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<RegistrationViewModel, RegistrationModel>();
            CreateMap<ChatViewModel, LogoutModel>();
            CreateMap<CurrentUserEventArgs, UserPartialModel>();
            CreateMap<UserJoinedEventArgs, UserPartialModel >();
            CreateMap<UserLeavedEventArgs, UserPartialModel>()
                .ForMember(u => u.Id, opt => opt.MapFrom(ulea => ulea.UserId));
            CreateMap<ChatViewModel, MessagePartialModel>()
                .ForMember(to => to.Text, opt => opt.MapFrom(from => from.MessageText))
                .ForMember(to => to.Status, opt => opt.MapFrom(from => from.MessageStatus));
            CreateMap<ChatViewModel, RelationModel>()
                .ForMember(to => to.InitiatorUserId, opt => opt.MapFrom(from => from.UserId))
                .ForMember(to => to.RelationUsersId, opt => opt.MapFrom(from => from.BlackListUsersId ?? from.UsersId));
        }
    }
}
