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
using TeamRankingApp.Domain.Models;
using TeamRankingApp.Domain.Data;

namespace TeamRankingApp.Android
{

    [Activity(Label = "Results", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class CommitResults : Activity
    {

        ImageButton btnBack;
        ImageButton btnCommit;
        List<GameModel> matches;

        Database db;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ConfirmSave);//set layout

            db = new Database();
            db.Load();

            ListView listView = FindViewById<ListView>(Resource.Id.confirmsave_matchview); // get reference to the ListView in the layout
                                                                                           // populate the listview with data

            //get match info from json data
            string json = Intent.GetStringExtra("Matches");
            Tuple<int, int, int, int>[] gvms = JsonConvert.DeserializeObject<Tuple<int, int, int, int>[]>(json);

            List<Team> teams = db.GetTeams();
            matches = gvms.Select(g =>
                GameModel.Create(teams.First(t => t.TeamID == g.Item1), g.Item2, teams.First(t => t.TeamID == g.Item3), g.Item4)
            ).ToList();

            listView.Adapter = new MatchConfirmAdapter(this, matches);

            btnBack = FindViewById<ImageButton>(Resource.Id.cancel_write);
            btnBack.Click += (s, e) => GoBack();
            btnCommit = FindViewById<ImageButton>(Resource.Id.confirm_write);
            btnCommit.Click += (s, e) => Commit();
        }

        private void GoBack()
        {
            base.OnBackPressed();
        }

        private void Commit()
        {
            db.SaveGames(matches);
            Toast.MakeText(this, "Results Saved", ToastLength.Short).Show();
            Intent i = new Intent(this, typeof(Home));
            StartActivity(i);
        }

    }
}