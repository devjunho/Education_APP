using PlantTemp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using static System.Net.Mime.MediaTypeNames;

namespace PlantTemp.ViewModels
{

    public class QuizViewModel :ViewModelBase
    {
        private static QuizViewModel _instance;
        private ConnectServ _connectServ;
        private string _selectCommonName;
        private string _selectKoreanFamilyName;
        private string _selectScientificName;
        private string _selectFamilyName;
        private string _selectFloweringPeriod;
        private string _selectFlowerColor;
        private string _selectInfo;
        private string _qlabel;
        private BitmapImage _selectUrl;
        private List<BitmapImage> _tempImage;
        private BitmapImage _quizUrl1;
        private BitmapImage _quizUrl2;
        private BitmapImage _quizUrl3;

        public QuizViewModel()
        {
            _connectServ = new ConnectServ();
            _tempImage = new List<BitmapImage>();
            GetQuiz();
        }

        private async void GetQuiz()
        {
            List<Data>? result = await Task.Run(() => _connectServ.GetQuiz());
            if (result is null)
            {
                MessageBox.Show("조회 오류");
            }
            else
            {
                int index = 0;
                SelectCommonName = result[0].Plant[0];
                SelectKoreanFamilyName = result[0].Plant[1];
                SelectScientificName = result[0].Plant[2];
                SelectFamilyName = result[0].Plant[3];
                SelectFloweringPeriod = result[0].Plant[4];
                SelectFlowerColor = result[0].Plant[5];
                SelectInfo = result[0].Plant[6];

                QLabel = "다음 중 국명이 " + SelectCommonName + "인 식물은?";
                foreach (Data item in result)
                {
                    if (index == 0)
                    {
                        BitmapImage a = new BitmapImage();
                        a = await DownloadImageAsync(item.Plant[7]);
                        QuizUrl1 = a;
                        SelectUrl = a;
                    }
                    else if (index == 1)
                    {
                        QuizUrl2 =  await DownloadImageAsync(item.Plant[0]);
                    }
                    else
                    {
                        QuizUrl3 = await DownloadImageAsync(item.Plant[0]);
                    }
                    index++;
                }
            }
        }

        public async Task<BitmapImage> DownloadImageAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageData = await client.GetByteArrayAsync(url);

                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            BitmapImage bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.StreamSource = ms;
                            bitmap.EndInit();
                            bitmap.Freeze();
                            return bitmap;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"이미지 다운로드 중 오류 발생: {ex.Message}");
                }
            }
            return null;
        }

        public static QuizViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuizViewModel();
                }
                return _instance;
            }
        }

        public void Reset()
        {
            _instance = null;
        }

        public async void LoadImage(string imageUrl)
        {
            SelectUrl = null;
            BitmapImage bitmap = await DownloadImageAsync(imageUrl);

            if (bitmap != null)
            {
                _tempImage.Add(bitmap);
            }
        }

        public string QLabel
        {
            get => _qlabel;
            set => SetProperty(ref _qlabel, value);
        }

        public string SelectCommonName
        {
            get => _selectCommonName;
            set => SetProperty(ref _selectCommonName, value);
        }

        public string SelectKoreanFamilyName
        {
            get => _selectKoreanFamilyName;
            set => SetProperty(ref _selectKoreanFamilyName, value);
        }

        public string SelectScientificName
        {
            get => _selectScientificName;
            set => SetProperty(ref _selectScientificName, value);
        }

        public string SelectFamilyName
        {
            get => _selectFamilyName;
            set => SetProperty(ref _selectFamilyName, value);
        }

        public string SelectFloweringPeriod
        {
            get => _selectFloweringPeriod;
            set => SetProperty(ref _selectFloweringPeriod, value);
        }

        public string SelectFlowerColor
        {
            get => _selectFlowerColor;
            set => SetProperty(ref _selectFlowerColor, value);
        }

        public string SelectInfo
        {
            get => _selectInfo;
            set => SetProperty(ref _selectInfo, value);
        }

        public BitmapImage SelectUrl
        {
            get => _selectUrl;
            set => SetProperty(ref _selectUrl, value);
        }

        public BitmapImage QuizUrl1
        {
            get => _quizUrl1;
            set => SetProperty(ref _quizUrl1, value);
        }

        public BitmapImage QuizUrl2
        {
            get => _quizUrl2;
            set => SetProperty(ref _quizUrl2, value);
        }

        public BitmapImage QuizUrl3
        {
            get => _quizUrl3;
            set => SetProperty(ref _quizUrl3, value);
        }
    }
}
