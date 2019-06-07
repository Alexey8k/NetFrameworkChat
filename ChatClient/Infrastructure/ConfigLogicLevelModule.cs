using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLevel.ChatServiceReference;
using System.ServiceModel;
using Ninject.Extensions.Wcf;
using LogicLevel;
using Ninject;
using LogicLevel.Infrastructure;
using AutoMapper;
using LogicLevel.Model;

namespace ChatClient.Infrastructure
{
    public class ConfigLogicLevelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAuthorizationManager>().To<AuthorizationManager>();
            Bind<IUserManager>().To<UserManager>();
            Bind<IMessageManager>().To<MessageManager>();
            Bind<IRelationManager>().To<RelationManager>();
            Bind<IHashAlgorithm>().To<HashSHA1>();
            Bind<IChatTransport>().To<ChatTransport>().InSingletonScope();

            Bind<IChatServiceCallback, IChatServiceCallbackEvent>().To<ChatServiceCallback>().InSingletonScope();
            //Bind<IChatServiceCallback>().To<ChatServiceCallback>();
            //Bind<IChatServiceCallbackEvent>().To<ChatServiceCallback>();
            Bind<IChatService>().ToMethod(c => new ChatServiceClient(new InstanceContext(c.Kernel.Get<IChatServiceCallback>())));
        }
    }
}
