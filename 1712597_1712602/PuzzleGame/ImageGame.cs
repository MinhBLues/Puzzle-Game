using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PuzzleGame
{
    public class ImageGame
    {
        private int x;
        private int y;
        private Image img;
        private int index;
        public Image Img { get => img; set => img = value; }
        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }
        public int Index { get => index; set => index = value; }

        public ImageGame() { }

        public ImageGame(Image img, int x, int y, int index)
        {
            this.x = x;
            this.y = y;
            this.img = img;
            this.index = index;
        }

    }
}
