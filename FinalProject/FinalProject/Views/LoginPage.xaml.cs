using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AsyncTask = System.Threading.Tasks.Task;


namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginViewModel;
        public LoginPage()
        {
            loginViewModel = new LoginViewModel();
            InitializeComponent();
            BindingContext = loginViewModel;
        }

        private void passwordImageClicked(object sender, EventArgs e)
        {
            if(loginPasswordEntry.IsPassword == true)
            {
                loginPasswordEntry.IsPassword = false;
            }else
            {
                loginPasswordEntry.IsPassword = true;
            }
        }
    }
}