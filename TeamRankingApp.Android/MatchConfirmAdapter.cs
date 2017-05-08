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
using TeamRankingApp.Domain.Models;

namespace TeamRankingApp.Android
{
    public class MatchConfirmAdapter : BaseAdapter<GameModel>
    {
        List<GameModel> matches;
        Activity context;

        public MatchConfirmAdapter(Activity context, List<GameModel> matches):base()
        {
            this.context = context;
            this.matches = matches;
        }

        public override GameModel this[int position]
        {
            get
            {
                return matches[position]; 
            }
        }

        public override int Count
        {
            get
            {
                return matches.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //this show how to render an individual row in the custom list
            GameModel m = matches[position];
            //TODO: update s1 and s2 to be the scores passed from backend once available
            //string s1 = "1";
            //string s2 = "11";
            //string p1 = "Guy";
            //string p2 = "James";
            //string p3 = "Megan";
            //string p4 = "Nicola";

            string s1 = m.Team1Score.ToString();
            string s2 = m.Team2Score.ToString();
            string p1 = m.Team1.Player1.Name;
            string p2 = m.Team1.Player2.Name;
            string p3 = m.Team2.Player1.Name;
            string p4 = m.Team2.Player2.Name;


            //make 4 images depending on players
            int Image1 = GetImageFromName(p1);
            int Image2 = GetImageFromName(p2);
            int Image3 = GetImageFromName(p3);
            int Image4 = GetImageFromName(p4);

            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomMatchResultsRow, null);

            //push images into here
            view.FindViewById<ImageView>(Resource.Id.saveplayer1).SetImageResource(Image1);
            view.FindViewById<ImageView>(Resource.Id.saveplayer2).SetImageResource(Image2);
            view.FindViewById<TextView>(Resource.Id.savescore1).Text = s1;
            view.FindViewById<TextView>(Resource.Id.savescore2).Text = s2;
            view.FindViewById<ImageView>(Resource.Id.saveplayer3).SetImageResource(Image3);
            view.FindViewById<ImageView>(Resource.Id.saveplayer4).SetImageResource(Image4);
            return view;
        }

        private int GetImageFromName(string name)
        {
            switch (name)
            {
                case "Guy": return Resource.Drawable.Guy;
                case "James": return Resource.Drawable.James;
                case "Luca": return Resource.Drawable.Luca;
                case "Martin": return Resource.Drawable.Martin;
                case "Megan": return Resource.Drawable.Megan;
                case "Neil": return Resource.Drawable.Neil;
                case "Nicola": return Resource.Drawable.Nicola;
                case "Rick": return Resource.Drawable.Rick;
                default: throw new ArgumentException("Name unknown");
            }
        }
    }
}