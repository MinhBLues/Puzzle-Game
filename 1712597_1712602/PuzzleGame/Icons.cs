using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame
{
    public class Icons
    {
        public Icons()
        {
            GetAll();
        }
        public List<string> icon { get; set; } = new List<string>();

        public void GetAll()
        {
            var lines = File.ReadAllLines(@"Icons\Icons.txt");
            var count = lines.Count();
            if (count > 0)
            {
                for (int i = 0; i < count ; i++) 
                {
                    var temp =  lines[i] ; ///$"{ AppDomain.CurrentDomain.BaseDirectory}Icons\\{lines[i]}";
                    icon.Add(temp);
                }
            }
        }
    }
}
