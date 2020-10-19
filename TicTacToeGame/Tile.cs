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
        public State TileState { get; set; }

        public enum State
        {
            None, 
            X, 
            O,
        }
    }
}