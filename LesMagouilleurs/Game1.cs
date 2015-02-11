#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

#endregion

namespace LesMagouilleurs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MouseState previousMouseState;

        private Model model;
        private Texture2D background;
        private Texture2D txtLes;
        private Texture2D txtMagouilleurs;

        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800 / 480f, 0.1f, 100f);

        // Menu principal
        enum GameState 
        {
            MainMenu,
            Options,
            Playing
        }

        // Screen Adjustements
        int screenWidth = 800;
        int screenHeight = 600;

        Button btnPlay;
        Button btnRegle;
        Button btnPropos;

        GameState CurrentGameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            // Etat de base de l'application
            CurrentGameState = GameState.MainMenu;

            // Initialisation permettant le Mousse Click
            previousMouseState = Mouse.GetState();

            // Permet de voir la souris a l'ecran
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Screen format
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            graphics.ApplyChanges();

            background = Content.Load<Texture2D>("background");
            txtLes = Content.Load<Texture2D>("txtLes");
            txtMagouilleurs = Content.Load<Texture2D>("txtMagouilleurs");
      
            btnPlay = new Button(Content.Load<Texture2D>("plancheJouer4"), graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(228, 222));

            btnPropos = new Button(Content.Load<Texture2D>("plancheRegle"), graphics.GraphicsDevice);
            btnPropos.setPosition(new Vector2(228, 347));

            btnRegle = new Button(Content.Load<Texture2D>("planchePropos"), graphics.GraphicsDevice);
            btnRegle.setPosition(new Vector2(228, 472));

            model = Content.Load<Model>("cubetest");

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState) {
                case GameState.MainMenu:
                    if (btnPlay.isClicked() == true)
                        CurrentGameState = GameState.Playing;

                    if (btnRegle.isClicked() == true)
                        CurrentGameState = GameState.Playing;

                    if (btnPropos.isClicked() == true)
                        CurrentGameState = GameState.Playing;
                    
                    btnPlay.Update(mouse);
                    btnRegle.Update(mouse);
                    btnPropos.Update(mouse);

                    break;

                case GameState.Playing: break;
            }

            // Si le user vient de faire un click.
            /*if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rectangle area = new Rectangle(2, 10, 20, 20);

                Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);

                if (area.Contains(mousePosition))
                    world *= Matrix.CreateRotationX(-0.05f);

            }

            previousMouseState = Mouse.GetState();*/

            // Faire bouger le monde
            /*if (Keyboard.GetState().IsKeyDown(Keys.Up))
                world *= Matrix.CreateRotationX(-0.05f);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                world *= Matrix.CreateRotationX(0.05f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                world *= Matrix.CreateRotationY(-0.05f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                world *= Matrix.CreateRotationY(0.05f);*/

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
  
            switch (CurrentGameState)
            {
                case GameState.MainMenu:

                    spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.Draw(txtLes, new Rectangle(335, 50, 130, 57), Color.White);
                    spriteBatch.Draw(txtMagouilleurs, new Rectangle(101, 125, 598, 72), Color.White);
                    btnPlay.Draw(spriteBatch);
                    btnPropos.Draw(spriteBatch);
                    btnRegle.Draw(spriteBatch);
                    break;

                case GameState.Playing:

                    DrawModel(model, world, view, projection);
                    
                    break;
            }

            spriteBatch.End();

            //DrawModel(model, world, view, projection);

            base.Draw(gameTime);
        }

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}