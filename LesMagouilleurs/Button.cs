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
    public class Button
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private Color color;
        private bool clicked;
        private Vector2 size;
        private SoundEffect clickedSound;
        private bool haveSound;
        private static double lastClick;

        // Contructor for a button WITHOUT a sound effect
        public Button(Texture2D newTexture, GraphicsDevice graphics, Vector2 size, Vector2 position)
        {
            lastClick = 0;
            texture = newTexture;
            color = Color.White;
            this.size = size;
            this.position = position;
            haveSound = false;
        }

        // Contructor for a button WITH a sound effect
        public Button(Texture2D newTexture, GraphicsDevice graphics, Vector2 size, Vector2 position, SoundEffect clickedSound)
        {
            texture = newTexture;
            color = Color.White;
            this.size = size;
            this.position = position;
            this.clickedSound = clickedSound;
            haveSound = true;
        }

        public void Update(MouseState mouse, double totalGameTimeMilliseconds) {
            clicked = false;
            rectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (color == Color.White)
                    color = Color.LightGray;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (!clicked && totalGameTimeMilliseconds - lastClick > 500)
                    {
                        clicked = true;
                        lastClick = totalGameTimeMilliseconds;
                    } 
                }

            }
            else if (color == Color.LightGray)
            {
                color = Color.White;
                clicked = false;
            }
        }

        public void setPosition(Vector2 newPosition) {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, rectangle, color);
        }

        public bool isClicked()
        {
            if (haveSound && clicked)
                clickedSound.Play(); 
            return clicked;
        }
    }
}
