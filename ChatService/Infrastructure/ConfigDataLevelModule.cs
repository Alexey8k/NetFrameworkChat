using DataLevel.Abstract;
using DataLevel.Concrete;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLevel;

namespace ChatService.Infrastructure
{
    public class ConfigDataLevelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserDataManager>().To<UserDataManager>();
            Bind<IAuthorizationDataManager>().To<AuthorizationDataManager>();
            Bind<IMessageDataManager>().To<MessageDataManager>();
            Bind<IRelationDataManager>().To<RelationDataManager>();

            Bind<IChatDb>().To<ChatDb>();

            Bind<IDataManagerFactory>().ToFactory();
        }
    }
}
