using FinalProject.Models;

using Firebase.Auth;

using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace FinalProject.ViewModel
{
    public class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("https://actio-5ec97-default-rtdb.firebaseio.com/");

        public static string webAPIkey = "AIzaSyDBOxgOxBZKB9jKezcVez5ho-nG1aIYmX8";
        static FirebaseAuthProvider authProvider;

        //User Operations
        public static async Task<List<Users>> GetAllUsers()
        {
            try
            {
                var userList = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>()).Select(item =>
                    new Users
                    {
                        username = item.Object.username,
                        password = item.Object.password,

                    }).ToList();
                return userList;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }
        
        public static async void GetUser(string username,string password)
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));      
            
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(username, password);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("myToken", serializedContent);
                
                /*var allUsers = await GetAllUsers();
                await firebase
                    .Child("Users")
                    .OnceAsync<Users>();
                return allUsers.Where(a => a.email == username).FirstOrDefault();*/
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                
            }
        }
        public static async Task<Users> GetUserInfo(string userID)
        {
            try
            {
                var allUsers = await GetAllUsers();
                await firebase
                    .Child("Users")
                    .OnceAsync<Users>();
                return allUsers.Where(a => a.userID == userID).FirstOrDefault();
            }catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
            
        }
        public static async Task<bool> AddUser(string Email, string Password,string username, string Name,string DateOfBirth, string PhoneNumber)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig(webAPIkey));
                var user = await auth.CreateUserWithEmailAndPasswordAsync(Email, Password, username);
                string gettoken = user.FirebaseToken;
                FirebaseAuth firebaseAuth = new FirebaseAuth();
                await firebase.Child("Users").PostAsync(new Users()
                {
                    email = user.User.Email,
                    password = Password,
                    userID = user.User.LocalId,
                    username = user.User.DisplayName,
                    DOB = DateOfBirth,
                    phone_number = PhoneNumber,
                });
                
                return true;
            }
            catch (Exception e )
            {
                Debug.WriteLine($"Error:{e.Message}");
                return false;
            }
        }
        
        public static async Task<bool> UpdateUser(string Username,string Password)
        {
            try
            {
                var toUpdateUser = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>())
                    .Where(a => a.Object.username == Username).FirstOrDefault();
                await firebase
                    .Child("Users")
                    .Child(toUpdateUser.Key)
                    .PutAsync(new Users() { username = Username, password = Password });
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
        public static async Task<bool> UpdateUserRating(string userID, int rating)
        {
            try
            {
                var toUpdateUserRating = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>())
                    .Where(a => a.Object.userID == userID).FirstOrDefault();
                await firebase
                    .Child("Users")
                    .Child(toUpdateUserRating.Key)
                    .PostAsync(new Users() { rating = rating});
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }
                
        public static async Task<bool> DeleteUser(string Username)
        {
            try
            {
                var toDeletePerson = (await firebase
                    .Child("Users")
                    .OnceAsync<Users>())
                    .Where(a => a.Object.username == Username).FirstOrDefault();
                await firebase.Child("Users").Child(toDeletePerson.Key).DeleteAsync();
                return true;
            
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;

            }
        }

        //Chat Operations
        public async Task<List<Room>> getRoomList()
        {
            return (await firebase
                .Child("ChatApp")
                .OnceAsync<Room>())
                .Select((item) =>
                new Room
                {
                    Key = item.Key,
                    roomName = item.Object.roomName
                }

                       ).ToList();
        }

        public async Task saveRoom(Room rm)
        {
            await firebase.Child("ChatApp")
                    .PostAsync(rm);

        }

        public async Task saveMessage(Chat _ch, string _room)
        {
            await firebase.Child("ChatApp/" + _room + "/Message")
                    .PostAsync(_ch);
        }

        public ObservableCollection<Chat> subChat(string _roomKEY)
        {

            return firebase.Child("ChatApp/" + _roomKEY + "/Message")
                           .AsObservable<Chat>()
                           .AsObservableCollection<Chat>();
        }

        //Activity Operations
        public static async Task<bool> AddActivity( string ActivityCategory, string ActivityDate, string ActivityTime, string ActivityCategoryParticipiantCount)
        {
            try
            {

                await firebase.Child("Activities").PostAsync(new Activities()
                {

                    activityCategory = ActivityCategory,
                    activityDate = ActivityDate,
                    activityTime = ActivityTime,
                    activityParticipiantCount = ActivityCategoryParticipiantCount 
                });
                
                return true;
            } catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        public async Task<List<Activities>> GetActivities()
        {
            try
            {
                return (await firebase
               .Child("Activities")
               .OnceAsync<Activities>())
               .Select((item) =>
               new Activities
               {
                   //activityURL = "https://image.shutterstock.com/image-photo/athletic-african-american-basketball-player-600w-341365352.jpg",
                   activityCategory = item.Object.activityCategory,
                   activityDate = item.Object.activityDate,
                   activityTime = item.Object.activityTime,
                   activityParticipiantCount = item.Object.activityParticipiantCount

               }).ToList();
            }catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
           
        } 

    }
}
