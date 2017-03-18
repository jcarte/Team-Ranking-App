using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamRankingApp.Domain
{
    public class MatchGenerator
    {
        /// <summary>
        /// Get all possible players
        /// </summary>
        /// <returns>List of all possible players</returns>
        public static List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            players.Add(new Player() { PlayerID = 1, Name = "Nicola" });
            players.Add(new Player() { PlayerID = 2, Name = "James" });
            players.Add(new Player() { PlayerID = 3, Name = "Guy" });
            players.Add(new Player() { PlayerID = 4, Name = "Megan" });
            return players;
        }

        /// <summary>
        /// Match Generator Constructor
        /// </summary>
        /// <param name="players">All players playing in this session and to be considered for matches</param>
        public MatchGenerator(List<Player> players)
        {
            this.players = players;
        }

        private List<Player> players;//Players in this session ready to be matched




        

        /// <summary>
        /// Get all match configurations
        /// </summary>
        /// <returns>Get all matches for the currently playing players</returns>
        public List<Match> GetAllMatches()
        {
            throw new NotImplementedException();

            //Player p1 = players[0];
            //Player p2 = players[1];
            //Player p3 = players[2];
            //Player p4 = players[3];

            //List<Team> allTeams = new List<Team>();

            //List<Match> finalMatches = new List<Match>();
            
            //Team t1 = new Team(p1, p2);
            //Team t2 = new Team(p3, p4);

            //Match m = new Match(t1, t2);


            //MatchCollection matches = new MatchCollection();
            //matches.Add(m);

        }
        




    }

}
