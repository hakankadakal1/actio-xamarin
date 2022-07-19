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
    public partial class ForgotPasswordPage : ContentPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void SignupClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new SignUpPage());

        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }
    }
}