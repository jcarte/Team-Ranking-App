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

namespace TeamRankingApp.Android
{

    [Activity(Label = "Results", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class CommitResults : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Confirm);//set layout

        }
    }
}