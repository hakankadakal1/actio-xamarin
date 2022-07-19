using FinalProject.ViewModel;
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
    public partial class ActivityCreationPage : ContentPage
        
    {
        

        ActivityViewModel activityViewModel;
        public ActivityCreationPage()
        {
            InitializeComponent();
            activityViewModel = new ActivityViewModel();
            BindingContext = activityViewModel;
            SelectCategory.Items.Add("Sports");
            SelectCategory.Items.Add("Gaming");
            SelectCategory.Items.Add("Art");
            
        }

        

        private void ClearAll_Clicked(object sender, EventArgs e)
        {
            var today = DateTime.Now;
            SelectCategory.SelectedIndex = -1;
            ParticipantAmount.SelectedIndex = -1;
            SelectDate.Text = DateTime.Now.ToString();
            SelectTime.Text = DateTime.Now.TimeOfDay.ToString();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayPromptAsync("New Category", "Please type the category you wish to add");
            SelectCategory.Items.Add(result.ToString());
        }
    }
}