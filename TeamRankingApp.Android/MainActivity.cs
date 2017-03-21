using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using TeamRankingApp.Domain;

namespace TeamRankingApp.Android
{
    [Activity(Label = "TeamRankingApp.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.PlayerInput);

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

