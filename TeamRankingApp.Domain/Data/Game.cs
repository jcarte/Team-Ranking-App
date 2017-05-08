using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamRankingApp.Domain.Data
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

        
    }
}
