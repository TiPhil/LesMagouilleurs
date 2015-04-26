using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    class DialogBox
    {
        private int width;
        private int height;
        private Texture2D hudBackgroundTexture;
        private Rectangle hudBackgroundRectangle = new Rectangle();
        private Color hudBackgroundColor = new Color(255, 255, 255, 1);
        private Vector2 position;
        private string message;
        private Button button;

        public string Message
        {
            set
            {
                message = value;
            }
        }

        public DialogBox(GraphicsDevice graphics, int width, int height, string message, Button button)
        {
            this.message = message;
            this.width = width;
            this.height = height;

            this.button = button;

            hudBackgroundTexture = new Texture2D(graphics, 1, 1, false, SurfaceFormat.Color);

            hudBackgroundTexture.SetData(new Color[] { hudBackgroundColor });

            hudBackgroundRectangle.Width = width;
            hudBackgroundRectangle.Height = height;

            position = new Vector2(Game1.SCREEN_WIDTH / 2 - width / 2, Game1.SCREEN_HEIGHT / 2 - height / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hudBackgroundTexture, position, hudBackgroundRectangle, hudBackgroundColor);
            spriteBatch.DrawString(Ressources.Instance.Arial, message, position + new Vector2(width / 2 - 100, height / 2 - 10), Color.Black); // CENTER THE TEXT IN THE BOX
            button.Draw(spriteBatch);
        }

        public bool IsClicked()
        {
            return button.isClicked();
        }

        public void Update(MouseState mouse)
        {
            button.Update(mouse);
        }
    }
}
