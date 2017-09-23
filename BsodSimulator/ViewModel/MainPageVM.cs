using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Reflection;
using Windows.UI.Xaml.Media;
using System.Threading;
using BsodSimulator.Util;
using Windows.UI.Xaml.Controls;
using BsodSimulator.Model;
using Windows.UI.Xaml.Media.Imaging;
using BsodSimulator.Service;

namespace BsodSimulator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MyColor SelectedColor
        {
            get { return selectedColor; }
            set { SetProperty(ref selectedColor, value,callback:async () => Bitmap = await QRCodeService.GenerateQRCodeAsync(Url, SelectedColor)); }
        }

        private MyColor selectedColor;

        private string emoji;

        public string Emoji
        {
            get { return emoji; }
            set { SetProperty(ref emoji, value); }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private WriteableBitmap _bitmap;

        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }
            set { SetProperty(ref _bitmap, value); }
        }


        private int percent;

        public int Percentage
        {
            get { return percent; }
            set { SetProperty(ref percent, value); }
        }


        private bool dynamicPercentage;

        public bool DynamicPercentage
        {
            get { return dynamicPercentage; }
            set { SetProperty(ref dynamicPercentage, value,
                callback: () => Percentage = 0); }
        }


        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value, callback: async() =>
              Bitmap=await QRCodeService.GenerateQRCodeAsync(Url, SelectedColor)
            ); }
        }


        private string stopCode;

        public string StopCode
        {
            get { return stopCode; }
            set { SetProperty(ref stopCode, value); }
        }

        public List<MyColor> MyColors;

        private bool classicBSOD;

        public bool ClassicBSOD
        {
            get { return classicBSOD; }
            set
            {
                SetProperty(ref classicBSOD, value);
                if (classicBSOD)
                {
                    SelectedColor = MyColor.GetColorByName("DodgerBlue");
                }
            }
        }

        private bool insiderGSOD;

        public bool InsiderGSOD
        {
            get { return insiderGSOD; }
            set
            {
                SetProperty(ref insiderGSOD, value);
                if (InsiderGSOD)
                {
                    SelectedColor = MyColor.GetColorByName("LimeGreen");
                }
            }
        }

        public bool RestartUponComplete { get; set; }


        public RelayCommand<Frame> GoToBSODPageCommand { get; set; }


        public MainViewModel()
        {
            MyColors = new List<MyColor>(MyColor.GetColors());

            Emoji = ":(";

            Description = "Your PC ran into a problem and needs to restart. We're just collecting some error info, and then we'll restart for you";

            Percentage = 0;


            StopCode = "KERNEL_MODE_HEAP_CORRUPTION";

            ClassicBSOD = true;

            DynamicPercentage = true;

            RestartUponComplete = true;

            GoToBSODPageCommand = new RelayCommand<Frame>(
                f => f.Navigate(typeof(BsodPage), this));

            Url = "http://windows.com/stopcode";

        }
        public async Task UpdateProgress(CancellationToken t)
        {
            int progress = Percentage;
            try
            {

                await Task.Delay(1000, t);//wait until navigation finishes

                Random r = new Random();
                if (!DynamicPercentage)
                {
                    return;
                }
                while (percent < 100)
                {
                    if (r.Next() % 2 != 0)
                    {
                        int interval = r.Next(1000, 2000);
                        await Task.Delay(interval, t);
                    }
                    else
                    {
                        int step = r.Next(5, 15);
                        int interval = r.Next(500, 1000);
                        await Task.Delay(interval, t);
                        Percentage += step;
                    }

                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            finally
            {
                Percentage = progress;
            }

        }

    }

  
}
