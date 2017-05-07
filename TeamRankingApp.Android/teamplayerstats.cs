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
    public class Teamplayerstats : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SetContentView(Resource.Layout.Teamstats);//set layout

            //SetContentView(Resource.Layout.Playerstats);//set layout
        }
    }
}