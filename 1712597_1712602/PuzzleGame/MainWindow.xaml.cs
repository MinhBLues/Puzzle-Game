using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class MainWindow : Window
    {
        Icons icons;
        private DispatcherTimer timer = new DispatcherTimer();
        BitmapImage source;
        public MainWindow()
        {
            InitializeComponent();
            icons = new Icons();
        }
       
        bool _isDragging = false;
        Image _selectedBitmap = null;
        Point _lastPosition;
        int lastX;
        int lastY;
        private void MouseRight_Click(object sender, MouseButtonEventArgs e)
        {
            isTimeStop = true;
            btnPlay.Text = "Pause";
            imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[8]}"));

            var position = e.GetPosition(this);

            int x = (int)(position.X - Game.Instance.StartX - 6) / (Game.Instance.Width + 2);
            int y = (int)(position.Y - Game.Instance.StartY - 68) / (Game.Instance.Height + 2);
            int inImageIndex = y * Game.Instance.Size + x;
            int indexselect = canvasGame.Children.IndexOf(sender as UIElement);
            Tuple<int, int> index = (Tuple<int, int>)(sender as Image).Tag;
            if (Game.Instance.InNullSliceIndex != inImageIndex)
            {
                timer.Start();
                List<int> FourBrothers = new List<int>(new int[] { ((inImageIndex % Game.Instance.Size == 0) ? -1 : inImageIndex - 1), inImageIndex - Game.Instance.Size, (inImageIndex % Game.Instance.Size == Game.Instance.Size - 1) ? -1 : inImageIndex + 1, inImageIndex + Game.Instance.Size });
                if (FourBrothers.Contains(Game.Instance.InNullSliceIndex))
                {
                    Game.Instance.Check[y, x] = Game.Instance.Size * Game.Instance.Size - 1;
                    int i = Game.Instance.InNullSliceIndex / Game.Instance.Size;
                    int j = Game.Instance.InNullSliceIndex - i * Game.Instance.Size;
                    Game.Instance.Check[i, j] = index.Item1 * Game.Instance.Size + index.Item2;
                    Canvas.SetLeft((Image)canvasGame.Children[indexselect], Game.Instance.StartX + j * (Game.Instance.Width + 2));
                    Canvas.SetTop((Image)canvasGame.Children[indexselect], Game.Instance.StartY + i * (Game.Instance.Height + 2));
                    Game.Instance.InNullSliceIndex = inImageIndex;
                    lblMove.Content = "Moves Made : " + (++Game.Instance.Inmoves).ToString();
                }
            }
        }
        private void MouseLeft_Up(object sender, MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
                var position = e.GetPosition(this);
                int i = (int)(position.X - Game.Instance.StartX - 6) / (Game.Instance.Width + 2);
                int j = (int)(position.Y - Game.Instance.StartY - 68) / (Game.Instance.Height + 2);
                int inImageIndex = lastY * Game.Instance.Size + lastX;
                List<int> FourBrothers = new List<int>(new int[] { ((inImageIndex % Game.Instance.Size == 0) ? -1 : inImageIndex - 1), inImageIndex - Game.Instance.Size, (inImageIndex % Game.Instance.Size == Game.Instance.Size - 1) ? -1 : inImageIndex + 1, inImageIndex + Game.Instance.Size });

                if (FourBrothers.Contains(Game.Instance.InNullSliceIndex) && FourBrothers.Contains(j * Game.Instance.Size + i) && Game.Instance.Check[j, i] == Game.Instance.Size * Game.Instance.Size - 1)
                {
                    Canvas.SetLeft(_selectedBitmap, Game.Instance.StartX + i * (Game.Instance.Width + 2));
                    Canvas.SetTop(_selectedBitmap, Game.Instance.StartY + j * (Game.Instance.Height + 2));
                    Game.Instance.InNullSliceIndex = inImageIndex;
                    Game.Instance.Check[j, i] = Game.Instance.Check[lastY, lastX];
                    Game.Instance.Check[lastY, lastX] = Game.Instance.Size * Game.Instance.Size - 1;
                }
                else
                {
                    Canvas.SetLeft(_selectedBitmap, Game.Instance.StartX + lastX * (Game.Instance.Width + 2));
                    Canvas.SetTop(_selectedBitmap, Game.Instance.StartY + lastY * (Game.Instance.Height + 2));
                }

            }
        }
        private void MouseLeft_Down(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            lblMove.Content = "Moves Made : " + (++Game.Instance.Inmoves).ToString();
            _isDragging = true;
            _selectedBitmap = sender as Image;
            _lastPosition = e.GetPosition(this);
            lastX = (int)(_lastPosition.X - Game.Instance.StartX - 6) / (Game.Instance.Width + 2);
            lastY = (int)(_lastPosition.Y - Game.Instance.StartY - 68) / (Game.Instance.Height + 2);
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);
            int i = ((int)position.Y - Game.Instance.StartY - 68) / (Game.Instance.Height + 2);
            int j = ((int)position.X - Game.Instance.StartX - 6) / (Game.Instance.Width + 2);
            if (_isDragging)
            {
                var dx = position.X - _lastPosition.X;
                var dy = position.Y - _lastPosition.Y;
                var lastLeft = Canvas.GetLeft(_selectedBitmap);
                var lastTop = Canvas.GetTop(_selectedBitmap);
                Canvas.SetLeft(_selectedBitmap, lastLeft + dx);
                Canvas.SetTop(_selectedBitmap, lastTop + dy);
                _lastPosition = position;
                if ((int)position.X > 600 || (int)position.X < 1 || (int)position.Y < 68 || (int)position.Y > 550)
                {
                    Canvas.SetLeft(_selectedBitmap, Game.Instance.StartX + lastX * (Game.Instance.Width + 2));
                    Canvas.SetTop(_selectedBitmap, Game.Instance.StartY + lastY * (Game.Instance.Height + 2));
                    _isDragging = false;
                }
            }
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Icon = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[14]}"));
            var src = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[15]}"));
            Original.Source = src;
            source = src;
            Game.Instance.split(source);
            Shuffle();
            this.DataContext = icons;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            lblTime.Content = $"00:0{Game.Instance.Time / 60}:0{Game.Instance.Time % 60}";
            tbHelp.Text = "1712597 - Phạm Bá Minh           : 1712597@student.hcmus.edu.vn\n" + "1712602 - Nguyễn Thị Cẩm My  : 1712602@student.hcmus.edu.vn";
        }
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            canvasGame.Children.Clear();
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                bool isSuccess = true;
                try
                {
                    var filename = screen.FileName;

                    var reader = new StreamReader(filename);
                    var firstLine = reader.ReadLine();
                    Game.Instance.Size = int.Parse(firstLine);
                    firstLine = reader.ReadLine();
                    Game.Instance.Inmoves = int.Parse(firstLine);
                    firstLine = reader.ReadLine();
                    Game.Instance.Time = int.Parse(firstLine);
                    firstLine = reader.ReadLine();
                    Game.Instance.Check = new int[Game.Instance.Size, Game.Instance.Size];
                    var src = new BitmapImage(
                       new Uri(firstLine));
                    Original.Source = src;
                    source = src;
                    Game.Instance.split(source);
                    for (int i = 0; i < Game.Instance.Size; i++)
                    {
                        var tokens = reader.ReadLine().Split(
                            new string[] { " " }, StringSplitOptions.None);
                        for (int j = 0; j < Game.Instance.Size; j++)
                        {
                            int k;
                            k = Game.Instance.Check[i, j] = int.Parse(tokens[j]);
                            if (k != Game.Instance.Size * Game.Instance.Size - 1)
                            {
                                Game.Instance.ImagesList[k].Img.MouseLeftButtonDown += MouseLeft_Down;
                                Game.Instance.ImagesList[k].Img.PreviewMouseLeftButtonUp += MouseLeft_Up;
                                Game.Instance.ImagesList[k].Img.MouseRightButtonDown += MouseRight_Click;
                                canvasGame.Children.Add(Game.Instance.ImagesList[k].Img);
                                Canvas.SetLeft(Game.Instance.ImagesList[k].Img, Game.Instance.ImagesList[i * Game.Instance.Size + j].X);
                                Canvas.SetTop(Game.Instance.ImagesList[k].Img, Game.Instance.ImagesList[i * Game.Instance.Size + j].Y);
                            }
                            else
                            {
                                Game.Instance.InNullSliceIndex = j;
                            }
                        }
                    }
                    reader.Close();
                   
                }
                catch
                {
                    isSuccess = false;
                }
                if (isSuccess)
                {
                    lblTime.Content = $"00:0{Game.Instance.Time / 60}:{Game.Instance.Time % 60}";
                    lblMove.Content = $"Moves made: {Game.Instance.Inmoves}";
                    isTimeStop = false;
                    btnPlay.Text = "Play";
                    imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[7]}"));
                    MessageBox.Show("Load game successfully!");
                    timer.Stop();
                    if (Game.Instance.CheckWin())
                    {
                        MessageBox.Show("You Win!!!","Puzzle Game");
                    }
                }
                else
                {
                    MessageBox.Show("Load game failed!");
                }
            }
           
         
        }
        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                bool isSuccess = true;
                try
                {
                    var src = new BitmapImage(new Uri(screen.FileName, UriKind.Absolute));
                    Original.Source = src;
                    source = src;
                    Game.Instance.split(source);
                    Shuffle();
                }
                catch
                {
                    isSuccess = false;
                }
                if (isSuccess)
                {
                    GameReset();
                    MessageBox.Show("Load images successfully!");
                }
                else
                {
                    MessageBox.Show("Load images failed!");
                }
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Game.Instance.SaveGame(source);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
            if (Game.Instance.Time > 0)
            {

                Game.Instance.Time--;
                if (Game.Instance.Time % 60 < 10) 
                {
                    lblTime.Content = $"00:0{Game.Instance.Time / 60}:0{Game.Instance.Time % 60}";
                }
               else
                    lblTime.Content = $"00:0{Game.Instance.Time / 60}:{Game.Instance.Time % 60}";
            }
            else
            {
                if (!(Game.Instance.CheckWin()))
                {
                    timer.Stop();
                    lblTime.Content = "00:00:00";
                    MessageBoxResult result = MessageBox.Show("Time Out !!! Do You Want To Play New Game?", "Puzzle Game", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            GameReset();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                    return;
                }
            }
            if (Game.Instance.CheckWin())
            {
                timer.Stop();
                int time = Game.Instance.Size == 3 ?60 - Game.Instance.Time : Game.Instance.Size * 60 - Game.Instance.Time;
                MessageBoxResult result = MessageBox.Show($"You Win !!! Do You Want To Play New Game?\n Time: {time} \n Moves made: {Game.Instance.Inmoves}", "Puzzle Game", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        GameReset();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
        }
        private void btn_click(object sender, RoutedEventArgs e)
        {
            if (btnCheck1.IsChecked == true) Game.Instance.Size = 3;
            else if (btnCheck2.IsChecked == true) Game.Instance.Size = 6;
            else if (btnCheck3.IsChecked == true) Game.Instance.Size = 9;
            GameReset();
            try
            {
                Game.Instance.split(source);
                Shuffle();
            }
            catch { }
        }
        bool isTimeStop = false;
        private void pause_click(object sender, RoutedEventArgs e)
        {
            if (isTimeStop)
            {
                btnPlay.Text = "Play";
                imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[7]}"));
                isTimeStop = false;
                timer.Stop();
            }
            else
            {
                isTimeStop = true;
                btnPlay.Text = "Pause";
                timer.Start();
                imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[8]}"));
                
            }
        }
        void Shuffle()
        {
            try
            {
                canvasGame.Children.Clear();
                Game.Instance.Shuffle();
                for (int i = 0; i < Game.Instance.Size; i++)
                {
                    for (int j = 0; j < Game.Instance.Size; j++)
                    {
                        int k = Game.Instance.Check[i, j];
                        if (k != Game.Instance.Size * Game.Instance.Size - 1)
                        {
                            Game.Instance.ImagesList[k].Img.MouseLeftButtonDown += MouseLeft_Down;
                            Game.Instance.ImagesList[k].Img.PreviewMouseLeftButtonUp += MouseLeft_Up;
                            Game.Instance.ImagesList[k].Img.MouseRightButtonDown += MouseRight_Click;
                            canvasGame.Children.Add(Game.Instance.ImagesList[k].Img);
                            Canvas.SetLeft(Game.Instance.ImagesList[k].Img, Game.Instance.ImagesList[i * Game.Instance.Size + j].X);
                            Canvas.SetTop(Game.Instance.ImagesList[k].Img, Game.Instance.ImagesList[i * Game.Instance.Size + j].Y);
                        }
                    }
                }
            }
            catch { }
        }
        void GameReset()
        {
            Game.Instance.Time = Game.Instance.Size == 3 ? 60 : 60 * Game.Instance.Size;
            lblTime.Content = $"00:0{Game.Instance.Time / 60}:0{Game.Instance.Time % 60}";
            lblMove.Content = "Moves made: 0";
            Game.Instance.Inmoves = 0;
            Shuffle();
            timer.Stop();
            isTimeStop = false;
            btnPlay.Text = "Play";
            imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[7]}"));
        }
        private void shuffle_click(object sender, RoutedEventArgs e)
        {
            try
            {
                GameReset();
                Shuffle();
                Game.Instance.Inmoves = 0;
            }
            catch
            {

            }
        }
        private void quit_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Save Game ?", "Puzzle Game", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Game.Instance.SaveGame(source);
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    this.Close();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }
        #region Button key
        private void KeyUp_Click(object sender, RoutedEventArgs e)
        {
            MoveKey(1, 0);
        }
        private void KeyLeft_Click(object sender, RoutedEventArgs e)
        {
            MoveKey(0, 1);
        }
        private void KeyRight_Click(object sender, RoutedEventArgs e)
        {
            MoveKey(0, -1);
        }
        private void KeyDown_Click(object sender, RoutedEventArgs e)
        {
            MoveKey(-1, 0);
        }
        void MoveKey(int indexI, int indexY)
        {
            for (int i = 0; i < Game.Instance.Size; i++)
            {
                for (int j = 0; j < Game.Instance.Size; j++)
                {
                    if (Game.Instance.Check[i, j] == Game.Instance.Size * Game.Instance.Size - 1)
                    {
                        isTimeStop = true;
                        btnPlay.Text = "Pause";
                        imgPlay.Source = new BitmapImage(new Uri($"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{icons.icon[8]}"));
                        int k = i + indexI;
                        int l = j + indexY;
                        if (k < Game.Instance.Size && l < Game.Instance.Size && k >= 0 && l >= 0)
                        {
                            timer.Start();
                            int index = canvasGame.Children.IndexOf(Game.Instance.ImagesList[Game.Instance.Check[k, l]].Img);
                            Canvas.SetLeft((Image)canvasGame.Children[index], Game.Instance.StartX + j * (Game.Instance.Width + 2));
                            Canvas.SetTop((Image)canvasGame.Children[index], Game.Instance.StartY + i * (Game.Instance.Height + 2));
                            Game.Instance.InNullSliceIndex = k * Game.Instance.Size + l;
                            Game.Instance.Check[i, j] = Game.Instance.Check[k, l];
                            Game.Instance.Check[k, l] = Game.Instance.Size * Game.Instance.Size - 1;
                            lblMove.Content = "Moves Made : " + (++Game.Instance.Inmoves).ToString();
                          
                            return;
                        }
                    }
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) MoveKey(1, 0);
            if (e.Key == Key.Down) MoveKey(-1, 0);
            if (e.Key == Key.Left) MoveKey(0, 1);
            if (e.Key == Key.Right) MoveKey(0, -1);
        }
        #endregion

    }
}
