using System;
using System.Linq;

namespace TicTacToe
{
    internal class Game
    {
        internal dynamic PlayerX { get; set; }
        internal dynamic PlayerO { get; set; }
        internal dynamic Winner { get; set; }
        internal dynamic Looser { get; set; }
        internal Guid GameId { get; set; }
        internal int Moves { get; set; }

        private Tile[] Board { get; }

        internal Game()
        {
            // creating board
            Board = new Tile[]{
                new Tile(){ X = 0, Y = 0, TileValue = Tile.State.None }, // 0
                new Tile(){ X = 0, Y = 1, TileValue = Tile.State.None }, // 1
                new Tile(){ X = 0, Y = 2, TileValue = Tile.State.None }, // 2
                new Tile(){ X = 1, Y = 0, TileValue = Tile.State.None }, // 3
                new Tile(){ X = 1, Y = 1, TileValue = Tile.State.None }, // 4
                new Tile(){ X = 1, Y = 2, TileValue = Tile.State.None }, // 5
                new Tile(){ X = 2, Y = 0, TileValue = Tile.State.None }, // 6
                new Tile(){ X = 2, Y = 1, TileValue = Tile.State.None }, // 7
                new Tile(){ X = 2, Y = 2, TileValue = Tile.State.None }, // 8
            };
        }


        /// <summary>
        /// Checks and persorms a move if it is valid according to rules
        /// </summary>
        /// <param name="chosen"></param>
        /// <param name="discarded"></param>
        /// <param name="color"></param>
        /// <returns>A value indicating whether the move is valid or not</returns>
        internal bool ValidateAndPerformMove(int chosenX, int chosenY, int? discardedX, int? discardedY, Tile.State color)
        {
            // Checks if the chosen tile is empty
            if (!Board.Any(t => t.X == chosenX && t.Y == chosenY && t.TileValue == Tile.State.None))
            {
                return false;
            }
            // clears discarded tile
            if (discardedX.HasValue && discardedY.HasValue)
            {
                // Checks if the discarded tile can be discarded
                if (!Board.Any(t => t.X == discardedX.Value && t.Y == discardedY.Value && t.TileValue == color))
                {
                    return false;
                }
                SetTileState(discardedX.Value, discardedY.Value, Tile.State.None);
            }
            // occupies selected tile
            SetTileState(chosenX, chosenY, color);

            return true;
        }

        private void SetTileState(int x, int y, Tile.State state)
        {
            Board.Single(t => t.X == x && t.Y == y).TileValue = state;
        }

        // Checks the state of the game. 
        internal bool CheckGameState()
        {
            // winning combinations
            //  0 | 1 | 2
            // -----------
            //  3 | 4 | 5
            // -----------
            //  6 | 7 | 8
            var test = (Board[0].TileValue & Board[1].TileValue & Board[2].TileValue) |
                       (Board[3].TileValue & Board[4].TileValue & Board[5].TileValue) |
                       (Board[6].TileValue & Board[7].TileValue & Board[8].TileValue) |
                       (Board[0].TileValue & Board[3].TileValue & Board[6].TileValue) |
                       (Board[1].TileValue & Board[4].TileValue & Board[7].TileValue) |
                       (Board[2].TileValue & Board[5].TileValue & Board[8].TileValue) |
                       (Board[0].TileValue & Board[4].TileValue & Board[8].TileValue) |
                       (Board[2].TileValue & Board[4].TileValue & Board[6].TileValue);

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