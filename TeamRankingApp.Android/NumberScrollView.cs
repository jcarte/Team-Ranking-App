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
using Android.Graphics;

namespace TeamRankingApp.Android
{
    public class NumberScrollView : LinearLayout
    {

        private int _value;

        public event Action ValueChanged;
        
        public int Value
        {
            get { return _value; }
            set { _value = value; numberTxt.Text = _value.ToString(); }
        }

        private TextView numberTxt;
        private TextView up;
        private TextView down;

        public NumberScrollView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public NumberScrollView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        public NumberScrollView(Context context) :
            base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            Orientation = Orientation.Vertical;
            _value = 0;

            up = new TextView(Context);
            up.Click += (s, e) => Increase();
            up.Text = "▲";
            up.Gravity = GravityFlags.Center;
            up.LayoutParameters = new TableLayout.LayoutParams(LayoutParams.MatchParent, 0, 1);
            up.TextSize = 20;
            up.SetTextColor(new Color(252, 74, 26));
            
            numberTxt = new TextView(Context);
            numberTxt.Text = _value.ToString();
            numberTxt.Gravity = GravityFlags.Center;
            numberTxt.LayoutParameters = new TableLayout.LayoutParams(LayoutParams.MatchParent, 0, 2);
            numberTxt.TextSize = 30;
            numberTxt.SetTextColor(new Color(252, 74, 26));

            down = new TextView(Context);
            down.Click += (s, e) => Decrease();
            down.Text = "▼";
            down.Gravity = GravityFlags.Center;
            down.LayoutParameters = new TableLayout.LayoutParams(LayoutParams.MatchParent, 0, 1);
            down.TextSize = 20;
            down.SetTextColor(new Color(252, 74, 26));

            AddView(up);
            AddView(numberTxt);
            AddView(down);

        }

        private void Increase()
        {
            _value++;
            numberTxt.Text = _value.ToString();
            ValueChanged?.Invoke();
        }

        private void Decrease()
        {
            if (_value > 0)
            {
                _value--;
                numberTxt.Text = _value.ToString();
                ValueChanged?.Invoke();
            }
        }

    }
}