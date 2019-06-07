using AutoMapper;
using ChatClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Mapping
{
    public static class BaseViewModelExtension
    {
        public static T Mapping<T>(this BaseViewModel obj)
        {
            return Mapper.Map<BaseViewModel, T>(obj);
        }
    }
}
