using PlantTemp.Models;
using PlantTemp.ViewModels;
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

namespace PlantTemp.View
{
    /// <summary>
    /// QNA1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class QNA1 : Page
    {
        ConnectServ connectServ = new ConnectServ();
        static public string? Question_Title;
        static public string? Question_Text;

        public QNA1()
        {
            InitializeComponent();
            if (Login.btn_flag == false)      // 상담사 버튼 내용 Chage
            {
                btn_Q.Content = "답변하기";
            }

            connectServ.ShowQNA(Login.public_ID, Login.public_PW);
            List<QNAData> QNADatas = new List<QNAData>();
            QNAData QnA = new QNAData();

            for (int i = 0; i < ConnectServ.Q_Title.Count; i++)
            {
                QnA.Q_Title = ConnectServ.Q_Title[i];
                QnA.Q_Text = ConnectServ.Q_Content[i];
                if (ConnectServ.Q_Check[i] == 0)
                {
                    QnA.Q_Check = "답변 진행중";
                }
                else
                {
                    QnA.Q_Check = "답변 완료";
                }
                QNADatas.Add(QnA);
            }
            listView_QnA.ItemsSource = QNADatas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Search1.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void btn_Q_Click(object sender, RoutedEventArgs e)
        {
            if (Login.btn_flag == true)     // '고객'일 경우
            {
                NavigationService.Navigate(new Uri("/View/QnA3.xaml", UriKind.Relative));
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Login.btn_flag == false)     // '상담사'일 경우
            {
                var selectedItem = listView_QnA.SelectedItem as QNAData;
                if (selectedItem != null)
                {
                    Question_Title = selectedItem.Q_Title;
                    Question_Text = selectedItem.Q_Text;
                    NavigationService.Navigate(new Uri("/View/QnA2.xaml", UriKind.Relative));
                }
            }
            else
            {
                var selectedItem = listView_QnA.SelectedItem as QNAData;
                if (selectedItem != null)
                {
                    Question_Title = selectedItem.Q_Title;
                    Question_Text = selectedItem.Q_Text;
                    NavigationService.Navigate(new Uri("/View/QnA_Customer.xaml", UriKind.Relative));
                }
            }
        }

        private void Logout_btn_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Login.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Quiz1_btn_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Quiz1.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
