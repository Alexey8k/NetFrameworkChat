using AutoMapper;
using DataLevel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Mapping
{
    public static class AutoMapperAppConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DataLevelProfile());
                cfg.AddProfile(new ChatServiceProfile());
            });
        }
    }
}
