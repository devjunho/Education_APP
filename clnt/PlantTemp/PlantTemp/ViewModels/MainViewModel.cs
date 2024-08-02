using MaterialDesignThemes.Wpf;
using PlantTemp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace PlantTemp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static MainViewModel instance;
        private string _imageUrl;
        private ConnectServ _connectServ;
        private ObservableCollection<PlantList> _items;
        private string _commonName;
        private string _koreanFamilyName;
        private string _scientificName;
        private string _familyName;
        private string _selectCommonName;
        private string _selectKoreanFamilyName;
        private string _selectScientificName;
        private string _selectFamilyName;
        private string _selectFloweringPeriod;
        private string _selectFlowerColor;
        private string _selectInfo;
        private BitmapImage _selectUrl;
        private List<BitmapImage> _imageList;
        private List<BitmapImage> _tempImage;
        private List<PlantList> _tempItem; 

        public MainViewModel()
        {
            _connectServ = new ConnectServ();
            _imageList = new List<BitmapImage>();
            _tempImage = new List<BitmapImage>();
            Itemsss = new ObservableCollection<PlantList> { };
            RecvUrl = new RelayCommand<object>(RecvUrl_Click);
            InitText = new RelayCommand<object>(InitText_Click);
            ItemDoubleClickCommand = new RelayCommand<Tuple<PlantList, int>>(OnItemDoubleClick);

            _tempItem = new List<PlantList>
            {
                new PlantList { CommonName = "노루삼", KoreanFamilyName = "미나리아재비과", ScientificName = "Actaea asiatica H. Hara", FamilyName = "Ranunculaceae" },
                new PlantList { CommonName = "백작약", KoreanFamilyName = "작약과", ScientificName = "Paeonia japonica (Makino) Miyabe & Takeda", FamilyName = "Paeoniaceae" },
                new PlantList { CommonName = "화살나무", KoreanFamilyName = "노박덩굴과", ScientificName = "Euonymus alatus (Thunb.) Siebold", FamilyName = "Celastraceae" },
                new PlantList { CommonName = "금불초", KoreanFamilyName = "국화과", ScientificName = "Inula britannica var. japonica (Thunb.) Franch. & Sav.", FamilyName = "Compositae" },
                new PlantList { CommonName = "가래나무", KoreanFamilyName = "가래나무과", ScientificName = "Juglans mandshurica Maxim.", FamilyName = "Juglandaceae" },
                new PlantList { CommonName = "까실쑥부쟁이", KoreanFamilyName = "국화과", ScientificName = "Aster ageratoides Turcz.", FamilyName = "Compositae" },
                new PlantList { CommonName = "개미취", KoreanFamilyName = "국화과", ScientificName = "Aster tataricus L.f.", FamilyName = "Compositae" },
                new PlantList { CommonName = "고려엉겅퀴", KoreanFamilyName = "국화과", ScientificName = "Cirsium setidens (Dunn) Nakai", FamilyName = "Compositae" },
                new PlantList { CommonName = "꽃창포", KoreanFamilyName = "붓꽃과", ScientificName = "Iris ensata var. spontanea (Makino) Nakai", FamilyName = "Iridaceae" },
                new PlantList { CommonName = "고삼", KoreanFamilyName = "콩과", ScientificName = "Sophora flavescens Aiton", FamilyName = "Leguminosae" },
            };

            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=16&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=26&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=88&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=9&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=89&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=10&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=2&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=3&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=11&imgSn=1&prntInfoClsfCd=GARDEN");
            InitImage("http://221.158.142.142/sios/getPrntImage.do?prntNo=4&imgSn=1&prntInfoClsfCd=GARDEN");

            InitItemsss();
        }

        public static MainViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainViewModel();
                }
                return instance;
            }
        }

        private void InitItemsss()
        {
            CommonName = "";
            KoreanFamilyName = "";
            ScientificName = "";
            FamilyName = "";
            Itemsss = new ObservableCollection<PlantList> { };
            _tempItem.ForEach(x => Itemsss.Add(x));
            _imageList = _tempImage;
        }

        public ICommand ItemDoubleClickCommand { get; set; }
        public ICommand RecvUrl { get; set; }
        public ICommand InitText { get; set; }

        private void OnItemDoubleClick(Tuple<PlantList, int> parameter)
        {
            PlantList selectedItem = parameter.Item1;
            int index = parameter.Item2;
            List<string>? test = new List<string>();
            test.Add(selectedItem.CommonName);
            test.Add(selectedItem.KoreanFamilyName);
            test.Add(selectedItem.ScientificName);
            test.Add(selectedItem.FamilyName);

            List<Data>? result = _connectServ.GetSearch(test);
            if (result is null)
            {
                MessageBox.Show("검색결과가없습니다.");
            }
            else
            {
                SelectCommonName = result[0].Plant[0];
                SelectKoreanFamilyName = result[0].Plant[1];
                SelectScientificName = result[0].Plant[2];
                SelectFamilyName = result[0].Plant[3];
                SelectFloweringPeriod = result[0].Plant[4];
                SelectFlowerColor = result[0].Plant[5];
                SelectInfo = result[0].Plant[6];
                SelectUrl = _imageList[index];
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

        public async void LoadImage(string imageUrl)
        {
            SelectUrl = null;
            BitmapImage bitmap = await DownloadImageAsync(imageUrl);

            if (bitmap != null)
            {
                _imageList.Add(bitmap);
                
            }
        }
        public async void InitImage(string imageUrl)
        {
            SelectUrl = null;
            BitmapImage bitmap = await DownloadImageAsync(imageUrl);

            if (bitmap != null)
            {
                _tempImage.Add(bitmap);
            }
        }

        private void RecvUrl_Click(object parameter)
        {
            List<string>? test = new List<string>();
            test.Add(CommonName);
            test.Add(KoreanFamilyName);
            test.Add(ScientificName);
            test.Add(FamilyName);

            List<Data>? result = _connectServ.GetSearch(test);
            if (result is null)
            {
                MessageBox.Show("검색결과가없습니다.");
            }
            else
            {
                Itemsss.Clear();
                _imageList.Clear();
                foreach (Data item in result)
                {
                    Itemsss.Add(new PlantList
                    {
                        CommonName = item.Plant[0],
                        KoreanFamilyName = item.Plant[1],
                        ScientificName = item.Plant[2],
                        FamilyName = item.Plant[3],
                    });
                    LoadImage(item.Plant[7]);
                }
            }
        }

        private void InitText_Click(object parameter)
        {
            
            InitItemsss();
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public string CommonName
        {
            get => _commonName;
            set => SetProperty(ref _commonName, value);
        }

        public string KoreanFamilyName
        {
            get => _koreanFamilyName;
            set => SetProperty(ref _koreanFamilyName, value);
        }

        public string ScientificName
        {
            get => _scientificName;
            set => SetProperty(ref _scientificName, value);
        }
        public string FamilyName
        {
            get => _familyName;
            set => SetProperty(ref _familyName, value);
        }

        public ObservableCollection<PlantList> Itemsss
        {
            get => _items;
            set => SetProperty(ref _items, value);
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

    }

    public class PlantList
    {
        public string? CommonName { get; set; }
        public string? KoreanFamilyName { get; set; }
        public string? ScientificName { get; set; }
        public string? FamilyName { get; set; }
    }
}
