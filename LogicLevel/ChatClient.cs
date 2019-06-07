using LogicLevel.EventArg;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel
{
    public class ChatClient : IChatClient
    {
        private readonly IAuthorizationManager _authorizationManager;

        private readonly IUserManager _userManager;

        private readonly IMessageManager _messageManager;

        private readonly IRelationManager _relationManager;

        public ChatClient(IAuthorizationManager authorizationManager, 
            IUserManager userManager, 
            IMessageManager messageManager,
            IRelationManager relationManager)
        {
            _authorizationManager = authorizationManager;
            _userManager = userManager;
            _messageManager = messageManager;
            _relationManager = relationManager;
        }

        public event EventHandler<BlackListByUserEventArgs> OnRemoveBlackListByUser
        {
            add { _relationManager.OnRemoveBlackListByUser += value; }
            remove { _relationManager.OnRemoveBlackListByUser -= value; }
        }

        public event EventHandler<BlackListByUserEventArgs> OnAddBlackListByUser
        {
            add { _relationManager.OnAddBlackListByUser += value; }
            remove { _relationManager.OnAddBlackListByUser -= value; }
        }

        public event EventHandler<UserJoinedEventArgs> OnUserJoined
        {
            add { _userManager.OnUserJoined += value; }
            remove { _userManager.OnUserJoined -= value; }
        }
        public event EventHandler<UserLeavedEventArgs> OnUserLeave
        {
            add { _userManager.OnUserLeave += value; }
            remove { _userManager.OnUserLeave -= value; }
        }
        public event EventHandler<MessageReceivedEventArgs> OnMessageReceived
        {
            add { _messageManager.OnMessageReceived += value; }
            remove { _messageManager.OnMessageReceived -= value; }
        }
        public event EventHandler<OnlineUsersEventArgs> OnlineUsersReceived
        {
            add { _userManager.OnlineUsersReceived += value; }
            remove { _userManager.OnlineUsersReceived -= value; }
        }
        public event EventHandler<CurrentUserEventArgs> CurrentUserReceived
        {
            add { _userManager.CurrentUserReceived += value; }
            remove { _userManager.CurrentUserReceived -= value; }
        }
        public event EventHandler<UnreadMessagesEventArgs> UnreadMessagesReceived
        {
            add { _messageManager.UnreadMessagesReceived += value; }
            remove { _messageManager.UnreadMessagesReceived -= value; }
        }

        public void AddBlackList(RelationModel obj)
        {
            _relationManager.AddBlackList(obj);
        }

        public async Task<LoginResultModel> Login(LoginModel obj)
        {
            return await _authorizationManager.Login(obj);
        }

        public void Logout(LogoutModel obj)
        {
            _authorizationManager.Logout(obj);
        }

        public async Task<RegistrationResultModel> Registration(RegistrationModel obj)
        {
            return await _authorizationManager.Registration(obj);
        }

        public void RemoveBlackList(RelationModel obj)
        {
            _relationManager.RemoveBlackList(obj);
        }

        public void SendMessage(MessagePartialModel obj)
        {
            _messageManager.SendMessage(obj);
        }
    }
}
