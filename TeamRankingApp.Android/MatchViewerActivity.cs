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

        ImageButton t1p1;
        ImageButton t1p2;
        ImageButton t2p1;
        ImageButton t2p2;

        Button submit;

        List<Match> matches;//all matches ever chosen

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MatchViewer);//set layout

            string json = Intent.GetStringExtra("Players");//get player info from json
            Player[] players = JsonConvert.DeserializeObject<Player[]>(json);

            gen = new MatchGenerator(players.ToList());
            matches = new List<Match>();

            //get images from layout
            t1p1 = FindViewById<ImageButton>(Resource.Id.matchviewer_T1P1);
            t1p2 = FindViewById<ImageButton>(Resource.Id.matchviewer_T1P2);
            t2p1 = FindViewById<ImageButton>(Resource.Id.matchviewer_T2P1);
            t2p2 = FindViewById<ImageButton>(Resource.Id.matchviewer_T2P2);

            //submit button
            submit = FindViewById<Button>(Resource.Id.matchviewer_next);
            submit.Click += (s, e) => ThreadPool.QueueUserWorkItem(o => ShowNextMatch());//get one item onclick

            //start first one manually
            ThreadPool.QueueUserWorkItem(o => ShowNextMatch());
        }

        
        /// <summary>
        /// Gets next match and updates images
        /// </summary>
        private void ShowNextMatch()
        {
            Match m = gen.GetMatches(1).First();

            matches.Add(m);

            System.Diagnostics.Debug.WriteLine(m);

            int img_t1p1 = new PlayerView(m.Teams[0].Players[0]).Image;
            int img_t1p2 = new PlayerView(m.Teams[0].Players[1]).Image;
            int img_t2p1 = new PlayerView(m.Teams[1].Players[0]).Image;
            int img_t2p2 = new PlayerView(m.Teams[1].Players[1]).Image;

            RunOnUiThread(() =>
            {
                t1p1.SetImageResource(img_t1p1);
                t1p2.SetImageResource(img_t1p2);
                t2p1.SetImageResource(img_t2p1);
                t2p2.SetImageResource(img_t2p2);
            });
        }
    }
}