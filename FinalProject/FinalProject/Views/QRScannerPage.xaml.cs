using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScannerPage : ContentPage
    {
        public QRScannerPage()
        {
            InitializeComponent();
        }

        private async void ScannerView_OnScanResult(ZXing.Result result)
        {
            var Result = result.ToString();
            await Navigation.PushAsync(new DuringActivity());
           
        }
    }
}