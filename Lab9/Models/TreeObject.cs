using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace Lab9.Models
{
    public class TreeObject
    {
        public string Path
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public TreeObject(string path)
        {
            Path = path;
            //Name = Path.Substring(Path.LastIndexOf(@"\") + 1);

            var currentObject = new FileInfo(Path);
            if (currentObject.Name.Length != 0)
            {
                Name = currentObject.Name;
            }
            else
            {
                Name = Path;
            }
        }
    }
}
