using BsodSimulator.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BsodSimulator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class BsodPage : Page
    {
        public ViewModel.MainPageVM VM { get; set; }

        public BsodPage()
        {
            this.InitializeComponent();
        }

        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {
            var vm = e.Parameter as MainPageVM;
            VM = vm;
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            base.OnNavigatedTo(e);
            ListenForProgress();
        }

        private async Task ListenForProgress()
        {
            if (!VM.DynamicPercentage)
            {
                return;
            }
            await VM.UpdateProgress();
            this.Frame.GoBack();
            this.Frame.Navigate(typeof(RestartPage), VM);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ApplicationView.GetForCurrentView().ExitFullScreenMode();
            base.OnNavigatedFrom(e);
        }
    }
}
