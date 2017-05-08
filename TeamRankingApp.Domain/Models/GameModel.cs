using System;
using System.Collections.Generic;
using System.Linq;
using TeamRankingApp.Domain.Data;

namespace TeamRankingApp.Domain.Models
{
    public class GameModel
    {
        public int Team1ID { get; set; }

        public int Team1Score { get; set; }

        public int Team2ID { get; set; }

        public int Team2Score { get; set; }

        public Team Team1 { get; set; }

        public Team Team2 { get; set; }

        public Team[] Teams { get { return new Team[] { Team1, Team2 }; } }

        public List<Player> OffCourtPlayers { get; set; }

        public GameModel() { }


        public static GameModel Create(Team t1, int t1Score, Team t2, int t2Score)
        {
            return Create(t1, t1Score, t2, t2Score, new List<Player>());
        }


        /// <summary>
        /// Static creator
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t1Score"></param>
        /// <param name="t2"></param>
        /// <param name="t2Score"></param>
        /// <returns></returns>
        public static GameModel Create(Team t1, int t1Score, Team t2, int t2Score, List<Player> allPlayers)
        {
            return new GameModel()
            {
                Team1ID = t1.TeamID,
                Team1Score = t1Score,
                Team2ID = t2.TeamID,
                Team2Score = t2Score,
                Team1 = t1,
                Team2 = t2,
                OffCourtPlayers = allPlayers.Except(t1.Players.Union(t2.Players)).ToList()
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

        public GameModel Clone()
        {
            return new GameModel()
            {
                OffCourtPlayers = this.OffCourtPlayers,
                Team1ID = this.Team1ID,
                Team1 = this.Team1,
                Team1Score = this.Team1Score,
                Team2ID = this.Team2ID,
                Team2 = this.Team2,
                Team2Score = this.Team2Score
            };
        }

        public override bool Equals(object obj)
        {
            return (GameModel)obj == this;
        }
        public override int GetHashCode()
        {
            return (int)Math.Pow(Team1ID.GetHashCode(), Team2ID.GetHashCode());
        }

        //Does this break database logic?
        public static bool operator ==(GameModel a, GameModel b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return b.Teams.Contains(a.Teams[0]) && b.Teams.Contains(a.Teams[1]);
        }

        public static bool operator !=(GameModel a, GameModel b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Team1.ToString(), Team2.ToString());
        }
    }
}