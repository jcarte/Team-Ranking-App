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

namespace TeamRankingApp.Domain
{
    public class MatchCollection
    {

        public List<Match> FinalMatchList;

        public MatchCollection()
        {

            FinalMatchList = new List<Match>();

        }

        //Method 1 - the number of games since this combination of teams was on court

        public int MatchConsecutiveNotPlayed(Match m)
        {

            Match tm = FinalMatchList.LastOrDefault(ma => ma == m);

            if (tm == null)
                return FinalMatchList.Count;

            return FinalMatchList.Count - FinalMatchList.LastIndexOf(tm) - 1;
        }

        //Methid 2 - the number of games since an individual team was on court

        public int TeamConsecutiveOffCourt(Team t)
        {
            Match tm = FinalMatchList.LastOrDefault(ma => ma.Teams.Contains(t));

            if (tm == null)
                return FinalMatchList.Count;

            return FinalMatchList.Count - FinalMatchList.LastIndexOf(tm) - 1;
        }

        //Method 3 - the number of games since an individual player was on court

        public int IndividualConsecutiveOffCourt(Player p)
        {
            Match tm = FinalMatchList.LastOrDefault(ma => ma.OffCourtPlayers.Contains(p));

            if (tm == null)
                return FinalMatchList.Count;

            return FinalMatchList.Count - FinalMatchList.LastIndexOf(tm) - 1;
        }

        public void AddMatch(Match m)
        {
            FinalMatchList.Add(m);
        }

    }
}