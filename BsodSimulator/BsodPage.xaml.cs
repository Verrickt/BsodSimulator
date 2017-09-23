﻿using BsodSimulator.ViewModel;
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
using Windows.UI.Core;
using System.Threading;

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

        private App App { get; set; }

        CancellationTokenSource cts;

        public BsodPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            App = Application.Current as App;
        }

        protected override  void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.New)
            {
                var vm = e.Parameter as MainPageVM;
                cts = new CancellationTokenSource();
                VM = vm;
                App.DisableCursor();
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                Bindings.Update();
                ListenForProgressChange(cts.Token);
            }
            base.OnNavigatedTo(e);

        }

        private async Task ListenForProgressChange(CancellationToken t)
        {
            if (!VM.DynamicPercentage)
            {
                return;
            }
            try
            {
                await VM.UpdateProgress(t);
            }
            catch (TaskCanceledException)
            {
                return;
            }
            if (VM.RestartUponComplete)
            {
                this.Frame.Navigate(typeof(RestartPage), VM);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (e.NavigationMode==NavigationMode.Back)
            {
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
                App.EnableCursor();
                cts.Cancel();
                base.OnNavigatedFrom(e);
            }
           
        }
    }
}
