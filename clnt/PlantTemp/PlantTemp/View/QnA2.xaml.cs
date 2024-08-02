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
    /// QnA2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class QnA2 : Page
    {
        public QnA2()
        {
            InitializeComponent();
            Qu_Title.Content = QNA1.Question_Title;
            Qu_Text.Text = QNA1.Question_Text;
        }
    }
}
