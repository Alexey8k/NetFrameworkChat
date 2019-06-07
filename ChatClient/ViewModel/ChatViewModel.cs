using ChatClient.Mapping;
using ChatClient.Model;
using ChatClient.UserControl;
using ChatClient.View;
using LogicLevel;
using LogicLevel.Mapping;
using LogicLevel.Model;
using Microsoft.Expression.Interactivity.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatClient.ViewModel
{
    class ChatViewModel : BaseViewModel
    {
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;

        private readonly IChatClient _chatClient;

        private AutoResetEvent _currentUserReceived = new AutoResetEvent(false);

        private UserPartialModel _currentUser;
        private UserPartialModel CurrentUser
        {
            set
            {
                _currentUser = value;
                OnPropertyChanged("IsLogin");
            }
        }

        private IEnumerable<UserPartialModel> _selectedItems;

        public int UserId => _currentUser?.Id ?? 0;

        public string Login => _currentUser?.Login ?? null;

        public bool IsLogin => _currentUser != null;

        public MessageStatus MessageStatus
        {
            get
            {
                if (_selectedTab == ItemTab.BlakList || _selectedItems == null || _selectedItems.Count() == 0)
                    return MessageStatus.Share;
                return MessageStatus.Privat;
                //return _selectedTab == ItemTab.Friends ? MessageStatus.Friend : MessageStatus.Privat;
            }
        }

        public int[] UsersId
        {
            get
            {
                if (_selectedItems != null && _selectedItems.Count() > 0)
                    return _selectedItems.Select(u => u.Id).ToArray();
                return MessageStatus == MessageStatus.Friend
                    ? Friends.Select(u => u.Id).ToArray()
                    : null;
            }
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText; 
            set
            {
                _messageText = value;
                OnPropertyChanged("MessageText");
            }
        }

        private string _chatBox;
        public string ChatBox
        {
            get => _chatBox; 
            set
            {
                _chatBox = value;
                OnPropertyChanged("ChatBox");
            }
        }

        public ObservableCollection<UserPartialModel> Users { get; set; } 
            = new ObservableCollection<UserPartialModel>();
        public ObservableCollection<UserPartialModel> Friends { get; set; } 
            = new ObservableCollection<UserPartialModel>();
        public ObservableCollection<UserPartialModel> BlackList { get; set; } 
            = new ObservableCollection<UserPartialModel>();

        public ChatViewModel(IChatClient chatClient)
        {
            _chatClient = chatClient;

            _chatClient.CurrentUserReceived += (sender, args) =>
            {
                CurrentUser = args.Mapping<UserPartialModel>();
                _currentUserReceived.Set();
            };

            _chatClient.OnMessageReceived += (sender, args) =>
            {
                var user = Users.FirstOrDefault(u => u.Id == args.Message.UserId)
                ?? Friends.FirstOrDefault(u => u.Id == args.Message.UserId);
                if (user == null) return;
                ChatBox += 
                $"({DateTime.Now}) " +
                $"{Users.ToList().Find(u => u.Id == args.Message.UserId).Login}" +
                $"{(args.Message.Status == MessageStatus.Share ? "" : "(private)")}: " +
                $"{args.Message.Text}\r\n";
            };

            _chatClient.OnlineUsersReceived += (sender, args) =>
            {
                if (args.Users == null) return;
                _dispatcher.Invoke(() =>
                {
                    foreach (var user in args.Users)
                        switch (user.Relation)
                        {
                            case UserRelation.Friend:
                                Friends.Add(user);
                                break;
                            case UserRelation.Blacklist:
                                BlackList.Add(user);
                                break;
                            case UserRelation.None:
                                Users.Add(user);
                                break;
                        }
                });
            };

            _chatClient.UnreadMessagesReceived += (sender, args) =>
            {
                if (args.Messages == null) return;
                foreach (var message in args.Messages)
                    ChatBox +=
                    
                $"({DateTime.Now}) " +
                $"{message.Login}" +
                $"{(message.Status == MessageStatus.Share ? "" : "(private)")}: " +
                $"{message.Text}\r\n";
            };

            _chatClient.OnUserJoined += (sender, args) =>
            {
                _dispatcher.Invoke(() =>
                {
                    switch (args.Relation)
                    {
                        case UserRelation.Friend:
                            Friends.Add(args.Mapping<UserPartialModel>());
                            break;
                        case UserRelation.Blacklist:
                            BlackList.Add(args.Mapping<UserPartialModel>());
                            break;
                        case UserRelation.None:
                            Users.Add(args.Mapping<UserPartialModel>());
                            break;
                    }
                });
                ChatBox += $"({DateTime.Now}) Присоединился---> {args.Login}\r\n";
            };
            _chatClient.OnUserLeave += (sender, args) =>
            {
                var user = Users.FirstOrDefault(u => u.Id == args.UserId)
                ?? Friends.FirstOrDefault(u => u.Id == args.UserId)
                ?? BlackList.FirstOrDefault(u => u.Id == args.UserId);
                _dispatcher.Invoke(() => Users.Remove(user) || Friends.Remove(user) || BlackList.Remove(user));
                ChatBox += $"({DateTime.Now}) Отсоединился ---> {user.Login}\r\n";
            };
            _chatClient.OnAddBlackListByUser += (sender, args) =>
            {
                var userLogin = Users.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login
                ?? Friends.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login
                ?? BlackList.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login;
                ChatBox += $"({DateTime.Now}) Пользователь ({userLogin}) добавил вас в чорный список\r\n";
            };
            _chatClient.OnRemoveBlackListByUser += (sender, args) =>
            {
                var userLogin = Users.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login
                ?? Friends.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login
                ?? BlackList.FirstOrDefault(u => u.Id == args.InitiatorUserId)?.Login;
                ChatBox += $"({DateTime.Now}) Пользователь ({userLogin}) удалил вас из чорного списка\r\n";
            };
        }

        public ICommand SendMessageCommamd => new ActionCommand(sender =>
        {
            if (string.IsNullOrEmpty(MessageText) || string.IsNullOrWhiteSpace(MessageText)) return;
            _chatClient.SendMessage(this.Mapping<MessagePartialModel>());
            ChatBox += $"({DateTime.Now}) " +
            $"{Login}{(MessageStatus == MessageStatus.Privat ? "(private)" : "")}: " +
            $"{MessageText}\r\n";
            MessageText = string.Empty;
        });

        public ICommand SelectionChangeCommand => new ActionCommand(sender =>
        {
            _selectedItems = ((System.Collections.IList)sender).Cast<UserPartialModel>();
        });

        public ICommand AddFriendCommand => new ActionCommand(sender =>
        {
            // sender type --- SelectedItemCollection
            // ToDo implement add to friends
        });

        public ICommand AddBlackListCommand => new ActionCommand(sender =>
        {
            //var selectedItems = ((System.Collections.IList)sender).Cast<UserPartialModel>();
            _chatClient.AddBlackList(this.Mapping<RelationModel>());
            foreach (var user in _selectedItems.ToArray())
            {
                BlackList.Add(user);
                Users.Remove(user);
            }
        });

        public int[] BlackListUsersId { get; private set; }

        public ICommand RemoveBlackListCommand => new ActionCommand(sender =>
        {
            var selectedItems = ((System.Collections.IList)sender).Cast<UserPartialModel>();
            BlackListUsersId = selectedItems.Select(u => u.Id).ToArray();
            _chatClient.RemoveBlackList(this.Mapping<RelationModel>());
            BlackListUsersId = null;
            foreach (var user in selectedItems.ToArray())
            {
                Users.Add(user);
                BlackList.Remove(user);
            }

        });

        private string _text;
        public string Text
        {
            get => _text; 
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private ItemTab _selectedTab;
        public int TabSelectedIndex
        {
            get => (int)_selectedTab; 
            set
            {
                _selectedTab = (ItemTab)value;
                OnPropertyChanged("TabSelectedIndex");
            }
        }

        private System.Windows.Controls.UserControl _logInOut = new LoginControl();
        public System.Windows.Controls.UserControl LogInOut
        {
            get => _logInOut; 
            private set
            {
                _logInOut = value;
                OnPropertyChanged("LogInOut");
            }
        }

        public ICommand LoginCommand => new ActionCommand(sender =>
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() != true) return;
            _currentUserReceived.WaitOne();
            LogInOut = new LogoutControl();
        });

        public ICommand LogoutCommand => new ActionCommand(sender =>
        {
            if (_currentUser == null) return;

            _chatClient.Logout(this.Mapping<LogoutModel>());

            if (sender != null) return;
            CurrentUser = null;
            LogInOut = new LoginControl();
            Users.Clear();
            Friends.Clear();
            BlackList.Clear();
            ChatBox = string.Empty;
            MessageText = string.Empty;
        });

        public ICommand ScrollChatCommand => new ActionCommand(sender =>
        {
            var chatBox = (TextBox)sender;
            chatBox.CaretIndex = chatBox.Text.Length;
            chatBox.ScrollToEnd();
        });

        public ICommand CabinetCommand => new ActionCommand(p =>
        {
            MessageBox.Show("Cabinet!!!!!!!!!!");
        });

        public ICommand RegistrationCommand => new ActionCommand(parametr =>
        {
            var addWindow = new RegistrationWindow();
            addWindow.ShowDialog();
        });
    }
}
