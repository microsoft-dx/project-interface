﻿using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Interface.Interfaces
{
    public interface IPhotoStorage
    {
        Task Save(string filename);

        Task<BitmapImage> GetPhoto(string filename);

        string GetLastPhotoSaved();
    }
}