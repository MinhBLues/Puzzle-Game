using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void split()
        {
            Image img;
            int widthThird = (int)((double)img.Width / 3.0 + 0.5);
            int heightThird = (int)((double)img.Height / 3.0 + 0.5);
            Bitmap[,] bmps = new Bitmap[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new System.Drawing.Rectangle(0, 0, widthThird, heightThird), new System.Drawing.Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();
                }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

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
    }
}
