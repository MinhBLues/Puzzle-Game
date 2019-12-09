using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PuzzleGame
{
    public class Game
    {
        private int size = 3;
        private int time = 60;
        private int width = 150;
        private int height =150;
        private int startX;
        private int startY;
        private int[,] check;
        private int inmoves = 0;
        private int inNullSliceIndex;
        private static Game instance;
        private List<ImageGame> imagesList = new List<ImageGame>();
        
        public Game() { }
        public int Size { get => size; set => size = value; }
        public int Time { get => time; set => time = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int[,] Check { get => check; set => check = value; }
        public static Game Instance
        {
            get { if (instance == null) instance = new Game(); return instance; }
            set => instance = value;
        }
        public int StartX { get => startX; set => startX = value; }
        public int StartY { get => startY; set => startY = value; }
        public int Inmoves { get => inmoves; set => inmoves = value; }
        public List<ImageGame> ImagesList { get => imagesList; set => imagesList = value; }
        public int InNullSliceIndex { get => inNullSliceIndex; set => inNullSliceIndex = value; }

        public void Shuffle()
        {
            bool isSuccess = true;
            try
            {
                int j;
                List<int> Indexes = new List<int>();
                for (int i = 0; i < size * size; i++)
                {
                    Indexes.Add(i);
                }
                Random r = new Random();
                for (int i = 0; i < size * size; i++)
                {
                    Indexes.Remove((j = Indexes[r.Next(0, Indexes.Count)]));
                    check[i / size, i - (i / size) * size] = j;
                    if (j == size * size - 1)
                    {
                        inNullSliceIndex = i;
                    }
                }
            }
            catch
            {
                isSuccess = false;
            }
        }
        public void split(BitmapImage source)
        {
            ImagesList.Clear();
            check = new int[size, size];
            int l = 600;
            switch (size)
            {
                case 3:
                    width = height = 150;
                    break;
                case 6:
                    width = height = 70;
                    break;
                case 9:
                    width = height = 45;
                    break;
            }
            startX = (l - (width + 2) * size) / 2;
            startY = (500 - (height + 2) * size) / 2;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var h = (int)source.PixelHeight / size;
                    var w = (int)source.PixelWidth / size;
                    var rect = new Int32Rect(j * w, i * h, w, h);
                    var cropBitmap = new CroppedBitmap(source, rect);
                    var cropImage = new Image();
                    cropImage.Stretch = Stretch.Fill;
                    cropImage.Width = width;// width;
                    cropImage.Height = height;// height;
                    cropImage.Source = cropBitmap;
                    RenderOptions.SetBitmapScalingMode(cropImage, BitmapScalingMode.HighQuality);
                    cropImage.Tag = new Tuple<int, int>(i, j);
                    ImagesList.Add(new ImageGame(cropImage, startX + j * (width + 2), startY + i * (height + 2), i * size + j));
                    check[i, j] = j + i * size;
                }
            }
        }
        public bool CheckWin()
        {
            int i;
            for (i = 0; i < Game.Instance.Size; i++)
            {
                for (int j = 0; j < Game.Instance.Size; j++)
                {
                    if (Game.Instance.Check[i, j] != i * Game.Instance.Size + j)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void SaveGame(BitmapImage source)
        {
            const string filename = "SaveGame.txt";
            bool isSuccess = true;
            try
            {
                var writer = new StreamWriter(filename);
                // Dong dau tien la luot di hien tai
                writer.WriteLine($"{Game.Instance.Size}"); //  kích thước
                writer.WriteLine($"{Game.Instance.Inmoves}");// số bước đã đi
                writer.WriteLine($"{Game.Instance.Time}");// thời gian 
                writer.WriteLine($"{source}");// Link Anh`
                                              // Theo sau la ma tran bieu dien game
                for (int i = 0; i < Game.Instance.Size; i++)
                {
                    for (int j = 0; j < Game.Instance.Size; j++)
                    {
                        writer.Write($"{Game.Instance.Check[i, j]}");
                        writer.Write(" ");
                    }
                    writer.WriteLine("");
                }
                writer.Close();
            }
            catch
            {
                isSuccess = false;
            }
            if (isSuccess)
            {
                MessageBox.Show("Save successfully!");
            }
            else
            {
                MessageBox.Show("Save failed!");
            }
        }
    }

}
