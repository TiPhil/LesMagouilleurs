using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace LesMagouilleurs
{
    /// <summary>
    /// A container for all the textures, models, sound effects and musics
    /// </summary>
    class Ressources
    {
        // Variables
        private ContentManager Content;
        private GameServiceContainer Services;

        private Texture2D rulesPanel;
        private Texture2D buttonCloseRules;
        private Texture2D buttonRollDice;

        private Model table;
        private Model gamePieceP1;
        private Model gamePieceP2;
        private Model gamePieceP3;
        private Model gamePieceP4;
        private Model board;
        private Model cubeTest;

        private SoundEffect buttonClicked;

        private SpriteFont arial;

        // Properties
        public Texture2D RulesPanel { get { return rulesPanel; } }
        public Texture2D ButtonCloseRules { get { return buttonCloseRules; } }
        public Texture2D ButtonRollDice { get { return buttonRollDice; } }

        public Model Table { get { return table; } }
        public Model GamePieceP1 { get { return gamePieceP1; } }
        public Model GamePieceP2 { get { return gamePieceP2; } }
        public Model GamePieceP3 { get { return gamePieceP3; } }
        public Model GamePieceP4 { get { return gamePieceP4; } }
        public Model Board { get { return board; } }
        public Model CubeTest { get { return cubeTest; } }

        public SoundEffect ButtonClickedSound { get { return buttonClicked; } }

        public SpriteFont Arial { get { return arial; } }


        public Ressources(ContentManager Content, GameServiceContainer Services)
        {
            this.Content = Content;
            this.Services = Services;
        }

        public void Load()
        {
            Content = new ContentManager(Services, "Content");

            rulesPanel = Content.Load<Texture2D>("Textures/rulesPanel");
            buttonCloseRules = Content.Load<Texture2D>("Textures/buttonCloseRules");
            buttonRollDice = Content.Load<Texture2D>("Textures/buttonDice");

            table = Content.Load<Model>("Models/table");
            gamePieceP1 = Content.Load<Model>("Models/gamePieceBlue");
            gamePieceP2 = Content.Load<Model>("Models/gamePieceYellow");
            gamePieceP3 = Content.Load<Model>("Models/gamePieceGreen");
            gamePieceP4 = Content.Load<Model>("Models/gamePieceRed");
            board = Content.Load<Model>("Models/board");
            cubeTest = Content.Load<Model>("Models/cubeTest");

            buttonClicked = Content.Load<SoundEffect>("SoundEffects/buttonSound");

            arial = Content.Load<SpriteFont>("Fonts/Arial");
        }
    }
}
