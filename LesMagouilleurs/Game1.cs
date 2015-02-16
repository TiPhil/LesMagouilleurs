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
        // GameStates
        private enum GameStates { ReadingRules, Waiting, RollingDice, RerollingDice, Magouille, MoneyLife }
        private GameStates currentGameState;
        private GameStates previousGameState;

        // Window's size
        private const int SCREEN_WIDTH = 1280;
        private const int SCREEN_HEIGHT = 768;

        // Misc
        private Ressources ressources;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        // Buttons
        private Button buttonCloseRules;
        private Button buttonRollDice;
        private Button buttonRerollDice;
        private Button buttonMoveToSpace;
        private Button buttonMagouille;
        private Button buttonMegaMagouille;
        private Button buttonMoneyForLife;
        private Button buttonLifeForMoney;
        private Button buttonFinishTurn;

        // Matrix
        private Matrix worldBoard = Matrix.CreateTranslation(new Vector3(0, -0.25f, 0));
        private Matrix worldGamePieceBlue1 = Matrix.CreateTranslation(new Vector3(0, 0.5f, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 13, 0), new Vector3(0, 0, 0), Vector3.Negate(Vector3.UnitZ));
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT, 0.1f, 100f);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ressources = new Ressources(Content, this.Services);

            // Screen format
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
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
            currentGameState = GameStates.ReadingRules;
            previousGameState = GameStates.RollingDice;

            // Initialisation permettant le Mousse Click
            previousMouseState = Mouse.GetState();

            // Permet de voir la souris a l'ecran
            IsMouseVisible = true;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        protected override void LoadContent()
        {
            ressources.Load();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            buttonCloseRules = new Button(
                ressources.ButtonCloseRules,
                graphics.GraphicsDevice,
                new Vector2(200, 50),
                new Vector2(540, 645),
                ressources.ButtonClickedSound);
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
                Exit();

            currentMouseState = Mouse.GetState();

            switch (currentGameState) {

                case GameStates.ReadingRules:
                    if (buttonCloseRules.isClicked() == true)
                        currentGameState = previousGameState;
                    buttonCloseRules.Update(currentMouseState);
                    break;

                case GameStates.RollingDice:

                    // (C) Change the view for a side view of the game
                    if (Keyboard.GetState().IsKeyDown(Keys.C))
                    {
                        view = Matrix.CreateLookAt(new Vector3(0, 0, 13), new Vector3(0, 0, 0), Vector3.UnitY);
                    }
                    // (V) Reset the camera position to the default one
                    if (Keyboard.GetState().IsKeyDown(Keys.V))
                    {
                        view = Matrix.CreateLookAt(new Vector3(0, 13, 0), new Vector3(0, 0, 0), Vector3.Negate(Vector3.UnitZ));
                    }

                    // Moves the angle of the camera - Look up, down, left or right
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        view *= Matrix.CreateRotationX(-0.02f);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        view *= Matrix.CreateRotationX(0.02f);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        view *= Matrix.CreateRotationY(-0.02f);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        view *= Matrix.CreateRotationY(0.02f);
                    }

                    // Moves the position of the camera - Move forward, backward, strafe left or strafe right
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        view *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, +0.1f));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        view *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, -0.1f));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        view *= Matrix.CreateTranslation(new Vector3(+0.1f, 0.0f, 0.0f));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        view *= Matrix.CreateTranslation(new Vector3(-0.1f, 0.0f, 0.0f));
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        view *= Matrix.CreateTranslation(new Vector3(0.0f, -0.1f, 0.0f));
                    }
                    break;
            }

            // TO DELETE
            // Si le user vient de faire un click.
            /*if (previousMouseState.LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rectangle area = new Rectangle(2, 10, 20, 20);

                Point mousePosition = new Point(Mouse.GetState().X, Mouse.GetState().Y);

                if (area.Contains(mousePosition))
                    world *= Matrix.CreateRotationX(-0.05f);
            }
            previousMouseState = Mouse.GetState();*/

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Teal);
            SetupStates();
            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameStates.ReadingRules:
                    spriteBatch.Draw(ressources.RulesPanel, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
                    buttonCloseRules.Draw(spriteBatch);
                    goto case GameStates.RollingDice;

                case GameStates.RollingDice:
                    DrawModel(ressources.Table, worldBoard, view, projection);
                    DrawModel(ressources.GamePieceBlue1, worldGamePieceBlue1, view, projection); 
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetupStates()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
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