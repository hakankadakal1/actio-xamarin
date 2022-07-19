using FinalProject.Models;
using FinalProject.ViewModel;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    public partial class HomePage : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        List<Activities> activityList;
        HomePageViewModel homePageVM;
        FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        FirebaseAuth firebaseAuth = new FirebaseAuth();
        
       
        public HomePage()
        {
            InitializeComponent();
            homePageVM = new HomePageViewModel();
            BindingContext = homePageVM;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            activityList = await firebase.GetActivities();
            myActivityList.BindingContext = activityList;
        }
                
        async private void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void CarouselViewClicked(object sender, EventArgs e)
        {
            try
            {
                var ans = await DisplayAlert("Activity Request", "Are you sure you want to send a request ?", "Yes", "No");
                if (ans == true)
                {
                    await Shell.Current.GoToAsync("ChatRoom");
                    string rName = await DisplayPromptAsync("Create New Room", "Enter room name");
                    await firebase.saveRoom(new Room()
                    {
                        roomName = rName
                    });
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
            
            
            
        }
    }
}