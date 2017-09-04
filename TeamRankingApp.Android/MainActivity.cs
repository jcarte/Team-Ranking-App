﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using TeamRankingApp.Domain;

namespace TeamRankingApp.Android
{
    [Activity(Label = "Who's Next?", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            //StartActivity(typeof(Home));

            //StartActivity(typeof(PlayerInputActivity));

            StartActivity(typeof(Login));

            //List<Player> players = MatchGenerator.GetAllPlayers();
            //Intent i = new Intent(this, typeof(MatchViewerActivity));
            //string json = JsonConvert.SerializeObject(players.ToArray());
            //i.PutExtra("Players", json);
            //StartActivity(i);

        }
    }
}

