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
using Java.Lang;
using Newtonsoft.Json;

namespace TeamRankingApp.Android
{
    [Activity(Label = "PlayerInput")]
    public class PlayerInputActivity : Activity
    {

        GridView gridview;
        Button submit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.PlayerInput);
            gridview = FindViewById<GridView>(Resource.Id.playerinput_gridview);
            gridview.Adapter = new PersonAdapter(this,MatchGenerator.GetAllPlayers());
            //gridview.SetNumColumns(2);

            submit = FindViewById<Button>(Resource.Id.playerinput_submit);
            submit.Click += Submit_Click;

            //gridview.ItemClick += delegate (object sender, ItemEventArgs args) {
            //    Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            //};
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            List<Player> players = ((PersonAdapter)gridview.Adapter).GetSelectedPlayers();

            //IList<Integer> playInts = players.Select(p => (Integer)p.PlayerID).ToList();

            Intent i = new Intent(this, typeof(MatchViewerActivity));
            //i.PutIntegerArrayListExtra("Players", playInts);

            string json = JsonConvert.SerializeObject(players.ToArray());
            i.PutExtra("Players", json);
            StartActivity(i);
        }
    }
}