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
using PlantTemp.Models;
using PlantTemp.View;

namespace PlantTemp.View
{
    public partial class Login : Page
    {
        ConnectServ connectServ = new ConnectServ();
        Data DATA = new Data();
        UserData UserData = new UserData();
        static public bool btn_flag;                  // '고객' 버튼이 눌러져 있을 경우로 초기화
        static public string id_save;
        static public string pw_save;
        static public string public_ID;
        static public string public_PW;

        public Login()
        {
            InitializeComponent();
            //btn_flag = true;            // '고객' 버튼이 눌러져 있을 경우로 초기화
        }

        private void Customer_btn_Click(object sender, RoutedEventArgs e)
        {
            btn_flag = true;

            SolidColorBrush brush_Green = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF446A46"));      // '고객' 버튼 색상 변경
            Customer_btn.Background = brush_Green;

            SolidColorBrush brush_Clear = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F446A46"));      // '고객' 버튼 색상 변경
            Oper_btn.Background = brush_Clear;
        }

        private void Oper_btn_Click(object sender, RoutedEventArgs e)
        {
            btn_flag = false;

            SolidColorBrush brush_Green = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF446A46"));      // '고객' 버튼 색상 변경
            Oper_btn.Background = brush_Green;

            SolidColorBrush brush_Clear = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7F446A46"));      // '고객' 버튼 색상 변경
            Customer_btn.Background = brush_Clear;
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            id_save = ID_text.Text;
            UserData.ID = id_save;
            public_ID = ID_text.Text;
            public_PW = PW_box.Password;

            if (btn_flag)
            {
                int loginResult = connectServ.TryLogin(UserData.ID, PW_box.Password, btn_flag);
                if (loginResult == (int)DataType.LOGIN_SUCCEED)
                {
                    MessageBox.Show("로그인 성공", "로그인");
                    NavigationService.Navigate(new Uri("/View/Search1.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("로그인 실패", "로그인");
                }
            }
            else
            {
                int loginResult = connectServ.TryLogin(UserData.ID, PW_box.Password, btn_flag);
                if (loginResult == (int)DataType.LOGIN_SUCCEED)
                {
                    MessageBox.Show("로그인 성공", "로그인");
                    NavigationService.Navigate(new Uri("/View/Search1.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("로그인 실패", "로그인");
                }
            }
        }

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/Join.xaml", UriKind.Relative));
        }

        private void ID_text_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ID_text != null)
            {
                ID_text.Text = "";
            }
        }
    }
}
