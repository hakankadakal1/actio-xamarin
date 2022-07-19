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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingPage : ContentPage
    {
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
        
        public RatingPage()
        {
            
            InitializeComponent();
            UpdateUserRating();
            

        }
        private string GetCurrentInfo()
        {
            try
            {
                var savedAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("token", ""));
                var refreshedContent = firebaseAuthProvider.RefreshAuthAsync(savedAuth);
                Preferences.Set("token", JsonConvert.SerializeObject(refreshedContent));
                return savedAuth.User.LocalId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }
        private async void UpdateUserRating()
        {
            try
            {
                var user = GetCurrentInfo();
                var authUser = await FirebaseHelper.GetUserInfo(user);
                int currentRating = authUser.rating;
                int rating = (int)((currentRating + ratingBar.SelectedStarValue) / 2);
                var isUpdate = await FirebaseHelper.UpdateUserRating(authUser.userID, 5);
                if (isUpdate)
                {
                    await App.Current.MainPage.DisplayAlert("Successfully rated","","");
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());
                }
                
                

            }catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                
            }
            
            
            
        }

        private async void rateBtnClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());

        }
    }
}