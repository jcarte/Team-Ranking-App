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
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace TeamRankingApp.Android
{
    [Activity(Label = "MatchViewerActivity")]
    public class MatchViewerActivity : Activity
    {
        MatchGenerator gen;

        TextView t1p1;
        TextView t1p2;
        TextView t2p1;
        TextView t2p2;

        Button submit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.MatchViewer);

            string json = Intent.GetStringExtra("Players");
            Player[] players = JsonConvert.DeserializeObject<Player[]>(json);

            gen = new MatchGenerator(players.ToList());
            matches = new List<Match>();

            t1p1 = FindViewById<TextView>(Resource.Id.matchviewer_T1P1);
            t1p2 = FindViewById<TextView>(Resource.Id.matchviewer_T1P2);
            t2p1 = FindViewById<TextView>(Resource.Id.matchviewer_T2P1);
            t2p2 = FindViewById<TextView>(Resource.Id.matchviewer_T2P2);

            ThreadPool.QueueUserWorkItem(o => ShowNextMatch());

            submit = FindViewById<Button>(Resource.Id.matchviewer_next);
            submit.Click += (s, e) => ThreadPool.QueueUserWorkItem(o => ShowNextMatch());
        }

        List<Match> matches;

        private void ShowNextMatch()
        {
            Match m = gen.GetMatches(1).First();

            matches.Add(m);

            //System.Diagnostics.Debug.WriteLine(m);

            RunOnUiThread(()=>ShowMatch(m));
        }

        private void ShowMatch(Match m)
        {
            t1p1.Text = m.Teams[0].Players[0].Name;
            t1p2.Text = m.Teams[0].Players[1].Name;
            t2p1.Text = m.Teams[1].Players[0].Name;
            t2p2.Text = m.Teams[1].Players[1].Name;
        }
    }
}