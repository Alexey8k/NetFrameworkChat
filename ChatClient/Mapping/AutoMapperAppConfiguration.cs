using AutoMapper;
using LogicLevel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Mapping
{
    public static class AutoMapperAppConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ChatClientProfile());
                cfg.AddProfile(new LogicLevelProfile());
            });
        }
    }
}
