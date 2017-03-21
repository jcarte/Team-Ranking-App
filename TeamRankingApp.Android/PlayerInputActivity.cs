using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TeamRankingApp.Domain;
using Newtonsoft.Json;

namespace TeamRankingApp.Android
{
    [Activity(Label = "Player Input", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
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

        ImageButton submit;
        List<Player> allPlayers;
        List<Player> selectedPlayers;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PlayerInput);

            allPlayers = MatchGenerator.GetAllPlayers();
            selectedPlayers = new List<Player>();

            //get all image buttons
            btnGO = FindViewById<ImageButton>(Resource.Id.BtnGuy);
            btnJC = FindViewById<ImageButton>(Resource.Id.BtnJames);
            btnLP = FindViewById<ImageButton>(Resource.Id.BtnLuca);
            btnMW = FindViewById<ImageButton>(Resource.Id.BtnMartin);
            btnMP = FindViewById<ImageButton>(Resource.Id.BtnMegan);
            btnN = FindViewById<ImageButton>(Resource.Id.BtnNeil);
            btnNT = FindViewById<ImageButton>(Resource.Id.BtnNicola);
            btnRW = FindViewById<ImageButton>(Resource.Id.BtnRick);

            //when each button is clicked, route it to the button pressed method
            btnGO.Click += (s, e) => ButtonPressed("Guy", (ImageButton)s);
            btnJC.Click += (s, e) => ButtonPressed("James", (ImageButton)s);
            btnLP.Click += (s, e) => ButtonPressed("Luca", (ImageButton)s);
            btnMW.Click += (s, e) => ButtonPressed("Martin", (ImageButton)s);
            btnMP.Click += (s, e) => ButtonPressed("Megan", (ImageButton)s);
            btnN.Click += (s, e) => ButtonPressed("Neil", (ImageButton)s);
            btnNT.Click += (s, e) => ButtonPressed("Nicola", (ImageButton)s);
            btnRW.Click += (s, e) => ButtonPressed("Rick", (ImageButton)s);

            submit = FindViewById<ImageButton>(Resource.Id.playerinput_submit);
            submit.Click += Submit_Click;
        }

        /// <summary>
        /// Logs that button has been pressed and changes fade on image
        /// </summary>
        /// <param name="name">persons name</param>
        /// <param name="image">button pressed</param>
        private void ButtonPressed(string name, ImageButton image)
        {
            //get persons name from all players
            Player p = allPlayers.FirstOrDefault(t => t.Name == name);
            if (selectedPlayers.Contains(p))//is person already "Selected"?
            {
                image.ImageAlpha = 255;//make visible
                selectedPlayers.Remove(p);
            }
            else
            {
                image.ImageAlpha = 75;//make semi transparent
                selectedPlayers.Add(p);
            }
        }


        
        private void Submit_Click(object sender, EventArgs e)
        {
            if (selectedPlayers.Count <4)
            {
                Toast.MakeText(this, "Please select 4 or more players", ToastLength.Long).Show();

            }
            else
            {
                Intent i = new Intent(this, typeof(MatchViewerActivity));//launch match viewer screen
                string json = JsonConvert.SerializeObject(selectedPlayers.ToArray());//convert all selected players to JSON text
                i.PutExtra("Players", json);//send JSON text to activity
                StartActivity(i);//run
            }
            
        }
    }
}