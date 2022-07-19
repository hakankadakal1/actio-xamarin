using FinalProject.Views;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FinalProject.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {

        public string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
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
        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }

        private string password;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string dob;

        public string DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DOB"));
            }
        }

        private string phone_number;

        public string PhoneNumber
        {
            get { return phone_number; }
            set
            {
                phone_number = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {
                confirmpassword = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword"));
            }
        }

        public Command SignUpCommand
        {
            get
            {
                return new Command(() =>
                {
                    SignUp();
                });
            }
        }

        private async void SignUp()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                //call AddUser function which we define in Firebase helper class    
                var user = await FirebaseHelper.AddUser(Email, Password, Username, Name, DOB, PhoneNumber);
                //AddUser return true if data insert successfuly     
                if (user)
                {
                    App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                    await Application.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");

            }
        }



    }
}
