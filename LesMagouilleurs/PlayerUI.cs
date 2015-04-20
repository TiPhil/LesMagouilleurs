using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    public class PlayerUI
    {
        private int width = 200;
        private int height = 50;
        private Texture2D hudBackgroundTexture;
        private Texture2D hudColorTexture;
        private Rectangle hudBackgroundRectangle = new Rectangle();
        private Rectangle hudColorRectangle = new Rectangle();
        private Color hudBackgroundColor = new Color(255, 255, 255, 1);
        private Color hudColorColor;
        private Vector2 position;

        // player's attributes
        private string playerName;
        private string playerMoney;
        private string playerHP;

        public string PlayerName { set { playerName = value; } }
        public string PlayerMoney { set { playerMoney = value; } }
        public string PlayerHP { set { playerHP = value; } }

        public PlayerUI(GraphicsDevice graphics, PlayerNumber playerNumber, string playerName)
        {
            // Set default values for name, money and HP
            this.playerName = playerName;
            playerMoney = Player.DEFAULT_MONEY.ToString();
            playerHP = Player.DEFAULT_HP.ToString();

            hudBackgroundTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);
            hudColorTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);

            hudBackgroundTexture.SetData(new Color[] { hudBackgroundColor });

            switch (playerNumber)
            {
                case PlayerNumber.P1:
                    hudColorColor = new Color(0, 0, 255, 255);
                    position = new Vector2(0, 0);
                    break;

                case PlayerNumber.P2: ;
                    hudColorColor = new Color(255, 0, 0, 255);
                    position = new Vector2(Game1.SCREEN_WIDTH - width, 0); // TODO
                    break;

                case PlayerNumber.P3: ;
                    hudColorColor = new Color(0, 255, 0, 255);
                    position = new Vector2(0, Game1.SCREEN_HEIGHT - height); // TODO
                    break;

                case PlayerNumber.P4: ;
                    hudColorColor = new Color(255, 255, 0, 255);
                    position = new Vector2(Game1.SCREEN_WIDTH - width, Game1.SCREEN_HEIGHT - height); // TODO
                    break;

                default: ;
                    break;
            }

            hudColorTexture.SetData(new Color[] { hudColorColor });

            hudBackgroundRectangle.Width = width;
            hudBackgroundRectangle.Height = height;

            hudColorRectangle.Width = width;
            hudColorRectangle.Height = height / 5;

            //texture.SetData(new Color[] { myTransparentColor });

        }

        public void Draw(SpriteBatch spriteBatch, Ressources ressources)
        {
            //spriteBatch.Draw(texture, whiteBackground, myTransparentColor);
            spriteBatch.Draw(hudBackgroundTexture, position, hudBackgroundRectangle, hudBackgroundColor);
            spriteBatch.Draw(hudColorTexture, position + new Vector2(0, height / 5 * 4), hudColorRectangle, hudColorColor);
            //spriteBatch.DrawString(ressources.Arial, playerStats, position + new Vector2(9, 9), Color.Black);
            spriteBatch.DrawString(ressources.Arial, playerName + " " + playerMoney + " $ " + playerHP + " PV", position + new Vector2(9, 9), Color.Black);

        }

    }
}
