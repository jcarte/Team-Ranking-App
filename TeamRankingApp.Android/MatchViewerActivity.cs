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

        ImageButton nextBtn;
        ImageButton backBtn;
        ImageButton homeBtn;

        int matchNumber = 0;//current match number (non 0 based)

        List<Match> matches;//all matches ever chosen

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MatchViewer);//set layout

            //back button
            backBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_back);
            RunOnUiThread(() => backBtn.Visibility = ViewStates.Invisible);//make invisible
            backBtn.Click += (s, e) => GoToPreviousMatch();

            //submit button
            nextBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_next);
            nextBtn.Click += (s, e) => ThreadPool.QueueUserWorkItem(o => GoToNextMatch());//get one item onclick

            //home button
            homeBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_home);
            homeBtn.Click += (s, e) => GoHome();

            //get player info from json data
            string json = Intent.GetStringExtra("Players");
            Player[] players = JsonConvert.DeserializeObject<Player[]>(json);

            gen = new MatchGenerator(players.ToList());
            matches = new List<Match>();

            //get images from layout
            t1p1 = FindViewById<ImageButton>(Resource.Id.matchviewer_T1P1);
            t1p2 = FindViewById<ImageButton>(Resource.Id.matchviewer_T1P2);
            t2p1 = FindViewById<ImageButton>(Resource.Id.matchviewer_T2P1);
            t2p2 = FindViewById<ImageButton>(Resource.Id.matchviewer_T2P2);

            //start first one manually
            ThreadPool.QueueUserWorkItem(o => GoToNextMatch());
        }

        /// <summary>
        /// goes back to the previous match if it can
        /// </summary>
        private void GoToPreviousMatch()
        {
            if (matchNumber > 1)
            {
                matchNumber--;
                UpdateImages(matches[matchNumber - 1]);
            }

            if(matchNumber == 1)
                RunOnUiThread(()=>backBtn.Visibility = ViewStates.Invisible);
        }


        /// <summary>
        /// Gets next match and updates images
        /// </summary>
        private void GoToNextMatch()
        {
            matchNumber++;
            Match m;

            if (matches.Count < matchNumber)
            {
                //System.Diagnostics.Debug.WriteLine("GET NEW MATCH");
                m = gen.GetMatches(1).First();
                matches.Add(m);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("GET OLD MATCH");
                m = matches[matchNumber - 1];
            }

            if (matchNumber > 1)
                RunOnUiThread(() => backBtn.Visibility = ViewStates.Visible);

            System.Diagnostics.Debug.WriteLine(m);

            UpdateImages(m);
        }



        private void UpdateImages(Match m)
        {
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

        /// <summary>
        /// Go back to the input screen
        /// </summary>
        private void GoHome()
        {
            StartActivity(typeof(PlayerInputActivity));
        }


    }
}