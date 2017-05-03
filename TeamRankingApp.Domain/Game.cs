using SQLite;
using System;

namespace TeamRankingApp.Domain
{
    /// <summary>
    /// The results of a single game where two teams play against each other
    /// </summary>
    public class Game
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionID { get; set; }

        /// <summary>
        /// Timestamp when the record was created / game was created
        /// </summary>
        public DateTime Created { get; set; }

        public int Team1ID { get; set; }

        public int Team1Score { get; set; }

        public int Team2ID { get; set; }

        public int Team2Score { get; set; }

        [Ignore]
        public Team Team1 { get; set; }

        [Ignore]
        public Team Team2 { get; set; }


        /// <summary>
        /// Static creator
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t1Score"></param>
        /// <param name="t2"></param>
        /// <param name="t2Score"></param>
        /// <returns></returns>
        public static Game Create(Team t1, int t1Score, Team t2, int t2Score)
        {
            return new Game()
            {
                Created = DateTime.Now,
                Team1ID = t1.TeamID,
                Team1Score = t1Score,
                Team2ID = t2.TeamID,
                Team2Score = t2Score,
                Team1 = t1,
                Team2 = t2
            };
        }

        public bool ContainsPlayer(Player p)
        {
            return Team1.ContainsPlayer(p) || Team2.ContainsPlayer(p);
        }

        public bool ContainsTeam(Team t)
        {
            return Team1ID == t.TeamID || Team2ID == t.TeamID;
        }

        public int GetPlayerPoints(Player p)
        {
            if (!ContainsPlayer(p))
                throw new ArgumentException("Player did not play in this game");
            else if (Team1.ContainsPlayer(p))
                return Team1Score;
            else
                return Team2Score;
        }


        public int GetTeamPoints(Team t)
        {
            if (!ContainsTeam(t))
                throw new ArgumentException("Team did not play in this game");
            else if (Team1ID == t.TeamID)
                return Team1Score;
            else
                return Team2Score;
        }

        public int GetPointsAgainst(Team t)
        {
            if (!ContainsTeam(t))
                throw new ArgumentException("Team did not play in this game");
            else if (Team1ID == t.TeamID)
                return Team2Score;
            else
                return Team1Score;
        }

        public int GetPointsAgainst(Player p)
        {
            if (!ContainsPlayer(p))
                throw new ArgumentException("Player did not play in this game");
            else if (Team1.ContainsPlayer(p))
                return Team2Score;
            else
                return Team1Score;
        }

        public bool DidPlayerWin(Player p)
        {
            if (!ContainsPlayer(p))
                throw new ArgumentException("Player did not play in this game");
            else if (Team1.ContainsPlayer(p))//they're team 1
                return Team1Score > Team2Score;
            else//they're team 2
                return Team2Score > Team1Score;
        }

        public bool DidTeamWin(Team t)
        {
            if (!ContainsTeam(t))
                throw new ArgumentException("Team did not play in this game");
            else if (Team1ID == t.TeamID)//they're team 1
                return Team1Score > Team2Score;
            else//they're team 2
                return Team2Score > Team1Score;
        }


        public override string ToString()
        {
            return string.Format("{0}-{1}", Team1.ToString(), Team2.ToString());
        }
    }
}
