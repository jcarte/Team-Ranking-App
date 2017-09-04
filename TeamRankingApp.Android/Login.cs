using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TeamRankingApp.Domain;
using Android.Content.PM;


namespace TeamRankingApp.Android
{
    [Activity(Label = "Who's Next?", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Login:Activity
    {
        EditText username;
        EditText password;
        protected override void OnCreate(Bundle bundle) 
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            username = FindViewById<EditText>(Resource.Id.login_Username);
            password = FindViewById<EditText>(Resource.Id.login_Password);

            FindViewById<Button>(Resource.Id.login_LoginButton).Click += Login_Click;

            //StartActivity(typeof(Home));
        }

        private void Login_Click(object sender, EventArgs e)
        {
            //FirebaseDatabase fbDb = new FirebaseDatabase(username.Text, password.Text);

            FirebaseDatabase db = new FirebaseDatabase();
            db.Login(username.Text, password.Text);

            //Firebase_Player j2 = new Firebase_Player()
            //{
            //    PlayerID = 1,
            //    Name = "James",
            //    ImagePath = DateTime.Now.ToLongTimeString()
            //};

            //db.UpdateAsync("People/1", j2);
            
            db.GetSingleAsync<Firebase_Player>("People/0");

            Toast.MakeText(this, "Done", ToastLength.Short);
        }
    }
}