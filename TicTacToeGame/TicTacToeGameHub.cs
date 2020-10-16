using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TicTacToe
{
    public class TicTacToeGameHub : Hub
    {
        private static readonly List<Game> _games = new List<Game>();

        public void Hello()
        {
            Clients.All.hello();
        }


        public void Join()
        {
            var game = _games.FirstOrDefault(g => g.PlayerO == null);

            if(game != null)
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

        public void Move(Guid gameId, dynamic chosen, dynamic discarded)
        {
            var game = _games.Single(g => g.GameId == gameId);

            if (discarded != null)
            {
                game.SetTileState((int)discarded.x, (int)discarded.y, Tile.State.None);
            }

            if (game.Moves++ % 2 == 1) // Player O
            {
                game.SetTileState((int)chosen.x, (int)chosen.y, Tile.State.O);
                game.PlayerX.opponentMove(chosen, discarded);
            }
            else // Player X
            {
                game.SetTileState((int)chosen.x, (int)chosen.y, Tile.State.X);
                game.PlayerO.opponentMove(chosen, discarded);
            }

            if (game.CheckGameState())
            {
                game.Winner.endGame(true);
                game.Looser.endGame(false);
            }
        }

        
    }
}