using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    public class TicTacToeGameHub : Hub
    {
        private static readonly List<Game> _games = new List<Game>();

        /// <summary>
        /// Finds an avaiable game to join or creates a new and puts the player in waiting position
        /// </summary>
        public void Join()
        {
            var game = _games.FirstOrDefault(g => g.PlayerO == null);

            if (game != null)
            {
                game.PlayerO = Clients.Caller;
                game.PlayerO.gameCreated(game.GameId, false);
                game.PlayerX.gameCreated(game.GameId, true);
            }
            else
            {
                game = new Game { PlayerX = Clients.Caller, GameId = Guid.NewGuid() };
                _games.Add(game);
            }
        }

        /// <summary>
        /// Performs a move
        /// </summary>
        /// <param name="gameId">The id of the game</param>
        /// <param name="chosen">The tile where the piece is placed</param>
        /// <param name="discarded">When the player has used alle three pieces, this parameter represents the tile that the piece is moved from</param>
        public void Move(Guid gameId, dynamic chosen, dynamic discarded)
        {
            var game = _games.Single(g => g.GameId == gameId);

            Tile.State color = game.Moves++ % 2 == 1 ? Tile.State.O : Tile.State.X;

            if (game.ValidateAndPerformMove((int)chosen.x, (int)chosen.y, (int?)discarded?.x, (int?)discarded?.y, color))
            {
                // Valid move 
                switch (color)
                {
                    case Tile.State.X:
                        game.PlayerO.opponentMove(chosen, discarded);
                        break;
                    case Tile.State.O:
                        game.PlayerX.opponentMove(chosen, discarded);
                        break;
                }

                // check winning conditions
                if (game.CheckGameState())
                {
                    game.Winner.endGame(true);
                    game.Looser.endGame(false);
                }
            }
            else
            {
                // Cheating attempt detected
                switch (color)
                {
                    case Tile.State.X:
                        game.PlayerX.abortGame("You is cheating!");
                        game.PlayerO.abortGame("The opponent are cheating");
                        break;
                    case Tile.State.O:
                        game.PlayerX.abortGame("The opponent are cheating");
                        game.PlayerO.abortGame("You is cheating!");
                        break;
                }
            }
        }
    }
}