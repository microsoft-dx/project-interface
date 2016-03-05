﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.Media.Capture;
using Interface.ImageSource;
using Interface.Storage;
using Interface.Interfaces;
using Interface.ImageProcessing;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Interface
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MediaCapture mediaDevice;
        private readonly string PHOTO_FILE_NAME = "photo.jpg";

        private IImageProcessing imageProcessing;
        private IPhotoStorage localPhotoStorage;
        private IImageSource localCameraImageSource;

        public MainPage()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private async void InitializeApplication()
        {
            mediaDevice = new MediaCapture();

            imageProcessing = new ProjectOxford();
            localPhotoStorage = new LocalPhotoStorage(mediaDevice);
            localCameraImageSource = new LocalCameraImageSource(localPhotoStorage, mediaDevice);

            await localCameraImageSource.InitializeDevice();

            interogationResult.Text = "Hello my dear friend !!";
              
            cameraElement.Source = mediaDevice;

            await mediaDevice.StartPreviewAsync();
        }

        private async void GetShirtColorButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.GetDominantForegroundColor(localPhotoStorage.GetLastPhotoSaved());

            interogationResult.Text = string.Format("Your shirt is {0} !!", result);
        }

        private async void ReadMeSomeTextButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await localPhotoStorage.Save(PHOTO_FILE_NAME);

            var result = await imageProcessing.RecognizeText(localPhotoStorage.GetLastPhotoSaved());

            interogationResult.Text = result;
        }

        private void AmIFamiliarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void WhatsMyMoodButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void ResetButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
