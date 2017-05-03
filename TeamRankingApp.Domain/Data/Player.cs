using SQLite;

namespace TeamRankingApp.Domain.Data
{
    /// <summary>
    /// A single player
    /// </summary>
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int PlayerID { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }


        public string ImagePath { get; set; }

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


        public static Player Create(string name, string imagePath)
        {
            return new Player()
            {
                Name = name,
                ImagePath = imagePath
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
