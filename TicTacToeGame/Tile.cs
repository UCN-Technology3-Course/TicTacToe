using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe
{
    class Tile   
    {
        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Color TileValue { get; set; }

        public bool IsSelected => TileValue == Color.X || TileValue == Color.O;

        public enum Color
        {
            None, 
            X, 
            O,
        }
    }
}