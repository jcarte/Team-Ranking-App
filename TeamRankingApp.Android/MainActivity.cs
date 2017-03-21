using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using TeamRankingApp.Domain;
using Android.Content.PM;

namespace TeamRankingApp.Android
{
    [Activity(Label = "FullScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //ActionBar.Hide();
            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.PlayerInput);

            StartActivity(typeof(PlayerInputActivity));


            //List<Player> players = MatchGenerator.GetAllPlayers();

            ////IList<Integer> playInts = players.Select(p => (Integer)p.PlayerID).ToList();

            //Intent i = new Intent(this, typeof(MatchViewerActivity));
            ////i.PutIntegerArrayListExtra("Players", playInts);

            //string json = JsonConvert.SerializeObject(players.ToArray());
            //i.PutExtra("Players", json);
            //StartActivity(i);

        }
    }
}

