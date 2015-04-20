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
        // Configs
        private bool cameraControl = true;
        private bool controlableCube = false;

        // GameStates
        private enum GameStates { ReadingRules, MovingGamePiece, RollingDice }
        private GameStates currentGameState;
        private GameStates previousGameState;

        // Window's size
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 768;

        // Misc
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        // Buttons - CREATE AN ASSETCONTAINER CLASS
        private Button buttonCloseRules;
        private Button buttonRollDice;
        /* TODO
        private Button buttonRerollDice;
        private Button buttonMoveToSpace;
        private Button buttonMagouille;
        private Button buttonMegaMagouille;
        private Button buttonMoneyForLife;
        private Button buttonLifeForMoney;
        private Button buttonFinishTurn;
         */

        // Matrix
        private Matrix worldTable = Matrix.CreateTranslation(new Vector3(0, -0.25f, 0));
        private Matrix worldGamePieceTest = Matrix.CreateTranslation(new Vector3(0, 0.5f, 0)); // to delete
        private Matrix worldBoard = Matrix.CreateScale(1.5f) * Matrix.CreateTranslation(new Vector3(0, 0, -1));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 13, 0), new Vector3(0, 0, 0), Vector3.Negate(Vector3.UnitZ));
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT, 0.1f, 100f);
        
        // (2.8f, 0.3f, -3.8f) => millieu de la case en haut a droite
        // (2.8f, 0.3f, 1.8f) => milieu de la case en bas a droite

        // Players
        private Player player1;
        private Player player2;
        private Player player3;
        private Player player4;
        private Player currentPlayer;

        // Vectors
        private Vector3 position = new Vector3(2.4f, 0.3f, 1.4f); // what is this ?

        private float compteur = 0;

        public Game1()
        {
            // Graphic stuff
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            previousGameState = GameStates.RollingDice; // why??

            // Initialisation permettant le Mousse Click
            previousMouseState = Mouse.GetState();

            // Permet de voir la souris a l'ecran
            IsMouseVisible = true;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            player1 = new Player(true, PlayerNumber.P1, "Joueur1", graphics.GraphicsDevice);
            player2 = new Player(false, PlayerNumber.P2, "Bot2", graphics.GraphicsDevice);
            player3 = new Player(false, PlayerNumber.P3, "Bot3", graphics.GraphicsDevice);
            player4 = new Player(false, PlayerNumber.P4, "Bot4", graphics.GraphicsDevice);

            currentPlayer = player1;
        }

        protected override void LoadContent()
        {
            Ressources.Instance.Load(this.Services);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Buttons
            buttonCloseRules = new Button(
                Ressources.Instance.ButtonCloseRules,
                graphics.GraphicsDevice,
                new Vector2(200, 50),
                new Vector2(540, 645),
                Ressources.Instance.ButtonClickedSound);

            buttonRollDice = new Button(
                Ressources.Instance.ButtonRollDice,
                graphics.GraphicsDevice,
                new Vector2(146, 146),
                new Vector2(700, 600),
                Ressources.Instance.ButtonClickedSound);
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
                    else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Ressources.Instance.ButtonClickedSound.Play();
                        currentGameState = previousGameState;
                    }
                    buttonCloseRules.Update(currentMouseState);
                    break;

                case GameStates.RollingDice:
                    compteur = 0;

                    //if (buttonRollDice.isClicked() == true)
                        // do something
                    if (buttonRollDice.isClicked())
                    {
                        currentGameState = GameStates.MovingGamePiece;
                        //worldPieceP1 = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(new Vector3(1.5f, 0.3f, 1.4f));
                    }

                    if (controlableCube)
                    {
                        // (Y) Move the blue cube
                        if (Keyboard.GetState().IsKeyDown(Keys.Y))
                        {
                            worldGamePieceTest *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, -0.1f));
                        }
                        // (H) Move the blue cube
                        if (Keyboard.GetState().IsKeyDown(Keys.H))
                        {
                            worldGamePieceTest *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, 0.1f));
                        }
                        // (G) Move the blue cube
                        if (Keyboard.GetState().IsKeyDown(Keys.G))
                        {
                            worldGamePieceTest *= Matrix.CreateTranslation(new Vector3(-0.1f, 0.0f, 0.0f));
                        }
                        // (J) Move the blue cube
                        if (Keyboard.GetState().IsKeyDown(Keys.J))
                        {
                            worldGamePieceTest *= Matrix.CreateTranslation(new Vector3(0.1f, 0.0f, 0.0f));
                        }
                    }

                    if (cameraControl)
                    {
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

                        // (ARROWS) Moves the angle of the camera - Look up, down, left or right
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

                        // (W, S, A, D, SPACEBAR) Moves the position of the camera - Move forward, backward, strafe left or strafe right
                        if (Keyboard.GetState().IsKeyDown(Keys.W))
                        {
                            view *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, 0.1f));
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.S))
                        {
                            view *= Matrix.CreateTranslation(new Vector3(0.0f, 0.0f, -0.1f));
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            view *= Matrix.CreateTranslation(new Vector3(0.1f, 0.0f, 0.0f));
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.D))
                        {
                            view *= Matrix.CreateTranslation(new Vector3(-0.1f, 0.0f, 0.0f));
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            view *= Matrix.CreateTranslation(new Vector3(0.0f, -0.1f, 0.0f));
                        }
                    }
                    buttonRollDice.Update(currentMouseState);

                    break;

                case GameStates.MovingGamePiece:
                    // TO DELETE! 
                    //float x = 0.1f;
                    //float y = 0;
                    //x += 0.01f;
                    //compteur += 0.01f;
                    //position += Vector3.Transform(new Vector3(0.05f, (float)Math.Sin(y), 0), Matrix.CreateRotationY(MathHelper.ToRadians(180)));
                    //worldPieceP1 = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(position);
                    //player1.GamePiece.World = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(position);



                    buttonRollDice.Update(currentMouseState);
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

            DrawUI();

            switch (currentGameState)
            {
                case GameStates.ReadingRules:
                    spriteBatch.Draw(Ressources.Instance.RulesPanel, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), Color.White);
                    buttonCloseRules.Draw(spriteBatch);
                    goto case GameStates.RollingDice;

                case GameStates.RollingDice:
                    buttonRollDice.Draw(spriteBatch);
                    goto case GameStates.MovingGamePiece;

                case GameStates.MovingGamePiece:
                    goto default;

                default:
                    DrawModel(Ressources.Instance.Table, worldTable, view, projection);
                    DrawModel(Ressources.Instance.Board, worldBoard, view, projection);
                    DrawModel(Ressources.Instance.GamePieceP1, player1.GamePiece.World, view, projection);
                    DrawModel(Ressources.Instance.GamePieceP2, player2.GamePiece.World, view, projection);
                    DrawModel(Ressources.Instance.GamePieceP3, player3.GamePiece.World, view, projection);
                    DrawModel(Ressources.Instance.GamePieceP4, player4.GamePiece.World, view, projection);

                    //buttonRollDice.Draw(spriteBatch);
                    //DrawModel(ressources.CubeTest, worldBoard, view, projection);
                    if (controlableCube)
                    {
                        DrawModel(Ressources.Instance.GamePieceP1, worldGamePieceTest, view, projection); 
                    }
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawUI()
        {
            player1.PlayerUI.Draw(spriteBatch, Ressources.Instance);
            player2.PlayerUI.Draw(spriteBatch, Ressources.Instance);
            player3.PlayerUI.Draw(spriteBatch, Ressources.Instance);
            player4.PlayerUI.Draw(spriteBatch, Ressources.Instance);
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
                    //effect.EnableDefaultLighting();

                    effect.LightingEnabled = true;
                    effect.DirectionalLight0.Direction = new Vector3(0, -1, 0); // La lumiere vient d'en haut de la table
                    effect.DirectionalLight0.SpecularColor = new Vector3(0.2f, 0.18f, 0.14f);
                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.1f, 0.08f, 0.4f);
                    effect.AmbientLightColor = new Vector3(0.4f, 0.35f, 0.25f);
                    effect.EmissiveColor = new Vector3(0.4f, 0.35f, 0.25f);

                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }
    }
}