using System;
using System.Collections.Generic;
using System.Text;
using EasyNote.Views;
using EasyNote.Models;
using System.Windows.Input;
using Xamarin.Forms;
using GalaSoft.MvvmLight.Command;
using Firebase.Auth;

namespace EasyNote.Controller
{
   public class RegistroLoginController : Base
    {
        #region Attributes
        public string email;
        public string password;
        public string name;
        public string age;

        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties
        public string txtemail
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string txtpass
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string txtnombre
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
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

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region Commands
        public ICommand SignUpCommand
        {
            get
            {
                return new RelayCommand(RegisterMethod);
            }
        }
        #endregion

        #region Methods
        private async void RegisterMethod()
        {


            string WebAPIkey = "AIzaSyA-EnkIeXwjuE5qSxpJjcPHXOF9bDDGmHc";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(txtemail.ToString(), txtpass.ToString());
                string gettoken = auth.FirebaseToken;

                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                var user = await FirebaseHelperUsers.AddUser(txtemail, txtpass, txtnombre);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }
        #endregion

        #region Constructor
        public RegistroLoginController()
        {
            IsEnabledTxt = true;
        }
        #endregion
    }
}
