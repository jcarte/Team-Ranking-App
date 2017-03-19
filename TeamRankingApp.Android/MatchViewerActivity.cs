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

namespace TeamRankingApp.Android
{
    [Activity(Label = "MatchViewerActivity")]
    public class MatchViewerActivity : Activity
    {
        MatchGenerator gen;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MatchViewer);

            int[] ids = Intent.GetIntArrayExtra("Players");
            List<Player> players = MatchGenerator.GetAllPlayers().Where(p=>ids.Contains(p.PlayerID)).ToList();
            gen = new MatchGenerator(players);
        }
    }
}