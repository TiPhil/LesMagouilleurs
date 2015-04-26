using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    class Assets
    {
        // Attributs
        private static Assets instance;
        private Button buttonOk;
        private Button buttonRollDice;

        // Proprietes
        public Button ButtonOk { get { return buttonOk; } }
        public Button ButtonRollDice { get { return buttonRollDice; } }
        public static Assets Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Assets();
                }
                return instance;
            }
        }

        // Constructeur
        private Assets() { }

        // Load les assets
        public void Load(GraphicsDeviceManager graphics)
        {
            buttonRollDice = new Button(
                Ressources.Instance.ButtonRollDice,
                graphics.GraphicsDevice,
                new Vector2(146, 146),
                new Vector2(Game1.SCREEN_WIDTH / 2 - 73, Game1.SCREEN_HEIGHT / 2 - 73),
                Ressources.Instance.ButtonClickedSound);

            buttonOk = new Button(
                Ressources.Instance.ButtonCloseRules,
                graphics.GraphicsDevice,
                new Vector2(200, 50),
                new Vector2(540, 500),
                Ressources.Instance.ButtonClickedSound);
        }
    }
}
