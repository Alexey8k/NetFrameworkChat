using AutoMapper;
using LogicLevel.ChatServiceReference;
using LogicLevel.Mapping;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IChatTransport _chatTransport;
        private readonly IHashAlgorithm _hashAlgorithm;
        private readonly IMapper _mapper;
        public AuthorizationManager(IChatTransport chatTransport, 
            IHashAlgorithm hashAlgorithm, IMapper mapper)
        {
            _chatTransport = chatTransport;
            _hashAlgorithm = hashAlgorithm;
            _mapper = mapper;
        }
        public async Task<LoginResultModel> Login(LoginModel obj)
        {
            return await Task.Run(() 
                => _chatTransport.Login(obj.Mapping<LoginTransportModel>())
                .Mapping<LoginResultModel>());
        }

        public void Logout(LogoutModel obj)
        {
            _chatTransport.Logout(obj.Mapping<LogoutTransportModel>());
        }

        public async Task<RegistrationResultModel> Registration(RegistrationModel obj)
        {
            //return await Task.Run(() => _chatTransport.Registration(obj.Mapping<RegistrationTransportModel>()).Mapping<RegistrationResultModel>());
            return await Task.Run(() 
                => _mapper.Map<RegistrationResultModel>(_chatTransport
                .Registration(_mapper.Map<RegistrationTransportModel>(obj))));
        }
    }
}
