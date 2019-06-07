using ChatClient.Infrastructure;
using ChatClient.Mapping;
using LogicLevel;
using LogicLevel.Model;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChatClient.ViewModel
{
    class RegistrationViewModel : BaseViewModel
    {
        private readonly IChatClient _chatClient;

        public RegistrationViewModel(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        private SecureString _securePassword;
        public SecureString SecurePassword
        {
            get => _securePassword;
            private set
            {
                _securePassword = value;
                OnPropertyChanged("PasswordLength");
            }
        }

        public string Login { get; set; }

        public string Email { get; set; }

        public UserRole Role => UserRole.User;

        public int Color32 => 44447;

        public int PasswordLength => _securePassword?.Length ?? 0;

        private bool? _isValidConfirmPassword = null;
        public bool? IsValidConfirmPassword
        {
            get => _isValidConfirmPassword;
            private set
            {
                _isValidConfirmPassword = value;
                OnPropertyChanged("IsValidConfirmPassword");
            }
        }

        private bool _buttonIsEnabled = true;
        public bool ButtonIsEnabled
        {
            get { return _buttonIsEnabled; }
            private set
            {
                _buttonIsEnabled = value;
                OnPropertyChanged("ButtonIsEnabled");
            }
        }

        public ICommand PasswordCommand => new ActionCommand(sender =>
        {
            SecurePassword = ((PasswordBox)sender).SecurePassword;
        });

        public ICommand ConfirmPasswordCommand => new ActionCommand(sender =>
        {
            IsValidConfirmPassword = IsEqualPasswords(((PasswordBox)sender).SecurePassword);
        });

        private bool? IsEqualPasswords(SecureString confirmPassword)
        {
            if (PasswordLength != confirmPassword.Length)
                return confirmPassword.Length == 0 && IsValidConfirmPassword == null ? null : (bool?)false;

            if (PasswordLength == 0)
                return null;

            var ptrPsw = Marshal.SecureStringToGlobalAllocAnsi(_securePassword);
            var ptrCnfPsw = Marshal.SecureStringToGlobalAllocAnsi(confirmPassword);

            var isEqual = Marshal.PtrToStringAnsi(ptrPsw) == Marshal.PtrToStringAnsi(ptrCnfPsw);

            Marshal.ZeroFreeGlobalAllocAnsi(ptrPsw);
            Marshal.ZeroFreeGlobalAllocAnsi(ptrCnfPsw);

            return isEqual;
        }


        public ICommand RegistrationCommand => new ActionCommand(async sender =>
        {
            if (string.IsNullOrEmpty(Login) || IsValidConfirmPassword != true || Email.Length == 0)
            {
                MessageBox.Show("Не все поля заполнены или пароли не совпадают.");
                return;
            }
            ButtonIsEnabled = false;
            var result = await _chatClient.Registration(this.Mapping<RegistrationModel>());
            switch (result.Result)
            {
                case RegistrationResult.Login:
                    ButtonIsEnabled = true;
                    MessageBox.Show("Данный логин уже занят.");
                    break;
                case RegistrationResult.Email:
                    ButtonIsEnabled = true;
                    MessageBox.Show("Пользователь с такой почтой уже есть.");
                    break;
                case RegistrationResult.Ok:
                    ((Window)sender).DialogResult = true;
                    break;
                default:
                    ButtonIsEnabled = true;
                    MessageBox.Show("Неизвестная ошибка регистрации.");
                    break;
            }
        });
    }
}
