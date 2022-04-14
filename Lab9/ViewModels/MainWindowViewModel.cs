using System;
using System.Collections.Generic;
using System.Text;
using Lab9.Models;
using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive;
using System.IO;
using Avalonia.Media.Imaging;

namespace Lab9.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ObservableCollection<Collector> tree;
        public ObservableCollection<Collector> Tree
        {
            get
            {
                return tree;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref tree, value);
            }
        }
        public MainWindowViewModel()
        {
            Tree = new ObservableCollection<Collector>();
            foreach (DriveInfo currentDisk in DriveInfo.GetDrives())
            { 
                Tree.Add(new Collector(currentDisk.Name));
            }
        }

        Collector selectedObject;
        public Collector SelectedObject
        {
            get
            {
                return selectedObject;
            }
            set
            { 
                this.RaiseAndSetIfChanged(ref selectedObject, value);
                ChangeImage();
            }
        }

        Bitmap image;
        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref image, value);
            }
        }

        string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref this.imagePath, value);
            }
        }

        public void ChangeImage()
        {
            try
            {
                Image = new Bitmap(SelectedObject.Path);
                ImagePath = SelectedObject.Path;
                UpdateImagesDirectory();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        int ImageIndex;
        List<string> ImagesList = new List<string>();

        bool isLeftOn;
        public bool IsLeftOn
        {
            get
            {
                return isLeftOn;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref isLeftOn, value);
            }
        }

        bool isRightOn;
        public bool IsRightOn
        {
            get
            {
                return isRightOn;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref isRightOn, value);
            }
        }

        public void UpdateImagesDirectory()
        {
            ImagesList.Clear();
            foreach (var currentObject in Directory.GetFiles(Directory.GetParent(SelectedObject.Path).FullName))
            {
                int amount = 0;
                var fileData = new FileInfo(currentObject);
                if (fileData.Extension == ".ico" || fileData.Extension == ".jpg" || fileData.Extension == ".jpeg" || fileData.Extension == ".png")
                {
                    if (currentObject == SelectedObject.Path)
                    {
                        ImageIndex = amount;
                    }
                    ImagesList.Add(currentObject);
                    ++amount;
                }
            }
            if (ImagesList.Count > 1)
            {
                IsLeftOn = true;
                IsRightOn = true;
            }
        }

        public void ImageMovePrev()
        {
            try
            {
                --ImageIndex;
                if (ImageIndex < 0)
                {
                    ImageIndex = 0;
                    IsLeftOn = false;      
                }

                IsRightOn = true;

                Image = new Bitmap(ImagesList[ImageIndex]);
                ImagePath = ImagesList[ImageIndex];
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void ImageMoveNext()
        {
            try
            {
                ++ImageIndex;
                if (ImageIndex >= ImagesList.Count - 1)
                {
                    ImageIndex = ImagesList.Count - 1;
                    IsRightOn = false;
                }

                IsLeftOn = true;

                Image = new Bitmap(ImagesList[ImageIndex]);
                ImagePath = ImagesList[ImageIndex];
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
