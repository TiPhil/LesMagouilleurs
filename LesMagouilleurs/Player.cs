using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    public enum PlayerNumber
    {
        P1, P2, P3, P4
    }

    class Player
    {
        private string name;
        private int money;
        private int hp; // Points de vie (PV)
        private PlayerNumber playerNumber;
        private bool isHuman;
        private PlayerInterface playerInterface;

        public int Money
        {
            set
            {
                money = value;
                playerInterface.PlayerMoney = money.ToString();
            }
        }
        public int Hp
        {
            set
            {
                hp = value;
                playerInterface.PlayerHP = hp.ToString();
            }
        }

        public Player(bool isHuman, PlayerNumber playerNumber, string name, PlayerInterface playerInterface)
        {
            this.name = name;
            this.playerNumber = playerNumber;
            this.isHuman = isHuman;
            this.playerInterface = playerInterface;
            money = 200;
            hp = 10;
            playerInterface.PlayerName = name;
            playerInterface.PlayerMoney = money.ToString();
            playerInterface.PlayerHP = hp.ToString();
        }


    }
}
