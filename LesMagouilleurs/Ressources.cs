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

        private Model table;
        private Model gamePieceBlue1;
        private Model gamePieceYellow2;
        private Model gamePieceGreen3;
        private Model gamePieceRed4;
        private Model board;
        private Model cubeTest;

        private SoundEffect buttonClicked;

        private SpriteFont arial;


        // Properties
        public Texture2D RulesPanel { get { return rulesPanel; } }
        public Texture2D ButtonCloseRules { get { return buttonCloseRules; } }

        public Model Table { get { return table; } }
        public Model GamePieceBlue1 { get { return gamePieceBlue1; } }
        public Model GamePieceYellow2 { get { return gamePieceYellow2; } }
        public Model GamePieceGreen3 { get { return gamePieceGreen3; } }
        public Model GamePieceRed4 { get { return gamePieceRed4; } }
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

            table = Content.Load<Model>("Models/table");
            gamePieceBlue1 = Content.Load<Model>("Models/gamePieceBlue");
            gamePieceYellow2 = Content.Load<Model>("Models/gamePieceYellow");
            gamePieceGreen3 = Content.Load<Model>("Models/gamePieceGreen");
            gamePieceRed4 = Content.Load<Model>("Models/gamePieceRed");
            board = Content.Load<Model>("Models/board");
            cubeTest = Content.Load<Model>("Models/cubeTest");

            buttonClicked = Content.Load<SoundEffect>("SoundEffects/buttonSound");

            arial = Content.Load<SpriteFont>("Fonts/Arial");
        }
    }
}
