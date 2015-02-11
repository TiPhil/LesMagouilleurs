using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace LesMagouilleurs
{
    class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private Color color;
        private bool clicked;

        private Vector2 size;

        public Button(Texture2D newTexture, GraphicsDevice graphics) {
            texture = newTexture;
            color = new Color(255, 255, 255, 255);

            //ScreenW = 800, ScreenH = 600;
            //ImgW = 344, ImgH = 115;
            size = new Vector2(graphics.Viewport.Width / 2.33f, graphics.Viewport.Height / 5.22f);
        }



        public void Update(MouseState mouse) { 
            rectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.B == 255)
                    color.B = 0;

                if (mouse.LeftButton == ButtonState.Pressed) clicked = true;

            }
            else if (color.B == 0) {
                color.B = 255;
                clicked = false;
            }
        }

        public void setPosition(Vector2 newPosition) {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, rectangle, color);
        }

        public bool isClicked() {
            return clicked;
        }

    }
}
