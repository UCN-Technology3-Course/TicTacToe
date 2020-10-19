using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace TicTacToe
{
    class Game
    {
        public dynamic PlayerX { get; set; }
        public dynamic PlayerO { get; set; }
        public dynamic Winner { get; set; }
        public dynamic Looser { get; set; }
        public Guid GameId { get; set; }
        public int Moves { get; set; }

        private Tile[] Board { get; }

        public Game()
        {
            // creating board
            Board = new Tile[]{
                new Tile(){ X = 0, Y = 0, TileState = Tile.State.None }, // 0
                new Tile(){ X = 0, Y = 1, TileState = Tile.State.None }, // 1
                new Tile(){ X = 0, Y = 2, TileState = Tile.State.None }, // 2
                new Tile(){ X = 1, Y = 0, TileState = Tile.State.None }, // 3
                new Tile(){ X = 1, Y = 1, TileState = Tile.State.None }, // 4
                new Tile(){ X = 1, Y = 2, TileState = Tile.State.None }, // 5
                new Tile(){ X = 2, Y = 0, TileState = Tile.State.None }, // 6
                new Tile(){ X = 2, Y = 1, TileState = Tile.State.None }, // 7
                new Tile(){ X = 2, Y = 2, TileState = Tile.State.None }, // 8
            };
        }

        public void SetTileState(int x, int y, Tile.State state)
        {
            Board.Single(t => t.X == x && t.Y == y).TileState = state;
        }

        // Checks the state of the game. 
        public bool CheckGameState()
        {
            // possible winning combinations
            var test = (Board[0].TileState & Board[1].TileState & Board[2].TileState) |
                (Board[3].TileState & Board[4].TileState & Board[5].TileState) | 
                (Board[6].TileState & Board[7].TileState & Board[8].TileState) | 
                (Board[0].TileState & Board[3].TileState & Board[6].TileState) | 
                (Board[1].TileState & Board[4].TileState & Board[7].TileState) | 
                (Board[2].TileState & Board[5].TileState & Board[8].TileState) | 
                (Board[0].TileState & Board[4].TileState & Board[8].TileState) | 
                (Board[2].TileState & Board[4].TileState & Board[3].TileState);

            // setting winner and looser
            switch (test)
            {
                case Tile.State.X:
                    Winner = PlayerX;
                    Looser = PlayerO;
                    break;
                case Tile.State.O:
                    Winner = PlayerO;
                    Looser = PlayerX;
                    break;
            }

            // returns true if the game is finished, otherwise false
            return test != Tile.State.None;
        }
    }
}