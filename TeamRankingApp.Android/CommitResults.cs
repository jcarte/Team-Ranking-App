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

namespace TeamRankingApp.Android
{

    [Activity(Label = "Results", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class CommitResults : Activity
    {

        ImageButton btnBack;
        ImageButton btnCommit;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ConfirmSave);//set layout

            ListView listView = FindViewById<ListView>(Resource.Id.confirmsave_matchview); // get reference to the ListView in the layout
                                                                                           // populate the listview with data

            //get match info from json data
            string json = Intent.GetStringExtra("Matches");
            List<Match> matches = JsonConvert.DeserializeObject<List<Match>>(json);

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
            //TODO - call backend save method
        }

    }
}