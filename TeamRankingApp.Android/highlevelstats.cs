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
    [Activity(Label = "Matches", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class Highlevelstats : Activity
    {
        ImageButton homeBtn;
        Button playerBtn;
        Button teamBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Highlevelstats);//set layout

            //home button
            homeBtn = FindViewById<ImageButton>(Resource.Id.highlevelstats_home);
            homeBtn.Click += (s, e) => GoHome();

            //toggle views
            playerBtn = FindViewById<Button>(Resource.Id.player_stats_btn);
            playerBtn.Click += (s, e) => StatsFragmentController("player");

            teamBtn = FindViewById<Button>(Resource.Id.team_stats_btn);
            teamBtn.Click += (s, e) => StatsFragmentController("team");

        }

        private void GoHome()
        {
            StartActivity(typeof(Home));
        }

        private void StatsFragmentController(String view)
            {
           
            //TODO class for buttons - when toggle need to change the fragment and also the button highlight colours

            //SetContentView(Resource.Layout.Playerstats);//set layout
            //SetContentView(Resource.Layout.Teamstats);//set layout

        }



    }
}