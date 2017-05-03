using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamRankingApp.Domain.Data
{
    public class DataController
    {
        SQLiteConnection db;

        /// <summary>
        /// Where the database is
        /// </summary>
        public string FilePath { get; }

        public DataController(string path)
        {
            this.FilePath = path;
        }

        public void Load()
        { 
            if(string.IsNullOrWhiteSpace(FilePath))
            {
                throw new ArgumentException("No database file path given");
            }
            
            //Init sql lite object, will create file if doesn't already exist
            db = new SQLiteConnection(FilePath);

            //Init all tables, doesn't do anything if they already exist
            db.CreateTable<Player>();
            db.CreateTable<Team>();
            db.CreateTable<Game>();
        }

        public List<Player> GetPlayers()
        {
            List<Player> plays = db.Table<Player>().ToList();
            List<Game> games = GetGames();

            foreach (var p in plays)
            {
                var g = games.Where(gg => gg.ContainsPlayer(p)).ToList();
                p.GamesPlayed = g.Count;
                p.GamesWon = g.Count(gg => gg.DidPlayerWin(p));
                p.SumPointsAgainst = g.Sum(gg => gg.GetPointsAgainst(p));
                p.SumPointsFor = g.Sum(gg => gg.GetPlayerPoints(p));
            }

            return plays;
        }

        public List<Team> GetTeams()
        {
            List<Team> teams = db.Table<Team>().ToList();
            List<Game> games = GetGames();

            foreach (Team t in teams)
            {
                var g = games.Where(gg => gg.ContainsTeam(t)).ToList();
                t.Player1 = db.Find<Player>(t.Player1ID);
                t.Player2 = db.Find<Player>(t.Player2ID);
                t.GamesPlayed = g.Count;
                t.GamesWon = g.Count(gg => gg.DidTeamWin(t));
                t.SumPointsAgainst = g.Sum(gg => gg.GetPointsAgainst(t));
                t.SumPointsFor = g.Sum(gg => gg.GetTeamPoints(t));
            }
            return teams;
        }

        private List<Game> GetGames()
        {
            List<Game> trans = db.Table<Game>().ToList();

            foreach (var t in trans)
            {
                t.Team1 = db.Find<Team>(t.Team1ID);
                t.Team2 = db.Find<Team>(t.Team2ID);
                t.Team1.Player1 = db.Find<Player>(t.Team1.Player1ID);
                t.Team1.Player2 = db.Find<Player>(t.Team1.Player2ID);
                t.Team2.Player1 = db.Find<Player>(t.Team2.Player1ID);
                t.Team2.Player2 = db.Find<Player>(t.Team2.Player2ID);
            }
            return trans;
        }

        public void SaveGame(Game t)
        {
            SaveGames(new List<Game>() { t });
        }

        public void SaveGames(IEnumerable<Game> t)
        {
            db.InsertAll(t);
        }



        private void InsertDefaults()
        {
            List<Player> players = new List<Player>();
            string[] names = new string[] {"Guy","James","Luca","Martin","Megan","Neil","Nicola","Rick"};
            foreach (string n in names)
            {
                players.Add(Player.Create(n));
            }
            db.InsertAll(players);

            List<Team> teams = new List<Team>();
            foreach (Player p1 in players)
            {
                foreach (Player p2 in players)
                {
                    if(p1.PlayerID != p2.PlayerID)
                    {
                        teams.Add(Team.Create(p1, p2));
                    }
                }
            }
            db.InsertAll(teams);
        }
    }
}
