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

namespace TeamRankingApp.Domain.OLD
{
    public class Team
    {
        public Player[] Players { get; set; }

        //This takes in two players and checks if the same player is being entered twice.

        public Team(Player A, Player B)
        {
            if (A == B)
                throw new ArgumentException("A team cannot have the same player twice");
            Players = new Player[2] { A, B };
        }

        //This is used by Match to check if A-B is the same as B-A

        //generic not used in project
        public override bool Equals(object obj)
        {
            return (Team)obj == this;
        }
        //generic not used in project
        public override int GetHashCode()
        {
            return (int)Math.Pow(Players[0].PlayerID, Players[1].PlayerID);
        }


        public static bool operator ==(Team a, Team b)
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
            return b.Players.Contains(a.Players[0]) && b.Players.Contains(a.Players[1]);
        }

        //generic not used in project YET
        public static bool operator !=(Team a, Team b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{Players[0]},{Players[1]}";
        }
    }
}