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
using Java.Lang;
using TeamRankingApp.Domain;

namespace TeamRankingApp.Android
{
    public class PersonAdapter : BaseAdapter
    {
        private readonly Context context;
        private int[] thumbIds;

        private List<PersonView> views;
        private List<Player> players;

        public PersonAdapter(Context c, List<Player> players)
        {
            this.players = players;
            views = new List<PersonView>(); 
            context = c;
            thumbIds = new int[]
            {
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon,
                Resource.Drawable.Icon
                //Resource.Drawable.Guy,
                //Resource.Drawable.James,
                //Resource.Drawable.Luca,
                //Resource.Drawable.Martin,
                //Resource.Drawable.Megan,
                //Resource.Drawable.Neil,
                //Resource.Drawable.Nicola,
                //Resource.Drawable.Rick
            };
        }

        public override int Count
        {
            get { return thumbIds.Length; }
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return thumbIds[position];
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            PersonView personView;

            if (convertView == null)
            {
                // if it's not recycled, initialize some attributes
                personView = new PersonView(context, players[position]);
                //personView.Player = players[position];
                //personView.LayoutParameters = new AbsListView.LayoutParams(85, 85);
                //personView.SetScaleType(ImageView.ScaleType.CenterCrop);
                //personView.SetPadding(8, 8, 8, 8);
            }
            else
            {
                personView = (PersonView)convertView;
            }
            personView.SetImageResource(thumbIds[position]);
            return personView;
        }

        public List<Player> GetSelectedPlayers()
        {
            return views.Where(v => v.IsSelected).Select(pv => pv.Player).ToList();
        }
    }
}