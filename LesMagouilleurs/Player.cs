﻿using Microsoft.Xna.Framework.Graphics;
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

    public class Player
    {
        public const int DEFAULT_MONEY = 200;
        public const int DEFAULT_HP = 10;
        private string name;
        private int money;
        private int hp; // Points de vie (PV)
        private PlayerNumber playerNumber;
        private bool isHuman;
        private PlayerUI playerUI;
        private GamePiece gamePiece;
        private bool active;

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
        public bool IsHuman
        {
            get
            {
                return isHuman;
            }
        }
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                playerUI.PlayerMoney = money.ToString();
            }
        }
        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
                playerUI.PlayerHP = hp.ToString();
            }
        }
        public PlayerUI PlayerUI
        {
            get
            {
                return playerUI;
            }
        }
        public GamePiece GamePiece
        {
            get
            {
                return gamePiece;
            }
        }

        public Player(bool isHuman, PlayerNumber playerNumber, string name, GraphicsDevice graphics)
        {
            this.active = true;
            this.name = name;
            this.playerNumber = playerNumber;
            this.isHuman = isHuman;
            playerUI = new PlayerUI(graphics, playerNumber, name);
            gamePiece = new GamePiece(playerNumber);
            money = DEFAULT_MONEY;
            hp = DEFAULT_HP;
        }
    }
}
