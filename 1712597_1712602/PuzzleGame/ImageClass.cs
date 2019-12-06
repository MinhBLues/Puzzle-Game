using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame
{

    public class ImageClass
    {
        private string foldername;

        public ImageClass(string folder = @"Images\")
        {
            foldername = folder;
            LoadImageName();
        }


        public void LoadImageName()
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + foldername;
            string[] filenames = Directory.GetFiles(path);

            foreach (var fullname in filenames)
            {
                string filename = Path.GetFileName(fullname);

                string[] tokens = filename.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

                Images.Add(path + filename);
            }
        }


        public BindingList<string> Images { get; set; } = new BindingList<string>();

    }



}
