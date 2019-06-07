using ChatClient.Mapping;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.ViewModel
{
    class ViewModelLocator
    {
        public ChatViewModel ChatViewModel => _kernel.Get<ChatViewModel>();

        public LoginViewModel LoginViewModel => _kernel.Get<LoginViewModel>();

        public RegistrationViewModel RegistrationViewModel => _kernel.Get<RegistrationViewModel>();

        private static readonly IKernel _kernel = new StandardKernel();

        static ViewModelLocator()
        {
            AutoMapperAppConfiguration.Configure();
            _kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
