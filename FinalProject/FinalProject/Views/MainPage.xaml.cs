using FinalProject.ViewModel;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinalProject
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel mainPageVM;
       
        public MainPage()
        {
            InitializeComponent();
            mainPageVM = new MainPageViewModel();        
            BindingContext = mainPageVM;

        }
    }
}
