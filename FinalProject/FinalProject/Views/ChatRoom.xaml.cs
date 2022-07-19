using FinalProject.Models;
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
    public partial class ChatRoom : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var list = await firebase.getRoomList();
            myListView.BindingContext = list;

        }


        public ChatRoom()
        {
            
            InitializeComponent();
            
        }

        private void myListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (myListView.SelectedItem != null)
            {
                var selectRoom = (Room)myListView.SelectedItem;
                Navigation.PushAsync(new ChatPage());
                MessagingCenter.Send<ChatRoom, Room>(this, "RoomProp", selectRoom);
            }
        }

        private async void myListView_Refreshing(object sender, EventArgs e)
        {
            myListView.BindingContext = await firebase.getRoomList();
            myListView.IsRefreshing = false;
        }

        private async void roomAddBtnClicked(object sender, EventArgs e)
        {
            string rName = await DisplayPromptAsync("Create New Room", "Enter room name");
            await firebase.saveRoom(new Room() 
            {
                roomName = rName
            });
            
        }

        private async void QR_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRCodePage());
        }
    }
}