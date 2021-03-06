﻿using System;
using System.Threading.Tasks;
using Interface.Interfaces;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.Media.Capture;

namespace Interface.Storage
{
    public class LocalPhotoStorage : IPhotoStorage
    {
        public StorageFile ImageFile { get; set; }

        public MediaCapture MediaDevice { get; set; }

        public string LastPhotoSavedPath { get; private set; }

        public LocalPhotoStorage(MediaCapture mediaCapture)
        {
            MediaDevice = mediaCapture;
        }

        public async Task<BitmapImage> GetPhoto(string filename)
        {
            var image = new BitmapImage();
            var photoStream = await ImageFile.OpenReadAsync();

            image.SetSource(photoStream);

            return image;
        }

        public async Task Save(string filename)
        {
            var imageProperties = ImageEncodingProperties.CreateJpeg();

            ImageFile = await KnownFolders.PicturesLibrary.CreateFileAsync(
                    filename, CreationCollisionOption.ReplaceExisting);

            await MediaDevice.CapturePhotoToStorageFileAsync(imageProperties, ImageFile);

            LastPhotoSavedPath = ImageFile.Path;
        }

        public string GetLastPhotoSaved()
        {
            return LastPhotoSavedPath;
        }
    }
}
