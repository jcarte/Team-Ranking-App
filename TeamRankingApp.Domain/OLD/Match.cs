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
    public class Match
    {
        public Team[] Teams { get; }
        public List<Player> OffCourtPlayers { get; set; }

        public Match(Team A, Team B, List<Player> AllPlayers)
        {
            if (A == B)
                throw new ArgumentException("A team cannot play itself");
            if(A.Players.Union(B.Players).Distinct().Count()!= 4)//merge player list, remove duplicates and check number is still 4
                throw new ArgumentException("A player cannot play on both teams");
            if(A.Players.Except(AllPlayers).Count() != 0 || B.Players.Except(AllPlayers).Count() != 0) //A-All and B-All should be 0 in both cases
                throw new ArgumentException("Players in both teams must be in all players");

            Teams = new Team[2] { A, B };
            
            OffCourtPlayers = AllPlayers.Except(A.Players).Except(B.Players).ToList();
            //TODO - check all players contains a and b
        }

        public override bool Equals(object obj)
        {
            return (Match)obj == this;
        }
        public override int GetHashCode()
        {
            return (int)Math.Pow(Teams[0].GetHashCode(), Teams[1].GetHashCode());
        }

        public static bool operator ==(Match a, Match b)
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

        public static bool operator !=(Match a, Match b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"[{Teams[0]}],[{Teams[1]}]";
        }
    }
}