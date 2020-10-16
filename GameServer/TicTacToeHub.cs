using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GameServer
{
    public class TicTacToeHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}