using BsodSimulator.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace BsodSimulator.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestartPage : Page
    {
        public MainViewModel VM { get; set; }

        private readonly App _app;

        public RestartPage()
        {
            this.InitializeComponent();
            _app = Application.Current as App;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            VM = e.Parameter as MainViewModel;

            Frame.BackStack.RemoveAt(1);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _app.ExitFullScreen();
            base.OnNavigatedFrom(e);
        }
    }
}
