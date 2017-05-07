using System.Collections.Generic;
using System.Linq;

namespace TeamRankingApp.Domain
{
    public class GameCollection
    {

        public List<Game> FinalGameList;

        public GameCollection()
        {
            FinalGameList = new List<Game>();
        }

        public int MatchConsecutiveNotPlayed(Game m)
        {

            Game tm = FinalGameList.LastOrDefault(ma => ma == m);

            if (tm == null)
                return FinalGameList.Count;

            return FinalGameList.Count - FinalGameList.LastIndexOf(tm) - 1;
        }

        //Methid 2 - the number of games since an individual team was on court

        public int TeamConsecutiveOffCourt(Team t)
        {
            Game tm = FinalGameList.LastOrDefault(ma => ma.Teams.Contains(t));

            if (tm == null)
                return FinalGameList.Count;

            return FinalGameList.Count - FinalGameList.LastIndexOf(tm) - 1;
        }

        //Method 3 - the number of games since an individual player was on court

        public int IndividualConsecutiveOffCourt(Player p)
        {
            //Match tm = FinalMatchList.LastOrDefault(ma => ma.OffCourtPlayers.Contains(p));
            Game tm = FinalGameList.LastOrDefault(ma => ma.Teams[0].Players.Contains(p) || ma.Teams[1].Players.Contains(p));

            if (tm == null)
                return FinalGameList.Count;

            return FinalGameList.Count - FinalGameList.LastIndexOf(tm) - 1;
        }

        public void AddGame(Game g)
        {
            FinalGameList.Add(g);
        }
    }
}