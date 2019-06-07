using ChatClient.ViewModel;
using LogicLevel;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Infrastructure
{
    public class ConfigChatClientModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ChatViewModel>().ToSelf();
            Bind<LoginViewModel>().ToSelf();
            Bind<RegistrationViewModel>().ToSelf();
            
            Bind<IChatClient>().To<LogicLevel.ChatClient>().InSingletonScope();
        }
    }
}
