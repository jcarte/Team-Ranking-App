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

namespace TeamRankingApp.Android
{
    [Activity(Label = "Home", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class Home : Activity
    {
        
        //TextView statsBtn;
        TextView playBtn;
        ImageButton settingsBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            SetContentView(Resource.Layout.Home);

            //statsbutton
            //statsBtn = FindViewById<TextView>(Resource.Id.home_stats);
            //statsBtn.Click += (s, e) => StartActivity(typeof(?????));

            //play button
            playBtn = FindViewById<TextView>(Resource.Id.home_play);
            playBtn.Click += (s, e) => StartActivity(typeof(PlayerInputActivity));

            //settings
            settingsBtn = FindViewById<ImageButton>(Resource.Id.settings_menu);
            settingsBtn.Click += (s, e) => StartActivity(typeof(SettingsMenu));
        }
    }
}