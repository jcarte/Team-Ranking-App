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
using Android.Content.PM;

namespace TeamRankingApp.Android
{
    [Activity(Label = "SettingsMenu", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class SettingsMenu : Activity
    {

        ImageButton btnCancel;
        ImageButton btnPush;
        ImageButton btnPull;
        EditText DBKey;
        TextView DBKeySave;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);//set layout

            //Set buttons to layout
            btnCancel = FindViewById<ImageButton>(Resource.Id.setting_cancel);
            btnCancel.Click += (s, e) => GoBack();

            btnPush = FindViewById<ImageButton>(Resource.Id.push_tocloud);
            btnPush.Click += (s, e) => PushToCloud();

            btnPull = FindViewById<ImageButton>(Resource.Id.pull_fromcloud);
            btnPull.Click += (s, e) => PullFromCloud();

            DBKey = FindViewById<EditText>(Resource.Id.input_DBKey);
            DBKeySave = FindViewById<TextView>(Resource.Id.save_DBKey);
            DBKeySave.Click += (s, e) => SaveDBKey();
        }

        private void SaveDBKey()
        {
            throw new NotImplementedException();
        }

        private void PullFromCloud()
        {
            throw new NotImplementedException();
        }

        private void PushToCloud()
        {
            throw new NotImplementedException();
        }

        private void GoBack()
        {
            StartActivity(typeof(Home));
        }
    }
}