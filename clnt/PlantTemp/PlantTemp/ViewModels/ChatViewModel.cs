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
    public class ChatViewModel : ViewModelBase
    {
        private IList<MessageModel> _messages = new ObservableCollection<MessageModel>();
        private string _newMessage;
        private string _userID;
        private string _loginLabel;
        private ConnectServ _connectServ;
        private CancellationTokenSource _cancleTokenSource;
        private bool _isChat = false;
        private bool _isLogin = false;
        private ChatGPT _GPT;

        public ChatViewModel()
        {
            _GPT = new ChatGPT();
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
                new MessageModel { MessageType = "Bot", Message = "안녕하세요? 고객님 식물 정보 챗봇입니다!" },
                new MessageModel { MessageType = "Bot", Message = "보다 정확한 내용이 궁금하실때는 상담사 연결을 해주세요." },
                new MessageModel { MessageType = "Bot", Message = "원하시는 서비스나 문의 사항을 말씀해 주시면,\n빠른 시간 내에 상담사와 연결해 드리겠습니다." },
            };
        }

        public ICommand SendMessage { get; private set; }
        public ICommand WantChat { get; private set; }

        public void WantChat_Click(object parameter)
        {
            if (!_isChat)
            {
                TryChat();
            }
            else
            { 
                _connectServ.ExitMessage();
            }
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
                    RecvGPT(NewMessage);
                }
                NewMessage = string.Empty;
            }
        }

        private async Task RecvGPT(string message)
        {
            string response = await _GPT.GetChatGPTResponse(message);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new MessageModel
                {
                    MessageType = "Bot",
                    Message = response,
                });
            });
        }

        private async void TryChat()
        {
            int result;
            result = await Task.Run(() => _connectServ.TryChat());
            if (result == (int)DataType.CUSTOMER_CHAT_BEGIN_SUCCEED)
            {
                _isChat = true;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new MessageModel
                    {
                        MessageType = "Bot",
                        Message = "상담원과 연결되었습니다.\n상담 중 폭언, 욕설, 인신공격 등 부적절한 언행이 있을 경우, 상담이 중단될 수 있습니다.\n고객님의 양해와 협조 부탁드립니다."
                    });
                });
                RecvMessage();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Add(new MessageModel
                    {
                        MessageType = "Bot",
                        Message = "현재 상담가능한 상담사가 없습니다 잠시 기다려 주세요."
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
                    string message = await Task.Run(() => _connectServ.RecvMessage(), cancellationToken);

                    if (!string.IsNullOrEmpty(message))
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
                        Messages.Add(new MessageModel { MessageType = "Bot", Message = "채팅이 종료되었습니다." });
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
