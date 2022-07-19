using FinalProject.Models;
using FinalProject.ViewModel;
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
    public partial class ProfilePage : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        HomePageViewModel profilePageVM;
        List<Activities> profilePageList;
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));

        public ProfilePage()
        {
            InitializeComponent();
            profilePageVM = new HomePageViewModel();
            BindingContext = profilePageVM;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            profilePageList = await firebase.GetActivities();
            ppActivityList.BindingContext = profilePageList;
            username.Text = await GetCurrentUserName();

        }

        private async void LogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("myToken");
            await App.Current.MainPage.Navigation.PopAsync();
        }
        private async Task<string> GetCurrentUserName()
        {
            try
            {
                var savedAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("myToken", ""));
                var refreshedContent = await firebaseAuthProvider.RefreshAuthAsync(savedAuth);
                Preferences.Set("myToken", JsonConvert.SerializeObject(refreshedContent));

                return savedAuth.User.DisplayName;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }
        
    }


}