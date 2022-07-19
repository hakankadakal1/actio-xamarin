using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCodePage : ContentPage
    {
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        public QRCodePage()
        {
            InitializeComponent();
        }

        private async void scannerClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new QRScannerPage());

        }
        private async void setBarcodeValue()
        {
            var id = await GetCurrentInfo();
            barcodeValue.BarcodeValue = id;

        }
        private async Task<string> GetCurrentInfo()
        {
            try
            {
                var savedAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("token", ""));
                var refreshedContent = await firebaseAuthProvider.RefreshAuthAsync(savedAuth);
                Preferences.Set("token", JsonConvert.SerializeObject(refreshedContent));

                return savedAuth.User.LocalId;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }

    }
}