using System;
using System.Collections.Generic;
using System.Text;

namespace CardsApp.Classes
{
    class GameInfo
    {
        public List<Player> Players;
        public Game GameType;
        public int GameUniqueId;
        public decimal Entry;
        public bool Finished;
        public string GameGuid;
        public Player Winner;
        public DateTime GameCompleted;
    }
}
