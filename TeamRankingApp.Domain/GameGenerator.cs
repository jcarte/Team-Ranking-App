using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;

namespace TeamRankingApp.Domain
{
    public class GameGenerator
    {
        
       

        /// <summary>
        /// Match Generator Constructor
        /// </summary>
        /// <param name="players">All players playing in this session and to be considered for matches</param>
        public GameGenerator(List<Player> players,List<Team> teams)
        {

        }

        /// <summary>
        /// Get all match configurations
        /// </summary>
        /// <returns>Get all matches for the currently playing players</returns>
        /// 
        public List<Game> GetGames(int n)
        {

        }
    }
}


/*
 * ------New Structure----
 * Expose database class to front end, fe chooses save path etc
 * fe chooses players in this session - from these players filters all teams down to only the teams containing those chosen players
 * Players and teams given to game generator and stored
 * Need to make game collection
 * on GetGames uses players (to work out players off court - need to add this to game) and teams to generate games, adds this to collection only when chosen on front end?
 * /