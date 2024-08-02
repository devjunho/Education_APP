using PlantTemp.Models;
using PlantTemp.View;
using PlantTemp.ViewModels;
using PlantTemp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml;

namespace PlantTemp.ViewModels
{
    public class ChatViewModel1 : ViewModelBase
    {
        private IList<MessageModel> _messages = new ObservableCollection<MessageModel>();
        private string _newMessage;
        private string _userID;
        private string _loginLabel;
        private ConnectServ _connectServ;
        private CancellationTokenSource _cancleTokenSource;
        private bool _isChat = false;
        private bool _isLogin = false;

        public ChatViewModel1()
        {
            SendMessage = new RelayCommand<object>(SendMessage_Click);
            WantChat = new RelayCommand<object>(WantChat_Click);
            _cancleTokenSource = new CancellationTokenSource();
            _connectServ = new ConnectServ();
            InitMessage();
        }

        private void InitMessage()
        {
            Messages = new ObservableCollection<MessageModel>
            {
                // 테스트 
                new MessageModel { MessageType = "Bot", Message = "현재 대기중인 고객님 0명" },
            };
        }

        public ICommand SendMessage { get; private set; }
        public ICommand WantChat { get; private set; }

        public void WantChat_Click(object parameter)
        {
            TryChat();
        }

        public void SendMessage_Click(object parameter)
        {
            if (!string.IsNullOrEmpty(NewMessage))
            {

                if (_isChat)
                {
                    Messages.Add(new MessageModel { MessageType = "Chat", Message = NewMessage });
                    _connectServ.SendMessage(NewMessage);
                    if (NewMessage.Contains("종료"))
                    {
                        _isChat = false;
                        _cancleTokenSource.Cancel();
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Messages.Add(new MessageModel { MessageType = "Bot", Message = "채팅이 종료되었습니다." });
                        });
                    }
                }
                else
                {
                    Messages.Add(new MessageModel { MessageType = "User", Message = NewMessage });
                }
                NewMessage = string.Empty;
            }
        }

        private async void TryChat()
        {
            Data result;
            result = await Task.Run(() => _connectServ.TryChat2());
            try
            {
                if (result.Type == DataType.EMPLOYEE_CHAT_BEGIN_SUCCEED)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add(new MessageModel
                        {
                            MessageType = "Bot",
                            Message = "고객님이 입장했습니다."
                        });
                    });

                    _isChat = true;
                    RecvMessage();
                }
            }
            catch (Exception)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new MessageModel
                    {
                        MessageType = "Bot",
                        Message = "서버연결오류입니다."
                    });
                });
            }
        }

        private async void RecvMessage()
        {
            CancellationToken cancellationToken = _cancleTokenSource.Token;

            while (_isChat)
            {
                try
                {
                    string message = await Task.Run(() => _connectServ.RecvMessage2(), cancellationToken);

                    if (message == "EMPLOYEE_CHAT_END_TRY")
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Messages.Add(new MessageModel
                            {
                                MessageType = "Bot",
                                Message = "고객님이 연결을 종료했습니다."
                            });
                        });
                        _isChat = false;
                    }
                    else if (!string.IsNullOrEmpty(message))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Messages.Add(new MessageModel { MessageType = "Service", Message = message });
                        });
                    }
                }
                catch (OperationCanceledException)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Messages.Add(new MessageModel { MessageType = "Bot", Message = "채팅종료" });
                    });
                    _isChat = false;
                    break;
                }
            }
        }

        public IList<MessageModel> Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public string NewMessage
        {
            get => _newMessage;
            set => SetProperty(ref _newMessage, value);
        }

    }
}
