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
    class GameViewModel
    {
        public int Team1ID { get; set; }

        public int Team1Score { get; set; }

        public int Team2ID { get; set; }

        public int Team2Score { get; set; }
    }
}