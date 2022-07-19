using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel signUpVM;
        public SignUpPage()
        {
            InitializeComponent();
            signUpVM = new SignUpViewModel();
            BindingContext = signUpVM;

        }

        async private void Button_Clicked(object sender, EventArgs e)
        {            
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}