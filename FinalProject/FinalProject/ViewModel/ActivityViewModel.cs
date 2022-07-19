using FinalProject.Models;
using FinalProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FinalProject.ViewModel
{
    public class ActivityViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> categoryList;

        private string activityCategory;

        public string ActivityCategory
        {
            get { return activityCategory; }
            set
            {
                activityCategory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ActivityCategory"));
            }
        }

        private string activityDate;

        public string ActivityDate
        {
            get { return activityDate; }
            set
            {
                activityDate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ActivityDate"));
            }
        }

        private string activityTime;

        public string ActivityTime
        {
            get { return activityTime; }
            set
            {
                activityTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ActivityTime"));
            }
        }

        private string activityParticipiantCount;

        public string ActivityParticipiantCount
        {
            get { return activityParticipiantCount; }
            set
            {
                activityParticipiantCount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ActivityParticipiantCount"));
            }
        }

        public Command AddActivityCommand
        {
            get
            {
                return new Command(AddActivity);
            }
        }
        private async void AddActivity()
        {
           
            var user = await FirebaseHelper.AddActivity(ActivityCategory, ActivityDate, ActivityTime, ActivityParticipiantCount);
            if (user)
            {
                await App.Current.MainPage.DisplayAlert("Activity created", "", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }else
                await App.Current.MainPage.DisplayAlert("Activity creation failed", "", "OK");
        }


        
    }
    
}
