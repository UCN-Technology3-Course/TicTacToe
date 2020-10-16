using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer
{
    public class TicTacToe
    {
        // Singleton
        private readonly static Lazy<TicTacToe> _instance = new Lazy<TicTacToe>(() => new TicTacToe());

        public TicTacToe()
        {

        }

        public void JoinGame()
        {

        }

        public void MakeMove(int x, int y)
        {

        }

        private void UpdateOpponent()
        {

        }
    }
}