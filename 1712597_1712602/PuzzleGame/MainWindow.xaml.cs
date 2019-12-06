using Microsoft.Win32;
using System;
using System.Collections.Generic;
//using System.Drawing;
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
using System.Windows.Threading;

namespace PuzzleGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int time = 120;
        private int size = 3;

        //int[,] model;
        //Button[,] buttons;
        public MainWindow()
        {

            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
        }


        const int startX = 30;
        const int startY = 30;
        const int width = 100;
        const int height = 100;
        private void split()
        {
            System.Drawing.Image img;
            //int widthThird = (int)((double)img.Width / 3.0 + 0.5);
            //int heightThird = (int)((double)img.Height / 3.0 + 0.5);
            //Bitmap[,] bmps = new Bitmap[3, 3];
            //for (int i = 0; i < 3; i++)
            //    for (int j = 0; j < 3; j++)
            //    {
            //        bmps[i, j] = new Bitmap(widthThird, heightThird);
            //        Graphics g = Graphics.FromImage(bmps[i, j]);
            //        g.DrawImage(img, new System.Drawing.Rectangle(0, 0, widthThird, heightThird), new System.Drawing.Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
            //        g.Dispose();
            //    }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            NewImageListView();
        }

        ImageClass images;

        private void NewImageListView()
        {
            images = new ImageClass();
            ImageListView.ItemsSource = images.Images;
            ImageListView.SelectedIndex = 0;
        }
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
            if (time > 0)
            {

                time--;
                lblTime.Content = $"00:0{time / 60}:{time % 60}";
            }
            else
            {
                lblTime.Content = "00:00:00";
            }
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            if (btnCheck1.IsChecked == true)
            {
                time = 60;
                size = 3;
            }
            else if (btnCheck2.IsChecked == true)
            {
                time = 120;
                size = 6;
            }
            else if (btnCheck3.IsChecked == true)
            {
                time = 180;
                size = 9;
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;

                //đưa hình ảnh đã chọn vào listview
                updateListView(filename);
            }
            else
            {

            }
        }

        private void updateListView(string newImageLink)
        {
            images.Images.Add(newImageLink);
        }


        private void ImageListView_Click(object sender, MouseButtonEventArgs e)
        {
            //   MessageBox.Show(e.GetPosition(this).ToString());
            var filename = ImageListView.SelectedItem as string;
            //   imageDisplay.Source = filename;


            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(filename);
            source.CacheOption = BitmapCacheOption.OnLoad;
            source.EndInit();
            imageDisplay.Source = source;
            //    MessageBox.Show(filename);

            //previewImage.Width = 300;
            //previewImage.Height = 240;
            //previewImage.Source = source;

            //Canvas.SetLeft(previewImage, 200);
            //Canvas.SetTop(previewImage, 20);


            //crop image
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!((i == 2) && (j == 2)))
                    {
                        var h = (int)source.PixelHeight / 3;
                        var w = (int)source.PixelWidth / 3;
                        //Debug.WriteLine($"Len = {len}");
                        var rect = new Int32Rect(j * w, i * h, w, h);
                        var cropBitmap = new CroppedBitmap(source,
                            rect);

                        var cropImage = new Image();
                        cropImage.Stretch = Stretch.Fill;
                        cropImage.Width = width;
                        cropImage.Height = height;
                        cropImage.Source = cropBitmap;
                        uiListView.Children.Add(cropImage);
                        Canvas.SetLeft(cropImage, startX + j * (width + 2));
                        Canvas.SetTop(cropImage, startY + i * (height + 2));

                        cropImage.MouseLeftButtonDown += CropImage_MouseLeftButtonDown;
                        cropImage.PreviewMouseLeftButtonUp += CropImage_PreviewMouseLeftButtonUp;
                        cropImage.Tag = new Tuple<int, int>(i, j);
                        //cropImage.MouseLeftButtonUp
                    }
                }
            }
        }

        private void CropImage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CropImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           throw new NotImplementedException();
        }

        private void ImageListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void previewImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
