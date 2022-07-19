using FinalProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Firebase.Auth;

namespace FinalProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        FirebaseHelper fbClient = new FirebaseHelper();
        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));

        Room room = new Room();

        Users user = new Users();

        

        public ChatPage()
        {
            InitializeComponent();
            chatListView.TabIndex = 0;
            MessagingCenter.Subscribe<ChatRoom, Room>(this, "RoomProp", (page, data) =>
            {
                room = data;
                chatListView.BindingContext = fbClient.subChat(data.Key);
                MessagingCenter.Unsubscribe<ChatRoom, Room>(this, "RoomProp");
            });
        }
        private async Task<string> GetCurrentInfo()
        {
            try
            {
                var savedAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("myToken", ""));
                var refreshedContent = await firebaseAuthProvider.RefreshAuthAsync(savedAuth);
                Preferences.Set("myToken", JsonConvert.SerializeObject(refreshedContent));
                
                return savedAuth.User.DisplayName;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }

        private async void sendButtonClicked(object sender, EventArgs e)
        {
            
            var chatObject = new Chat
            {
                userMessage = messageToSent.Text,
                username = await GetCurrentInfo()
            };
            await fbClient.saveMessage(chatObject, room.Key);
            messageToSent.Text = String.Empty;
        }

        private async void startClicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert("", "Your event will start. Are you sure?", "Confirm", "Cancel");

            if (result)
            {
                await Navigation.PushModalAsync(new DuringActivity());
            }
            
        }
    }
}