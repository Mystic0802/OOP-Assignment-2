using System;
using System.Collections.Generic;

namespace OOPAssignment2
{
    internal class Game
    {
        #region Fields & Properties

        private List<Player> playerList = new List<Player>();
        private List<Die> dice = new List<Die>();

        protected List<Player> PlayerList { get => playerList; set => playerList = value; }
        protected List<Die> Dice { get => dice; set => dice = value; }

        #endregion

        public void StartGame()
        {

        }


    }
}
