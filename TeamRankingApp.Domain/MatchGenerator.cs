using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TeamRankingApp.Domain
{

    //I provide list of all players from -1- and am given list -2- of useable players
    //I manipulate to return a list of matches in order -3-

    public class MatchGenerator
    {
        /// <summary>
        /// Get all possible players
        /// </summary>
        /// <returns>List of all possible players</returns>
        /// 

        // -1-
        public static List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            
            players.Add(new Player() { PlayerID = 1, Name = "Guy" });
            players.Add(new Player() { PlayerID = 2, Name = "James" });
            players.Add(new Player() { PlayerID = 3, Name = "Luca" });
            players.Add(new Player() { PlayerID = 4, Name = "Martin" });
            players.Add(new Player() { PlayerID = 5, Name = "Megan" });
            players.Add(new Player() { PlayerID = 6, Name = "Neil" });
            players.Add(new Player() { PlayerID = 7, Name = "Nicola" });
            players.Add(new Player() { PlayerID = 8, Name = "Rick" });

            return players;
        }



        //Players in this session ready to be matched - this has been passed from the front end
        private List<Player> activePlayers;
        private MatchCollection chosenMatches;

        List<Team> allTeams;
        List<Match> allMatches;

        // -2-

        /// <summary>
        /// Match Generator Constructor
        /// </summary>
        /// <param name="players">All players playing in this session and to be considered for matches</param>

        public MatchGenerator(List<Player> players)
        {
            this.activePlayers = players;
            this.chosenMatches = new MatchCollection();
            this.allTeams = GetAllTeams(players);
            this.allMatches = GetAllMatches(allTeams, players);
        }

        


        // -3-

        /// <summary>
        /// Get all match configurations
        /// </summary>
        /// <returns>Get all matches for the currently playing players</returns>
        /// 

        public List<Match> GetMatches(int n)
        {
            List<Match> newMatches = new List<Match>();
            
            for (int i = 0; i < n; i++)
            {
                if (chosenMatches.FinalMatchList.Count==0)
                {
                    //set first match randomly
                    int len = allMatches.Count;
                    Random r = new Random();
                    int j = r.Next(0, len - 1);
                    Match m = allMatches[j];
                    chosenMatches.AddMatch(m);
                    newMatches.Add(m);
                }
                else
                {
                    List<Tuple<int, Match>> matchRankScore = new List<Tuple<int, Match>>();

                    foreach (Match m in allMatches)
                    {
                        //score for match
                        int mScore = chosenMatches.MatchConsecutiveNotPlayed(m);

                        //score for teams
                        int t1 = (int)Math.Pow(chosenMatches.TeamConsecutiveOffCourt(m.Teams[0]), 2);
                        int t2 = (int)Math.Pow(chosenMatches.TeamConsecutiveOffCourt(m.Teams[1]), 2);
                        int tScore = t1 + t2;

                        //scores for individuals
                        int p1 = (int)Math.Pow(chosenMatches.IndividualConsecutiveOffCourt(m.Teams[0].Players[0]), 4);
                        int p2 = (int)Math.Pow(chosenMatches.IndividualConsecutiveOffCourt(m.Teams[0].Players[1]), 4);
                        int p3 = (int)Math.Pow(chosenMatches.IndividualConsecutiveOffCourt(m.Teams[1].Players[0]), 4);
                        int p4 = (int)Math.Pow(chosenMatches.IndividualConsecutiveOffCourt(m.Teams[1].Players[1]), 4);
                        int iScore = p1 + p2 + p3 + p4;

                        //add all scores together to get final score
                        int rankingScore = mScore + tScore + iScore;

                        Tuple<int, Match> tup = new Tuple<int, Match>(rankingScore, m);
                        matchRankScore.Add(tup);
                    }

                    matchRankScore = matchRankScore.OrderByDescending(m => m.Item1).ToList();
                    Match cm = matchRankScore.First().Item2;
                    chosenMatches.AddMatch(cm);
                    newMatches.Add(cm);

                    //Debug.WriteLine(cm);

                }

            }

            return newMatches;

        }

        

        private List<Team> GetAllTeams(List<Player> players)
        {

            List<Team> tl = new List<Team>();

            //fill tl with all the combinations with no duplicates

            //loop through players from i= 0 to len
            for (int i = 0; i < players.Count; i++)
            {
                //loop through players j from cuurent row (i) to len
                for (int j = i+1; j < players.Count; j++)
                {
                    Team t = new Team(players[i], players[j]);
                    tl.Add(t);
                }
            }
                           

            return tl;

        }

        private List<Match> GetAllMatches(List<Team> allTeams, List<Player> allPlayers)
        {

            List<Match> ml = new List<Match>();

            //fill tl with all the combinations with no duplicates

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
                        Match m = new Match(A, B, allPlayers);
                        ml.Add(m);
                    }
                }
            }

            return ml;

        }

    }

}
