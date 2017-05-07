using Android.App;
using Android.Content;
using Android.OS;
using Newtonsoft.Json;
using System.Collections.Generic;
using TeamRankingApp.Domain;

namespace TeamRankingApp.Android
{
    [Activity(Label = "Who's Next?", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            StartActivity(typeof(Home));

            //StartActivity(typeof(PlayerInputActivity));

            //List<Player> players = MatchGenerator.GetAllPlayers();
            //Intent i = new Intent(this, typeof(MatchViewerActivity));
            //string json = JsonConvert.SerializeObject(players.ToArray());
            //i.PutExtra("Players", json);
            //StartActivity(i);

        }
    }
}

