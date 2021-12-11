using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using EasyNote.Models;
using EasyNote.Views;
using Firebase.Auth;
using Xamarin.Essentials;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace EasyNote
{
    public class Login : Base
    {

        FirebaseAuth firebaseAuth;

        #region Attribute
        public string email;
        public string password;
        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties

        public INavigation Navigation { get; set; }

        public string txtEmail
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string txtPassword
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }


        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(LoginMethod);
            }
        }

       /* public ICommand RestarCommand
         {
             get
             {
                 return new RelayCommand(ResetPasswordEmail);
             }
         }*/
        #endregion

        #region Methods


        public async void LoginMethod()
        {


            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("!", "No Hay Conexion a Internet", "Ok");
                return;
            }
                     

            if (string.IsNullOrEmpty(this.email))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            string WebAPIkey = "AIzaSyA-EnkIeXwjuE5qSxpJjcPHXOF9bDDGmHc";


            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(txtEmail.ToString(), txtPassword.ToString());
                var content = await auth.GetFreshAuthAsync(); 
                
                var serializedcontnet = JsonConvert.SerializeObject(content);
                
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);

                await Application.Current.SavePropertiesAsync();
                GetFreshAuthAsync1(authProvider);

                //await Application.Current.MainPage.Navigation.PushModalAsync(new AppShell());

              //  Application.Current.Properties["Email"] = email;
                //Application.Current.Properties["Password"] = password;
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");


            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Usuario o Password Invalido", "OK");
            }

            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            this.IsEnabledTxt = false;

            await Task.Delay(20);





            this.IsRunningTxt = false;
            this.IsVisibleTxt = false;
            this.IsEnabledTxt = true;

        }

        public async Task<bool> ResetPasswordEmail(string email)
        {
            string WebAPIkey = "AIzaSyA-EnkIeXwjuE5qSxpJjcPHXOF9bDDGmHc";

            try
            {
                var firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                await firebaseAuth.SendPasswordResetEmailAsync(email);
            }
            catch (Exception)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Estamos","Aqui","Ok");
            }
            return true;
        }

        public async Task<FirebaseAuthProvider> GetFreshAuthAsync1(FirebaseAuthProvider authProvider1)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyA-EnkIeXwjuE5qSxpJjcPHXOF9bDDGmHc"));

            var savedfirebaseauth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("FirebaseRefreshToken", ""));
            var RefreshedContent = await authProvider.RefreshAuthAsync(savedfirebaseauth);
            Preferences.Set("FirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));
            string UsersEmailToDisplay = savedfirebaseauth.User.Email;
            return authProvider1;
        }


        #endregion

        #region Constructor
        public Login()
        {
            this.IsEnabledTxt = true;
        }
        #endregion
    }
}
