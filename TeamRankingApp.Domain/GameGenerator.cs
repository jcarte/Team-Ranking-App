using System;
using System.Collections.Generic;
using System.Linq;
using TeamRankingApp.Domain.Data;
using TeamRankingApp.Domain.Models;

namespace TeamRankingApp.Domain
{
    public class GameGenerator
    {

        public List<Player> Players { get; }
        public List<Team> Teams { get; }
        private List<GameModel> AllGames; //all possible games

        private GameCollection chosenGames;

        public GameGenerator(List<Player> players,List<Team> teams)
        {
            Players = players;
            Teams = teams;
            chosenGames = new GameCollection();
            AllGames = GetAllGames(teams, players);//TODO move to init?
        }

        public List<GameModel> GetGames(int n)
        {
            List<GameModel> newGames = new List<GameModel>();
            for (int i = 0; i < n; i++)
            {
                if (chosenGames.FinalGameList.Count == 0)
                {
                    //set first match randomly
                    int len = AllGames.Count;
                    Random r = new Random();
                    int j = r.Next(0, len - 1);
                    GameModel m = AllGames[j].Clone();
                    chosenGames.AddGame(m);
                    newGames.Add(m);
                }
                else
                {
                    List<Tuple<int, GameModel>> matchRankScore = new List<Tuple<int, GameModel>>();

                    foreach (GameModel m in AllGames)
                    {
                        //score for match
                        int mScore = chosenGames.MatchConsecutiveNotPlayed(m);

                        //score for teams
                        int t1 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[0])*2, 1);
                        int t2 = (int)Math.Pow(chosenGames.TeamConsecutiveOffCourt(m.Teams[1])*2, 1);
                        int tScore = t1 + t2;

                        //scores for individuals
                        int p1 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[0]) * 3, 2);
                        int p2 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[0].Players[1]) * 3, 2);
                        int p3 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[0]) * 3, 2);
                        int p4 = (int)Math.Pow(chosenGames.IndividualConsecutiveOffCourt(m.Teams[1].Players[1]) * 3, 2);
                        int iScore = p1 + p2 + p3 + p4;

                        int g1 = (int)Math.Pow(chosenGames.IndividualTotalGamesPlayed(m.Teams[0].Players[0]) * 2, 2);
                        int g2 = (int)Math.Pow(chosenGames.IndividualTotalGamesPlayed(m.Teams[0].Players[1]) * 2, 2);
                        int g3 = (int)Math.Pow(chosenGames.IndividualTotalGamesPlayed(m.Teams[1].Players[0]) * 2, 2);
                        int g4 = (int)Math.Pow(chosenGames.IndividualTotalGamesPlayed(m.Teams[1].Players[1]) * 2, 2);
                        int gScore = g1 + g2 + g3 + g4;
                        

                        //add all scores together to get final score
                        int rankingScore = mScore + tScore + iScore - gScore;

                        Tuple<int, GameModel> tup = new Tuple<int, GameModel>(rankingScore, m);
                        matchRankScore.Add(tup);
                    }

                    matchRankScore = matchRankScore.OrderByDescending(m => m.Item1).ToList();
                    GameModel cm = matchRankScore.First().Item2.Clone();
                    chosenGames.AddGame(cm);
                    newGames.Add(cm);

                    //Debug.WriteLine(cm);

                }

            }

            return newGames;
        }


        private List<GameModel> GetAllGames(List<Team> allTeams, List<Player> allPlayers)
        {

            List<GameModel> ml = new List<GameModel>();

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
                        GameModel m = GameModel.Create(A,0, B, 0,allPlayers);
                        ml.Add(m);
                    }
                }
            }

            return ml;

        }

    }
}

