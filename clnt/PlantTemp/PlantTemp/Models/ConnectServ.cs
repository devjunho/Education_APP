using System;
using PlantTemp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PlantTemp.View;

namespace PlantTemp.Models
{
    public class ConnectServ
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private string _ip = "10.10.21.110";
        private int _port = 9998;

        static public Data sendData = new Data();                
        UserData userData = new UserData();
        List<string> userList = new List<string>();

        static public List<string> Q_Title = new List<string>();
        static public List<string> Q_Content = new List<string>();
        static public List<int> Q_Check = new List<int>();
        static public string? Q_Answer;

        public ConnectServ()
        {

        }

        private int Connect()
        {
            try
            {
                // 연결 안녕
                _client = new TcpClient(_ip, _port);
                _stream = _client.GetStream();
            }
            catch (Exception e)
            {
                return 0;
            }

            return 1;
        }

        private void Disconnect()
        {
            // 연결잘가
            _stream.Close();
            _client.Close();
        }

        public List<Data>? GetSearch(List<string>? Plant)
        {
            List<Data>? result = new List<Data>();

            //서버와 연결
            if (Connect() == 0)
            {
                return null;
            }

            Data sendData = new Data
            {
                Type = DataType.PLANT_SEARCH_TRY,
                Plant = Plant,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            while (true)
            {
                // 결과 수신
                byte[] buffer = new byte[2048];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // 역직렬화
                Data? recvData = JsonSerializer.Deserialize<Data>(jsonString);

                if (recvData.Type == DataType.PLANT_SEARCH_NO_RESULT)
                {
                    Disconnect();
                    return null;
                }
                else if (recvData.Type == DataType.PLANT_SEARCH_END)
                {
                    break;
                }
                else if (recvData.Type == DataType.PLANT_SEARCH)
                {
                    result.Add(recvData);
                }
                else
                {
                    Disconnect();
                    return null;
                }
            }

            //// 서버와 연결 종료
            Disconnect();
            
            return result;
        }

        public List<Data>? GetQuiz()
        {
            List<Data>? result = new List<Data>();

            //서버와 연결
            if (Connect() == 0)
            {
                return null;
            }

            Data sendData = new Data
            {
                Type = DataType.QUESTION_TRY,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            while (true)
            {
                // 결과 수신
                byte[] buffer = new byte[4096];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // 역직렬화
                Data? recvData = JsonSerializer.Deserialize<Data>(jsonString);

                if (recvData.Type == DataType.QUESTION_END)
                {
                    break;
                }
                else if (recvData.Type == DataType.QUESTION_INFO)
                {
                    result.Add(recvData);
                }
                else
                {
                    Disconnect();
                    return null;
                }
            }

            //// 서버와 연결 종료
            Disconnect();

            return result;
        }

        public int TryLogin(string ID, string PW, bool btn_flag)
        {

            List<string> userList = new List<string>();

            //서버와 연결
            if (Connect() == 0)
            {
                return 0;
            }

            UserData userData = new UserData
            {
                ID = ID,
                PW = PW,
            };

            userList.Add(userData.ID);
            userList.Add(userData.PW);

            Data sendData = new Data();

            if (btn_flag)
            {
                sendData.Type = DataType.CUSTOMER_LOGIN_TRY;
                sendData.User = userList;
            }
            else
            {
                sendData.Type = DataType.EMPLOYEE_LOGIN_TRY;
                sendData.User = userList;
            }


            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            // 서버와 연결 종료
            Disconnect();

            return (int)result.Type;
        }

        public int TryIDCheck(string ID)
        {
            //서버와 연결
            if (Connect() == 0)
            {
                return 0;
            }
            List<string> user = new List<string>();
            UserData userdata = new UserData()
            {
                ID = ID,
            };
            user.Add(userdata.ID);

            Data sendData = new Data()
            {
                Type = DataType.SIGN_UP_CHECK_ID,
                User = user,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            // 서버와 연결 종료
            Disconnect();

            return (int)result.Type;
        }

        public int TrySignUp(string ID, string PW, string NAME, string EMAIL, bool btn_flag)
        {
            //서버와 연결
            if (Connect() == 0)
            {
                return 0;
            }
            List<string> user = new List<string>();
            UserData data = new UserData()
            {
                ID = ID,
                PW = PW,
                Name = NAME,
                Email = EMAIL,
            };

            user.Add(data.ID);
            user.Add(data.PW);
            user.Add(data.Name);
            user.Add(data.Email);

            Data sendData = new Data();

            if (btn_flag == true)
            {
                sendData.Type = DataType.CUSTOMER_SIGN_UP_TRY;
                sendData.User = user;
            }
            else
            {
                sendData.Type = DataType.EMPLOYEE_SIGN_UP_TRY;
                sendData.User = user;
            }

            // 직렬화 
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            // 서버와 연결 종료
            Disconnect();

            return (int)result.Type;
        }

        public int TryChat()
        {
            //서버와 연결
            if (Connect() == 0)
            {
                return 0;
            }

            Data sendData = new Data
            {
                Type = DataType.CUSTOMER_CHAT_BEGIN_TRY,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            // 서버와 연결 종료

            return (int)result.Type;
        }

        public Data TryChat2()
        {
            //서버와 연결
            if (Connect() == 0)
            {
                return null;
            }

            Data sendData = new Data
            {
                Type = DataType.EMPLOYEE_CHAT_BEGIN_TRY,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            return result;
        }

        public string RecvMessage()
        {
            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            if (result.Type == DataType.CUSTOMER_CHAT_END_TRY_SUCCED)
            {
                Disconnect();
                return "";
            }
            return result.Chat;
        }

        public string RecvMessage2()
        {
            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            // 연결 종료
            if (result.Type == DataType.EMPLOYEE_CHAT_END_TRY)
            {
                Data sendData = new Data
                {
                    Type = DataType.EMPLOYEE_CHAT_END_TRY,
                };

                // 직렬화
                string jsonData = JsonSerializer.Serialize(sendData);
                byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
                _stream.Write(bytesToSend, 0, bytesToSend.Length);

                Disconnect();
                return "EMPLOYEE_CHAT_END_TRY";
            }
            return result.Chat;
        }

        public void SendMessage(string message)
        {
            Data sendData = new Data
            {
                Type = DataType.CHAT_MESSAGE,
                Chat = message,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void SendMessage2(string message)
        {
            Data sendData = new Data
            {
                Type = DataType.CHAT_MESSAGE,
                Chat = message,
            };

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void ExitMessage()
        {
            Data sendData = new Data();

            sendData.Type = DataType.CUSTOMER_CHAT_END_TRY;

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        public void ShowAnswer(string ID, string Que_Title)
        {
            Connect();

            userData.ID = ID;

            userList.Add(userData.ID);

            sendData.Type = DataType.QNA_PAGE_NEXT_TRY;
            sendData.User = userList;
            sendData.QTitle = Que_Title;

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 받기
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            Data? result = JsonSerializer.Deserialize<Data>(jsonString);

            Q_Answer = result.Content;

            // 서버와 연결 종료
            Disconnect();
        }

        public void ShowQNA(string ID, string PW)
        {
            Connect();

            userData.ID = ID;
            userData.PW = PW;

            userList.Add(userData.ID);
            userList.Add(userData.PW);

            sendData.Type = DataType.QNA_CUSTOMER_TRY;
            sendData.User = userList;

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);          // 아이디랑 비밀번호 보냄


            while (true)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // 역직렬화
                Data? result = JsonSerializer.Deserialize<Data>(jsonString);

                if (result.Type == DataType.QNA_FINISH)
                {
                    break;
                }

                Q_Title.Add(result.QTitle);
                Q_Content.Add(result.Content);
                Q_Check.Add(result.Check);
            }
            // 서버와 연결 종료
            Disconnect();
        }

        public int TryChart()
        {
            // 서버와 연결
            if (Connect() == 0)
            {
                return 0;
            }

            ResultData sendData = new ResultData();
            Data? result = null;

            sendData.Type = (int)DataType.CHARTS_QUESTION_TRY;

            // 직렬화
            string jsonData = JsonSerializer.Serialize(sendData);
            byte[] bytesToSend = Encoding.UTF8.GetBytes(jsonData);
            _stream.Write(bytesToSend, 0, bytesToSend.Length);

            // 결과 수신
            byte[] buffer = new byte[1024];
            int bytesRead = _stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // 역직렬화
            result = JsonSerializer.Deserialize<Data>(jsonString); // 변경된 부분

            if (result != null && result.Type == DataType.CHARTS_QUESTION_SUCCEED)
            {
                foreach (var chartData in result.Chart)
                {
                    ResultData temp = new ResultData
                    {
                        Id = chartData.Id,
                        Correct = chartData.Correct,
                        Incorrect = chartData.Incorrect,
                    };

                    E_Chart.chart.Add(temp);

                    //MessageBox.Show($"{temp.Id}, {temp.Correct},{temp.Incorrect}");
                }
            }
            // 서버와 연결 종료
            Disconnect();
            return (int)result.Type;
        }
    }
}