using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Infrastructure
{
    public class ConfigChatServiceModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<ChatService>().ToSelf();
            Bind<IMessenger>().To<Messenger>();
        }
    }
}
