using SQLite;

namespace TeamRankingApp.Domain
{
    /// <summary>
    /// Two player team
    /// </summary>
    public class Team
    {
        [PrimaryKey, AutoIncrement]
        public int TeamID { get; set; }

        public int Player1ID { get; set; }

        public int Player2ID { get; set; }

        [Ignore]
        public Player Player1 { get; set; }

        [Ignore]
        public Player Player2 { get; set; }
        
        /// <summary>
        /// Number of games played
        /// </summary>
        [Ignore]
        public int GamesPlayed { get; set; }

        /// <summary>
        /// Number of games won
        /// </summary>
        [Ignore]
        public int GamesWon { get; set; }

        /// <summary>
        /// Total points scored by this team
        /// </summary>
        [Ignore]
        public int SumPointsFor { get; set; }

        /// <summary>
        /// Total points scored against this team
        /// </summary>
        [Ignore]
        public int SumPointsAgainst { get; set; }

        /// <summary>
        /// Does this team contain a given player
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ContainsPlayer(Player p)
        {
            return Player1ID == p.PlayerID || Player2ID == p.PlayerID;
        }

        public static Team Create(Player p1, Player p2)//TODO force in id order?
        {
            return new Data.Team()
            {
                Player1ID = p1.PlayerID,
                Player2ID = p2.PlayerID
            };
        }

        public override string ToString()
        {
            return string.Format("({0},{1})",Player1.ToString(),Player2.ToString());
        }
    }
}
