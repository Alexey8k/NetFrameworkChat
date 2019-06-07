using AutoMapper;
using ChatClient.Mapping;
using LogicLevel.ChatServiceReference;
using LogicLevel.Infrastructure;
using LogicLevel.Mapping;
using LogicLevel.Model;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Infrastructure
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMappingAction<BaseSecureModel, BaseHashModel>>().To<SecureAfterMapAction>();

            Bind<MapperConfiguration>().ToConstant(MapperConfiguration).InSingletonScope();

            // This teaches Ninject how to create automapper instances say if for instance
            // MyResolver has a constructor with a parameter that needs to be injected
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(MapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration MapperConfiguration => new MapperConfiguration(cfg =>
        {
            // Add all profiles in current assembly
            //cfg.AddProfiles(GetType().Assembly);
            cfg.AddProfile(new ChatClientProfile());
            cfg.AddProfile(new LogicLevelProfile());
        });
    }
}
