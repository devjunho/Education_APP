using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LiveCharts.Defaults;
using LiveCharts;
using PlantTemp.Models;
using LiveCharts.Wpf.Charts.Base;
using System.Reflection.Emit;
using LiveCharts.Wpf;

namespace PlantTemp.View
{
    /// <summary>
    /// E_Chart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class E_Chart : Page
    {
        ConnectServ ConnectServ = new ConnectServ();

        static public ResultData ResultData = new ResultData();
        static public List<ResultData> chart = new List<ResultData>();

        //public SeriesCollection SeriesCollection { get; set; }
        //public string[] Labels { get; set; }
        //public Func<double, string> Formatter { get; set; }
        public ChartValues<ObservableValue> Values { get; set; }


        public E_Chart()
        {
            InitializeComponent();
            Sep_lv.ItemsSource = chart;

            Values = new ChartValues<ObservableValue>();
            DataContext = this;
        }

        private void Plant_info_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
                new Uri("/Search1.xaml", UriKind.Relative)
                );
        }

        private void Qna1_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
            new Uri("/QnA1.xaml", UriKind.Relative)
                    );
        }

        private void Chat_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(
            new Uri("/Chat.xaml", UriKind.Relative)
                    );
        }

        private void check_btn_Click(object sender, RoutedEventArgs e)
        {

            int charts = ConnectServ.TryChart();
            if (charts == (int)DataType.CHARTS_QUESTION_SUCCEED)
                //MessageBox.Show("확인");

            Sep_lv.Items.Refresh();

            foreach (var a in chart)
            {
                Values.Add(new ObservableValue(a.Correct));

            }
        }
    }
}
