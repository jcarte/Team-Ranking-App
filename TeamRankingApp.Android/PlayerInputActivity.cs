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

        //GridView gridview;
        ImageButton btnGO;
        ImageButton btnJC;
        ImageButton btnLP;
        ImageButton btnMW;
        ImageButton btnMP;
        ImageButton btnN;
        ImageButton btnNT;
        ImageButton btnRW;

        Button submit;
        List<Player> allPlayers;
        List<Player> selectedPlayers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PlayerInput);

            allPlayers = MatchGenerator.GetAllPlayers();
            selectedPlayers = new List<Player>();

            btnGO = FindViewById<ImageButton>(Resource.Id.BtnGuy);
            btnJC = FindViewById<ImageButton>(Resource.Id.BtnJames);
            btnLP = FindViewById<ImageButton>(Resource.Id.BtnLuca);
            btnMW = FindViewById<ImageButton>(Resource.Id.BtnMartin);
            btnMP = FindViewById<ImageButton>(Resource.Id.BtnMegan);
            btnN = FindViewById<ImageButton>(Resource.Id.BtnNeil);
            btnNT = FindViewById<ImageButton>(Resource.Id.BtnNicola);
            btnRW = FindViewById<ImageButton>(Resource.Id.BtnRick);

            btnGO.Click += (s, e) => ButtonPressed("Guy");
            btnJC.Click += (s, e) => ButtonPressed("James");
            btnLP.Click += (s, e) => ButtonPressed("Luca");
            btnMW.Click += (s, e) => ButtonPressed("Martin");
            btnMP.Click += (s, e) => ButtonPressed("Megan");
            btnN.Click += (s, e) => ButtonPressed("Neil");
            btnNT.Click += (s, e) => ButtonPressed("Nicola");
            btnRW.Click += (s, e) => ButtonPressed("Rick");

            //gridview = FindViewById<GridView>(Resource.Id.playerinput_gridview);
            //gridview.Adapter = new PersonAdapter(this,MatchGenerator.GetAllPlayers());
            //gridview.SetNumColumns(2);

            submit = FindViewById<Button>(Resource.Id.playerinput_submit);
            submit.Click += Submit_Click;

            //gridview.ItemClick += delegate (object sender, ItemEventArgs args) {
            //    Toast.MakeText(this, args.Position.ToString(), ToastLength.Short).Show();
            //};
        }

        private void ButtonPressed(string name)
        {
            //get guy from all players
            Player p = allPlayers.FirstOrDefault(t => t.Name == name);
            if (selectedPlayers.Contains(p))
            {
                selectedPlayers.Remove(p);
            }
            else
            {
                selectedPlayers.Add(p);
            }

        }

        private void Submit_Click(object sender, EventArgs e)
        {
            //List<Player> players = ((PersonAdapter)gridview.Adapter).GetSelectedPlayers();

            //IList<Integer> playInts = players.Select(p => (Integer)p.PlayerID).ToList();

            Intent i = new Intent(this, typeof(MatchViewerActivity));
            //i.PutIntegerArrayListExtra("Players", playInts);

            string json = JsonConvert.SerializeObject(selectedPlayers.ToArray());
            i.PutExtra("Players", json);
            StartActivity(i);
        }
    }
}