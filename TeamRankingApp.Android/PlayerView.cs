using System;
using TeamRankingApp.Domain.Data;

namespace TeamRankingApp.Android
{
    public class PlayerView
    {
        public int Image { get; set; }
        public Player Player { get; set; }

        public PlayerView(){}

        public PlayerView(Player p)
        {
            Player = p;
            Image = GetImageFromName(p.Name);
        }

        private int GetImageFromName(string name)
        {
            switch (name)
            {
                case "Guy": return Resource.Drawable.Guy;
                case "James": return Resource.Drawable.James;
                case "Luca": return Resource.Drawable.Luca;
                case "Martin": return Resource.Drawable.Martin;
                case "Megan": return Resource.Drawable.Megan;
                case "Neil": return Resource.Drawable.Neil;
                case "Nicola": return Resource.Drawable.Nicola;
                case "Rick": return Resource.Drawable.Rick;
                default: throw new ArgumentException("Name unknown");
            }
        }

    }
}