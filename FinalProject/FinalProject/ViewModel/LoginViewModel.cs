using FinalProject.CustomControls;
using FinalProject.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public static string googleAPIkey = "AIzaSyDIj3SIO25KIw1177JEKNh0FFZHMs2unWU";
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        public const string FirebaseAppUri = "https://actio-5ec97-default-rtdb.firebaseio.com/";
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {

        }
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Username"));
            }

        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command LoginCommand
        {
            get
            {
                return new Command(Login);
            }
        }
       
        private async void Login()
        {
            

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                try
                {
                    FirebaseHelper.GetUser(Username, Password);
                    
                    
                }
                catch(Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                }
                finally
                {
                    Application.Current.MainPage = new AppShell();
                }
                /*if (user != null)
                    if (Username == user.username && Password == user.password)
                    {

                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Login Fail!", "Please enter correct Email and Password", "OK");
                else
                    await App.Current.MainPage.DisplayAlert("Login Fail!", "User not found", "OK");*/



            }
        }

        public Command ForgotPasswordCommand
        {
            get
            {
                return new Command(ForgotPassword);
            }
        }
        private async void ForgotPassword()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }

       
        /*public Command GoogleLogin
{
   get
   {
       return new Command(GoogleLoginCommand);
   }
}
private async void GoogleLoginCommand()
{
   try
   {
       var cr = new PromptCodeReceiver();

       var result = await GoogleWebAuthorizationBroker.AuthorizeAsync(
           new ClientSecrets { ClientId = googleAPIkey },
           new[] { "email", "profile" },
           "user",
           CancellationToken.None);

       if (result.Token.IsExpired(SystemClock.Default))
       {
           await result.RefreshTokenAsync(CancellationToken.None);
       }

       this.FetchFirebaseData(result.Token.AccessToken, FirebaseAuthType.Google);
   }
   catch (Exception ex)
   {
       await App.Current.MainPage.DisplayAlert("", ex.ToString(), "");
   }
}
private async void FetchFirebaseData(string accessToken, FirebaseAuthType authType)
{
   try
   {
       // Convert the access token to firebase token
       var auth = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
       var data = await auth.SignInWithOAuthAsync(authType, accessToken);

       // Setup FirebaseClient to use the firebase token for data requests
       var db = new FirebaseClient(
              FirebaseAppUri,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(data.FirebaseToken)
              });

       // TODO: your path within your DB structure.
       var dbData = await db
           .Child("userGroups")
           .Child(data.User.LocalId)
           .OnceAsync<object>(); // TODO: custom class to represent your data instead of just object

       // TODO: present your data
       //MessageBox.Show(string.Join("\n", dbData.Select(d => d.ToString())));
   }
   catch (Exception ex)
   {
       //MessageBox.Show(ex.ToString());
   }
}*/
    }
}
