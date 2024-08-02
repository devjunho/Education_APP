using PlantTemp.View;
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
    public partial class QnA_Customer : Page
    {
        ConnectServ connectServ = new ConnectServ();
        public QnA_Customer()
        {
            InitializeComponent();
  
            Qu_Text.Text = QNA1.Question_Text;
            connectServ.ShowAnswer(Login.public_ID, QNA1.Question_Title);
            Qu_Text1.Text = ConnectServ.Q_Answer;
        }
    }
}
