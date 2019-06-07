using AutoMapper;
using LogicLevel.ChatServiceReference;
using LogicLevel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLevel.Infrastructure
{
    public class SecureAfterMapAction : IMappingAction<BaseSecureModel, BaseHashModel>
    {
        private readonly IHashAlgorithm _hashAlgorithm = new HashSHA1();

        public SecureAfterMapAction(IHashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public void Process(BaseSecureModel source, BaseHashModel destination)
        {
            var ptrPsw = Marshal.SecureStringToGlobalAllocAnsi(source.SecurePassword);
            destination.Hash = _hashAlgorithm.GetHash($"{source.Login}|{Marshal.PtrToStringAnsi(ptrPsw)}");
            Marshal.ZeroFreeGlobalAllocAnsi(ptrPsw);
        }
    }
}
