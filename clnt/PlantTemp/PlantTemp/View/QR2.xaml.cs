﻿using PlantTemp.ViewModels;
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
    /// QR2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class QR2 : Page
    {
        public QR2()
        {
            InitializeComponent();
            DataContext = QuizViewModel1.Instance;
            _ = QuizViewModel2.Instance;
        }
        private void Search_btn_Click(object sender, RoutedEventArgs e)
        {
            string temp = "/View/I_Quiz3.xaml";
            Uri uri = new Uri(temp, UriKind.Relative);
            NavigationService.Navigate(uri);
            QuizViewModel1.Instance.Reset1();
        }

        private void Logout_btn_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Login.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Search1.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Quiz1_btn_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/View/Quiz1.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }
    }
}
