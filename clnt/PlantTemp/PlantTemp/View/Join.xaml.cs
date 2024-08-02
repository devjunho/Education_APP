using PlantTemp;
using PlantTemp.View;
using PlantTemp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlnatTemp
{
    /// <summary>
    /// Join.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Join : Page
    {
        Data data = new Data();
        UserData userData = new UserData();
        ConnectServ ConnectServ = new ConnectServ();
        Login Login = new Login();
        

        public Join()
        {
            InitializeComponent();
        }

        private void Join_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Login.btn_flag == false)
            {
                if (PW_box.Password.ToString() != PW_check_box.Password.ToString())
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "비밀번호 확인");
                }
                else if (PW_box.Password.ToString() == PW_check_box.Password.ToString())
                {
                    int join = ConnectServ.TrySignUp(ID_box.Text, PW_box.Password.ToString(), Name_box.Text, Email1_box.Text + "@" + Email2_box.Text, Login.btn_flag);
                    if (join == (int)DataType.SIGN_UP_SUCCEED)
                    {
                        MessageBox.Show("회원가입 완료", "회원가입 확인");
                        NavigationService.Navigate(
                            new Uri("/View/Login.xaml", UriKind.Relative)
                            );
                    }
                    else
                    {
                        MessageBox.Show("실패");
                    }
                }
            }
            else
            {
                if (PW_box.Password.ToString() != PW_check_box.Password.ToString())
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "비밀번호 확인");
                }
                else if (PW_box.Password.ToString() == PW_check_box.Password.ToString())
                {
                    int join = ConnectServ.TrySignUp(ID_box.Text, PW_box.Password.ToString(), Name_box.Text, Email1_box.Text + "@" + Email2_box.Text, Login.btn_flag);
                    if (join == (int)DataType.SIGN_UP_SUCCEED)
                    {
                        MessageBox.Show("회원가입 완료", "회원가입 확인");
                        NavigationService.Navigate(
                            new Uri("/View/Login.xaml", UriKind.Relative)
                            );
                    }
                    else
                    {
                        MessageBox.Show("실패");
                    }
                }
            }
        }

        private void Id_check_btn_Click(object sender, RoutedEventArgs e)
        {
            int idcheck = ConnectServ.TryIDCheck(ID_box.Text);
            if (idcheck == (int)DataType.SIGN_UP_CHECK_ID_FAIL)
            {
                MessageBox.Show("사용 가능한 아이디입니다.", "아이디 중복 확인");
            }
            else if (idcheck == (int)DataType.SIGN_UP_CHECK_ID_SUCCED)
            {
                MessageBox.Show("이미 사용 중인 아이디입니다.", "아이디 중복 확인");
            }
        }
    }
}
