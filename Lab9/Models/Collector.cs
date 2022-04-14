using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Lab9.Models
{
    public class Collector : TreeObject
    {
        ObservableCollection<Collector> objects;
        public ObservableCollection<Collector> Objects
        {
            get
            {
                return GetInfo(Path);
            }
        }

        public Collector(string path) : base(path)
        {

        }

        public ObservableCollection<Collector> GetInfo(string path)
        {
            var DirObjectrs = new ObservableCollection<Collector>();

            foreach (var currentObject in Directory.GetDirectories(path))
            {
                DirObjectrs.Add(new Collector(currentObject));
            }
            foreach (var currentObject in Directory.GetFiles(path))
            {
                var fileData = new FileInfo(currentObject);
                if(fileData.Extension == ".ico" || fileData.Extension == ".jpg" || fileData.Extension == ".jpeg" || fileData.Extension == ".png")
                {
                    DirObjectrs.Add(new Collector(currentObject));
                }            
            }

            return DirObjectrs;
        }
    }
}
