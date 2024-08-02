using PlantTemp.Models;
using PlantTemp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// ECHAT.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ECHAT : Page
    {
        public ECHAT()
        {
            InitializeComponent();
            this.DataContext = new ChatViewModel1();
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

        private void Move_Login_Page(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/LoginPage.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void ChatListBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChatViewModel1 viewModel)
            {
                ((ObservableCollection<MessageModel>)viewModel.Messages).CollectionChanged += Messages_CollectionChanged;
            }
        }

        private void Messages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems.Count > 0)
            {
                var newItem = e.NewItems[0];
                ChatListBox.Dispatcher.Invoke(() =>
                {
                    ChatListBox.ScrollIntoView(newItem);
                });
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

        private void Qna1_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
