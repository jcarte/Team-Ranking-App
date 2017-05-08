using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeamRankingApp.Domain.Data;
using TeamRankingApp.Domain.Models;

namespace TeamRankingApp.Domain
{
    //Install-Package sqlite-net-pcl
    public class Database
    {
        //TODO give to front end
        private const string DB_FILE_NAME = "teamdb.db3";
        private static string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), DB_FILE_NAME);


        public Database()
        {
            this.FilePath = dbPath;
        }

        SQLiteConnection db;

        /// <summary>
        /// Where the database is
        /// </summary>
        public string FilePath { get; }

        public Database(string path)
        {
            this.FilePath = path;
        }

        public void Load()
        { 
            //TODO remove
            //if(File.Exists(FilePath))
            //{
            //    File.Delete(FilePath);
            //}
            

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

            //todo remove
            if(!db.Table<Player>().Any())
            {
                InsertDefaults();
            }
        }

        public List<Player> GetPlayers()
        {
            List<Player> plays = db.Table<Player>().ToList();
            List<GameModel> games = GetGames();

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
            List<GameModel> games = GetGames();

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

        private List<GameModel> GetGames()
        {
            List<Game> trans = db.Table<Game>().ToList();
            List<GameModel> games = new List<GameModel>();

            foreach (var t in trans)
            {
                var Team1 = db.Find<Team>(t.Team1ID);
                var Team2 = db.Find<Team>(t.Team2ID);
                Team1.Player1 = db.Find<Player>(Team1.Player1ID);
                Team1.Player2 = db.Find<Player>(Team1.Player2ID);
                Team2.Player1 = db.Find<Player>(Team2.Player1ID);
                Team2.Player2 = db.Find<Player>(Team2.Player2ID);

                games.Add(GameModel.Create(Team1, t.Team1Score, Team2, t.Team2Score));
            }
            return games;
        }

        public void SaveGame(GameModel t)
        {
            SaveGames(new List<GameModel>() { t });
        }

        public void SaveGames(IEnumerable<GameModel> t)
        {
            db.InsertAll(t.Select(g => new Game()
            {
                Created = DateTime.Now,
                Team1ID = g.Team1ID,
                Team1Score = g.Team1Score,
                Team2ID = g.Team2ID,
                Team2Score = g.Team2Score
            }));
        }



        private void InsertDefaults()
        {
            List<Player> players = new List<Player>();
            string[] names = new string[] {"Guy","James","Luca","Martin","Megan","Neil","Nicola","Rick"};
            foreach (string n in names)
            {
                players.Add(Player.Create(n,n + ".png"));//todo check image path is right
            }
            db.InsertAll(players);

            List<Team> teams = new List<Team>();
            foreach (Player p1 in players)
            {
                foreach (Player p2 in players)
                {
                    if(p1.PlayerID < p2.PlayerID)
                    {
                        teams.Add(Team.Create(p1, p2));
                    }
                }
            }
            db.InsertAll(teams);
        }
    }
}
