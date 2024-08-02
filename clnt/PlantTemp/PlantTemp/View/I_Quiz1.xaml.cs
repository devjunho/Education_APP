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
    /// I_Quiz1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class I_Quiz1 : Page
    {
        public I_Quiz1()
        {
            InitializeComponent();
            DataContext = QuizViewModel.Instance;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Search1.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Uri uri = new Uri("/View/QuizResult.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
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
