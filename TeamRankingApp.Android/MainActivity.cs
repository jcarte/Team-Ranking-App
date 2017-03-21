using Android.App;
using Android.OS;

namespace TeamRankingApp.Android
{
    [Activity(Label = "TeamRankingApp.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

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

