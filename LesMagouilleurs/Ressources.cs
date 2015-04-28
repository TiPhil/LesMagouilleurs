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
    public class Ressources
    {
        private static Ressources instance;

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

        private Ressources() { }

        public static Ressources Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Ressources();
                }
                return instance;
            }
        }

        // Met en memoire les differentes ressources necessaire au programme
        public void Load(GameServiceContainer Services)
        {
            ContentManager content = new ContentManager(Services, "Content");

            rulesPanel = content.Load<Texture2D>("Textures/rulesPanel");
            buttonCloseRules = content.Load<Texture2D>("Textures/buttonCloseRules");
            buttonRollDice = content.Load<Texture2D>("Textures/buttonDice");

            table = content.Load<Model>("Models/table");
            gamePieceP1 = content.Load<Model>("Models/gamePieceBlue");
            gamePieceP2 = content.Load<Model>("Models/gamePieceRed");
            gamePieceP3 = content.Load<Model>("Models/gamePieceGreen");
            gamePieceP4 = content.Load<Model>("Models/gamePieceYellow");
            board = content.Load<Model>("Models/board");
            cubeTest = content.Load<Model>("Models/cubeTest");

            buttonClicked = content.Load<SoundEffect>("SoundEffects/buttonSound");

            arial = content.Load<SpriteFont>("Fonts/Arial");
        }
    }
}