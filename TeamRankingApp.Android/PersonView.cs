using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TeamRankingApp.Domain;

namespace TeamRankingApp.Android
{
    public class PersonView : LinearLayout
    {
        Context context;
        //LinearLayout layout;

        public Player Player { get; set; }

        public PersonView(Context context, Player p) : base(context)
        {
            this.Player = p;
            Initialize(context);
        }
        public PersonView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public PersonView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }
        ImageView iv;
        TextView tv;

        private void Initialize(Context context)
        {
            this.context = context;
            _isSelected = false;

            this.Orientation = Orientation.Vertical;

            iv = new ImageView(context);
            iv.SetImageResource(Resource.Drawable.Icon);
            AddView(iv);


            tv = new TextView(context);

            if(Player == null)
                tv.Text = "TEST1";
            else
                tv.Text = Player.Name;

            tv.TextAlignment = TextAlignment.Center;
            tv.Gravity = GravityFlags.CenterHorizontal;
            AddView(tv);
        }

        public void SetImageResource(int i)
        {
            iv.SetImageResource(i);
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            _isSelected = !_isSelected;
            tv.Text = tv.Text + "!";

            if(_isSelected)
                iv.ImageAlpha = 75;
            else
                iv.ImageAlpha = 255;

            return base.OnTouchEvent(e);
        }

        private bool _isSelected;
        public bool IsSelected { get { return _isSelected; } }
    }
}