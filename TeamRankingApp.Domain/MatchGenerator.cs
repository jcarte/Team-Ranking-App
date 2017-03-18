using System;
using System.Collections;
using System.Collections.Generic;
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

        // -2-

        /// <summary>
        /// Match Generator Constructor
        /// </summary>
        /// <param name="players">All players playing in this session and to be considered for matches</param>

        public MatchGenerator(List<Player> players)
        {
            this.activePlayers = players;
        }

        //Players in this session ready to be matched - this has been passed from the front end
        private List<Player> activePlayers;


        // -3-

        /// <summary>
        /// Get all match configurations
        /// </summary>
        /// <returns>Get all matches for the currently playing players</returns>
        /// 

        public List<Match> GetAllMatches()
        {

            //Player p1 = activePlayers[0]; - example of how to get a player

            //Step 1 - populate all combinations of teams

            List<Team> allTeams = GetAllTeams(activePlayers);

            //Step 2 - populate all combinations of matches

            List<Match> allMatches = GetAllMatches(allTeams);


            ///////////START HEREE!!!!!!!







            //Step x - output the matches to be played
            List<Match> finalMatches = new List<Match>();

            //Team t1 = new Team(p1, p2);
            //Team t2 = new Team(p3, p4);

            //Match m = new Match(t1, t2);


            //MatchCollection matches = new MatchCollection();
            //matches.Add(m);


            return finalMatches;

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

        private List<Match> GetAllMatches(List<Team> allTeams)
        {

            List<Match> ml = new List<Match>();

            //fill tl with all the combinations with no duplicates

            //loop through players from i= 0 to len
            for (int i = 0; i < allTeams.Count; i++)
            {
                //loop through players j from cuurent row (i) to len
                for (int j = i + 1; j < allTeams.Count; j++)
                {
                    Match m = new Match(allTeams[i], allTeams[j]);
                    ml.Add(m);
                }
            }

            return ml;

        }

    }

}
