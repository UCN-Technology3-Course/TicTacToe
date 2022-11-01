using System;
using System.Linq;

namespace TicTacToe
{
    internal class Game
    {
        internal dynamic PlayerX { get; set; }
        internal dynamic PlayerO { get; set; }
        internal dynamic Winner { get; set; }
        internal dynamic Loser { get; set; }
        internal Guid GameId { get; set; }
        internal int Moves { get; set; }

        private Tile[] Board { get; }

        public Game()
        {
            // creating board
            //  0 | 1 | 2
            // -----------
            //  3 | 4 | 5
            // -----------
            //  6 | 7 | 8
            Board = new Tile[]{
                new Tile(){ X = 0, Y = 0, TileValue = Tile.Color.None }, // 0
                new Tile(){ X = 0, Y = 1, TileValue = Tile.Color.None }, // 1
                new Tile(){ X = 0, Y = 2, TileValue = Tile.Color.None }, // 2
                new Tile(){ X = 1, Y = 0, TileValue = Tile.Color.None }, // 3
                new Tile(){ X = 1, Y = 1, TileValue = Tile.Color.None }, // 4
                new Tile(){ X = 1, Y = 2, TileValue = Tile.Color.None }, // 5
                new Tile(){ X = 2, Y = 0, TileValue = Tile.Color.None }, // 6
                new Tile(){ X = 2, Y = 1, TileValue = Tile.Color.None }, // 7
                new Tile(){ X = 2, Y = 2, TileValue = Tile.Color.None }, // 8
            };
        }


        /// <summary>
        /// Checks and persorms a move if it is valid according to rules
        /// </summary>
        /// <param name="chosen"></param>
        /// <param name="discarded"></param>
        /// <param name="color"></param>
        /// <returns>A value indicating whether the move is valid or not</returns>
        internal bool ValidateAndMove(int selectedX, int selectedY, int? discardedX, int? discardedY, Tile.Color color)
        {
            // Checks if the chosen tile is empty
            if (!Board.Any(t => t.X == selectedX && t.Y == selectedY && !t.IsSelected))
            {
                return false;
            }
            
            // If a tile has been emptied
            if (discardedX.HasValue && discardedY.HasValue)
            {
                // Checks if the discarded tile is not empty and is of current color
                if (!Board.Any(t => t.X == discardedX.Value && t.Y == discardedY.Value && t.TileValue == color))
                {
                    return false;
                }
                // clears discarded tile
                SetTileState(discardedX.Value, discardedY.Value, Tile.Color.None);
            }
            // occupies selected tile
            SetTileState(selectedX, selectedY, color);

            return true;
        }

        private void SetTileState(int x, int y, Tile.Color state)
        {
            Board.Single(t => t.X == x && t.Y == y).TileValue = state;
        }

        // Checks the state of the game. 
        internal bool CheckGameState()
        {
            // Testing for winning combinations
            var test = (Board[0].TileValue & Board[1].TileValue & Board[2].TileValue) |
                       (Board[3].TileValue & Board[4].TileValue & Board[5].TileValue) |
                       (Board[6].TileValue & Board[7].TileValue & Board[8].TileValue) |
                       (Board[0].TileValue & Board[3].TileValue & Board[6].TileValue) |
                       (Board[1].TileValue & Board[4].TileValue & Board[7].TileValue) |
                       (Board[2].TileValue & Board[5].TileValue & Board[8].TileValue) |
                       (Board[0].TileValue & Board[4].TileValue & Board[8].TileValue) |
                       (Board[2].TileValue & Board[4].TileValue & Board[6].TileValue);

            // setting winner and loser
            switch (test)
            {
                case Tile.Color.X:
                    Winner = PlayerX;
                    Loser = PlayerO;
                    break;
                case Tile.Color.O:
                    Winner = PlayerO;
                    Loser = PlayerX;
                    break;
            }

            // returns true if the game is finished, otherwise false
            return test != Tile.Color.None;
        }
    }
}