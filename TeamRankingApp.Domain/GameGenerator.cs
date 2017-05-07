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
using System.IO;

namespace TeamRankingApp.Domain
{
    public class GameGenerator
    {

        public List<Player> Players { get; }
        public List<Team> Teams { get; }
        private List<Game> Games;

        private GameCollection chosenGames;

        public GameGenerator(List<Player> players,List<Team> teams)
        {
            Players = players;
            Teams = teams;
            chosenGames = new GameCollection();
            Games = GetAllGames(teams, players);//TODO move to init?
        }

        public List<Game> GetGames(int n)
        {
            List<Game> newGames = new List<Game>();
            for (int i = 0; i < n; i++)
            {
                if (chosenGames.FinalGameList.Count == 0)
                {
                    //set first match randomly
                    int len = Games.Count;
                    Random r = new Random();
                    int j = r.Next(0, len - 1);
                    Game m = Games[j];
                    chosenGames.AddGame(m);
                    newGames.Add(m);
                }
                else
                {
                    List<Tuple<int, Game>> matchRankScore = new List<Tuple<int, Game>>();

                    foreach (Game m in Games)
                    {
                        //score for match
                        int mScore = chosenGames.MatchConsecutiveNotPlayed(m);

                        //score for teams
                        //int t1 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[0]), 2);
                        //int t2 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[1]), 2);
                        int t1 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[0])*2, 1);
                        int t2 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[1])*2, 1);
                        int tScore = t1 + t2;

                        //scores for individuals
                        //int p1 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[0]), 4);
                        //int p2 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[1]), 4);
                        //int p3 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[0]), 4);
                        //int p4 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[1]), 4);
                        int p1 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[0]) * 2, 2);
                        int p2 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[1]) * 2, 2);
                        int p3 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[0]) * 2, 2);
                        int p4 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[1]) * 2, 2);
                        int iScore = p1 + p2 + p3 + p4;

                        //add all scores together to get final score
                        int rankingScore = mScore + tScore + iScore;

                        Tuple<int, Game> tup = new Tuple<int, Game>(rankingScore, m);
                        matchRankScore.Add(tup);
                    }

                    matchRankScore = matchRankScore.OrderByDescending(m => m.Item1).ToList();
                    Game cm = matchRankScore.First().Item2;
                    chosenGames.AddGame(cm);
                    newGames.Add(cm);

                    //Debug.WriteLine(cm);

                }

            }

            return newGames;
        }


        private List<Game> GetAllGames(List<Team> allTeams, List<Player> allPlayers)
        {

            List<Game> ml = new List<Game>();

            //loop through players from i= 0 to len
            for (int i = 0; i < allTeams.Count; i++)
            {
                //loop through players j from cuurent row (i) to len
                for (int j = i + 1; j < allTeams.Count; j++)
                {
                    Team A = allTeams[i];
                    Team B = allTeams[j];
                    if (A.Players.Union(B.Players).Distinct().Count() == 4)//only add matches with 4 distinct players
                    {
                        Game m = Game.Create(A,0, B, 0,allPlayers);
                        ml.Add(m);
                    }
                }
            }

            return ml;

        }

    }
}


/*
 * ------New Structure----
 * Expose database class to front end, fe chooses save path etc
 * fe chooses players in this session - from these players filters all teams down to only the teams containing those chosen players
 * Players and teams given to game generator and stored
 * Need to make game collection
 * on GetGames uses players (to work out players off court - need to add this to game) and teams to generate games, adds this to collection only when chosen on front end?
 */