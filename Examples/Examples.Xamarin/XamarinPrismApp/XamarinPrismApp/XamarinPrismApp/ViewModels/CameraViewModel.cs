using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Dialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.IO;
using Xamarin.Forms;

namespace XamarinPrismApp.ViewModels
{
    public class CameraViewModel : ViewModelBase
    {
        private readonly IApplicationCoreService applicationCoreService;
        private readonly IDialogsService dialogsService;
        private ImageSource image;

        public CameraViewModel(INavigationService navigationService, IDialogsService dialogsService,
            IApplicationCoreService applicationCoreService)
            : base(navigationService)
        {
            Title = "Camera";
            this.dialogsService = dialogsService;
            this.applicationCoreService = applicationCoreService;
        }

        public DelegateCommand FrontCameraCommand => new DelegateCommand(() => TakePicture(CameraDevice.Front));

        public ImageSource Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public DelegateCommand RearCameraCommand => new DelegateCommand(() => TakePicture(CameraDevice.Rear));

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            var files = Directory.GetFiles(Path.Combine(applicationCoreService.PrivateExternalFolder, "Pictures"));
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        private async void TakePicture(CameraDevice camera)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    dialogsService.Toast("No Camera");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",
                    DefaultCamera = camera
                });

                if (file != null)
                {
                    Image = ImageSource.FromStream(() => file.GetStream());
                    //File.Delete(file.Path);
                }
            }
            catch (Exception exc)
            {
                dialogsService.Toast(exc.Message);
            }
        }
    }
}