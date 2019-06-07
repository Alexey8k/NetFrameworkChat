using ChatClient.Mapping;
using ChatClient.View;
using LogicLevel;
using LogicLevel.ChatServiceReference;
using LogicLevel.Model;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChatClient.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IChatClient _chatClient;
        public LoginViewModel(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public SecureString Password { get; private set; }

        public string Login { get; set; }

        public byte[] Hash
        {
            get
            {
                var ptrPsw = Marshal.SecureStringToGlobalAllocAnsi(Password);
                var hash =
                    new SHA1CryptoServiceProvider().ComputeHash(
                        Encoding.Default.GetBytes($"{Login}|{Marshal.PtrToStringAnsi(ptrPsw)}"));
                Marshal.ZeroFreeGlobalAllocAnsi(ptrPsw);
                return hash;
            }
        }

        private bool _buttonIsEnabled = true;

        public bool ButtonIsEnabled
        {
            get { return _buttonIsEnabled; }
            private set
            {
                _buttonIsEnabled = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand => new ActionCommand(async sender =>
        {
            if (string.IsNullOrEmpty(Login) || Password == null || Password.Length == 0)
            {
                MessageBox.Show("Не все поля заполнены.");
                return;
            }
            ButtonIsEnabled = false;
            try
            {
                var result = await _chatClient.Login(this.Mapping<LoginModel>());
                switch (result.Result)
                {
                    case LoginResult.Fail:
                        ButtonIsEnabled = true;
                        MessageBox.Show("Не верный логин или пароль.");
                        break;
                    case LoginResult.Online:
                        ButtonIsEnabled = true;
                        MessageBox.Show("Пользователь онлайн.");
                        break;
                    case LoginResult.Ok:
                        Password.Dispose();
                        ((Window)sender).DialogResult = true;
                        break;
                    default:
                        MessageBox.Show("Неизвестная ошибка");
                        ButtonIsEnabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                ButtonIsEnabled = true;
                MessageBox.Show(ex.Message);
            }
        });

        public ICommand PswCommand => new ActionCommand(sender => Password = ((PasswordBox)sender).SecurePassword);

        public ICommand RegistrationCommand => new ActionCommand(sender => new RegistrationWindow().ShowDialog());
    }
}
