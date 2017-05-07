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
    [Activity(Label = "Matches", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class MatchViewerActivity : Activity
    {
        GameGenerator gen;

        ImageButton t1p1;
        ImageButton t1p2;
        ImageButton t2p1;
        ImageButton t2p2;

        TextView nextBtn;
        
        ImageButton backBtn;
        ImageButton homeBtn;

        int matchNumber = 0;//current match number (non 0 based)

        List<Game> matches;//all matches ever chosen
        Game currentMatch = null;

        Database db = new Database();
        bool isGettingNextMatch = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.MatchViewer);//set layout

            db.Load();

            //back button
            backBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_back);
            RunOnUiThread(() => backBtn.Visibility = ViewStates.Invisible);//make invisible
            backBtn.Click += (s, e) => GoToPreviousMatch();


            ////submit button
            nextBtn = FindViewById<TextView>(Resource.Id.matchviewer_next);
            nextBtn.Click += (s, e) => ThreadPool.QueueUserWorkItem(o => GoToNextMatch());//get one item onclick

            //home button
            homeBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_home);
            homeBtn.Click += (s, e) => GoHome();

            //commit button
            homeBtn = FindViewById<ImageButton>(Resource.Id.matchviewer_save);
            homeBtn.Click += (s, e) => CommitResults();

            //score scrollers
            NumberScrollView team1ScoreLabel = FindViewById<NumberScrollView>(Resource.Id.topScore);
            NumberScrollView team2ScoreLabel = FindViewById<NumberScrollView>(Resource.Id.lowerScore);
            team1ScoreLabel.ValueChanged += () =>
            {
                if (!isGettingNextMatch)
                { currentMatch.Team1Score = team1ScoreLabel.Value; }
            };

            team2ScoreLabel.ValueChanged += () =>
            {
                if (!isGettingNextMatch)
                { currentMatch.Team2Score = team2ScoreLabel.Value; }
            };


            //get player info from json data
            string json = Intent.GetStringExtra("Players");
            Player[] players = JsonConvert.DeserializeObject<Player[]>(json);

            //get all teams which are valid for selected players
            List<Team> teams = new List<Team>();
            foreach (Team t in db.GetTeams())
            {
                if(t.Players.Except(players).Count()==0)
                {
                    teams.Add(t);
                }
            }

            gen = new GameGenerator(players.ToList(),teams);
            matches = new List<Game>();

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
            if(!isGettingNextMatch)
            {
                matchNumber--;
                Game g = matches[matchNumber - 1];
                currentMatch = null;
                currentMatch = g;

                UpdateImages(currentMatch);
                RunOnUiThread(() =>
                {
                    FindViewById<NumberScrollView>(Resource.Id.topScore).Value = currentMatch.Team1Score;
                    FindViewById<NumberScrollView>(Resource.Id.lowerScore).Value = currentMatch.Team2Score;
                });
                
                RunOnUiThread(() => FindViewById<TextView>(Resource.Id.game_number).Text = ("game " + matchNumber.ToString()));

                if (matchNumber == 1)
                {
                    RunOnUiThread(() => backBtn.Visibility = ViewStates.Invisible);
                }
            }
        }


        /// <summary>
        /// Gets next match and updates images
        /// </summary>
        private void GoToNextMatch()
        {

            if (!isGettingNextMatch)
            {
                isGettingNextMatch = true;
                matchNumber++;

                Game g;

                if (matches.Count < matchNumber)
                {
                    System.Diagnostics.Debug.WriteLine("GET NEW MATCH");
                    g = gen.GetGames(1).First();
                    matches.Add(g);

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("GET OLD MATCH");
                    g = matches[matchNumber - 1];
                }

                currentMatch = null;
                currentMatch = g;

                UpdateImages(currentMatch);

                RunOnUiThread(() =>
                {
                    FindViewById<NumberScrollView>(Resource.Id.topScore).Value = currentMatch.Team1Score;
                    FindViewById<NumberScrollView>(Resource.Id.lowerScore).Value = currentMatch.Team2Score;
                });

                if (matchNumber > 1)
                {
                    RunOnUiThread(() => backBtn.Visibility = ViewStates.Visible);
                }

                System.Diagnostics.Debug.WriteLine(currentMatch);

                RunOnUiThread(() => FindViewById<TextView>(Resource.Id.game_number).Text = ("game " + matchNumber.ToString()));

                isGettingNextMatch = false;
            }
        }



        private void UpdateImages(Game m)
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
            StartActivity(typeof(Home));
        }

        private void CommitResults()
        {
            //remove any zero score matches
            List <Game>  matchesforsave;
            matchesforsave = matches;


            List<GameViewModel> gvms = matchesforsave.Select(m => new GameViewModel()
            {
                Team1ID = m.Team1ID,
                Team1Score = m.Team1Score,
                Team2ID = m.Team2ID,
                Team2Score = m.Team2Score
            }).ToList();

            matchesforsave = matches.Where(m => m.Team1Score == 0 && m.Team2Score == 0).ToList();

            Intent i = new Intent(this, typeof(CommitResults));//launch match viewer screen
            string json = JsonConvert.SerializeObject(gvms);//convert all selected matches to JSON text
            i.PutExtra("Matches", json);//send JSON text to activity
            StartActivity(i);

        }
        
     

    }
}