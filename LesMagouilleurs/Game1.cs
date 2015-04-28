#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

#endregion

namespace LesMagouilleurs
{
    // Class principale du jeu
    public class Game1 : Game
    {
        // Configs
        private bool cameraControl = true;

        // GameStates
        private enum GameStates { ReadingRules, MovingGamePiece, RollingDice, ShowDiceResult, ShowSquareEffect,
            ApplySquareEffect, ControlableCamera, EndGame }
        private GameStates currentGameState;

        // Window's size
        public const int SCREEN_WIDTH = 1280;
        public const int SCREEN_HEIGHT = 768;

        // Misc
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        // Random and dice results
        private Random random = new Random();
        private int diceResults;
        private int moveToPosition;

        // Matrix
        private Matrix worldTable = Matrix.CreateTranslation(new Vector3(0, -0.25f, 0));
        private Matrix worldBoard = Matrix.CreateScale(1.5f) * Matrix.CreateTranslation(new Vector3(0, 0, -1));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 10, -1), new Vector3(0, 0, -1), Vector3.Negate(Vector3.UnitZ));
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), (float)SCREEN_WIDTH / (float)SCREEN_HEIGHT, 0.1f, 100f);
        
        // Players
        private Player player1;
        private Player player2;
        private Player player3;
        private Player player4;
        private Player currentPlayer;

        // Effet de la case
        private BoardTile.EffectType effectType;

        // Vectors
        private Vector3 position = new Vector3(2.4f, 0.3f, 1.4f); // what is this ?

        // DialogBoxes
        private DialogBox dialogBox;

        // Le constructeur de Game1
        public Game1()
        {
            // Graphic stuff
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Screen format
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.IsFullScreen = false;   // Le jeu n'est pas en plein ecran a cause de bugs dans MonoGames
            graphics.ApplyChanges();
        }

        // Effectu les initialisations necessaire au bon fonctionnement du jeu
        protected override void Initialize()
        {
            base.Initialize();

            // Etat de base de l'application
            currentGameState = GameStates.RollingDice;

            // Initialisation permettant le Mousse Click
            previousMouseState = Mouse.GetState();

            // Permet de voir la souris a l'ecran
            IsMouseVisible = true;

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // Cree les joueurs
            player1 = new Player(true, PlayerNumber.P1, "Joueur1", graphics.GraphicsDevice);
            player2 = new Player(false, PlayerNumber.P2, "Ordi2", graphics.GraphicsDevice);
            player3 = new Player(false, PlayerNumber.P3, "Ordi3", graphics.GraphicsDevice);
            player4 = new Player(false, PlayerNumber.P4, "Ordi4", graphics.GraphicsDevice);

            // Le joueur1 debute son tour en premier
            currentPlayer = player1;
        }

        // Load le contenu pour une utilisation ulterieur
        protected override void LoadContent()
        {
            // Load les ressources et les assets
            Ressources.Instance.Load(this.Services);
            Assets.Instance.Load(graphics);

            // Create dialog boxes
            dialogBox = new DialogBox(graphics.GraphicsDevice, 345, 345, "", Assets.Instance.ButtonOk);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;            
        }

        // Met a jour la logique du jeu
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentMouseState = Mouse.GetState();

            switch (currentGameState) {

                // Etat ou le joueur doit bracer les dees
                case GameStates.RollingDice:
                    Assets.Instance.ButtonRollDice.Update(currentMouseState, gameTime.TotalGameTime.TotalMilliseconds);

                    if (currentPlayer.IsHuman && Assets.Instance.ButtonRollDice.isClicked() ||
                        !currentPlayer.IsHuman)
                    {
                        currentGameState = GameStates.ShowDiceResult;

                        diceResults = random.Next(1, 7);
                        diceResults += random.Next(1, 7);

                        dialogBox.Message = currentPlayer.Name + " a brasse " + diceResults.ToString() + ".";

                        moveToPosition = currentPlayer.GamePiece.GetNextPosition(diceResults);
                    }
                    goto case GameStates.ControlableCamera;

                // Etat pour afficher le resultat des dees
                case GameStates.ShowDiceResult: ;
                    dialogBox.Update(currentMouseState, gameTime.TotalGameTime.TotalMilliseconds);

                    if (dialogBox.IsClicked())
                    {
                        currentGameState = GameStates.MovingGamePiece;
                    }
                    goto case GameStates.ControlableCamera;

                // Etat pour le mouvement des pieces des joueurs
                case GameStates.MovingGamePiece:
                    if (currentPlayer.GamePiece.Move(moveToPosition))
                    {
                        effectType = BoardTile.GetEffectType(currentPlayer.GamePiece.Position);
                        dialogBox.Message = BoardTile.GetEffectTypeMessage(effectType);
                        currentGameState = GameStates.ShowSquareEffect;
                    }
                    goto case GameStates.ControlableCamera;

                // Etat qui affiche l'effet d'une case sur les joueurs
                case GameStates.ShowSquareEffect: ;
                    dialogBox.Update(currentMouseState, gameTime.TotalGameTime.TotalMilliseconds);

                    if (dialogBox.IsClicked())
                    {
                        currentGameState = GameStates.ApplySquareEffect;
                    }

                    goto case GameStates.ControlableCamera;

                // Etat qui effectu l'effet d'une case sur les joueurs
                case GameStates.ApplySquareEffect: ;

                    BoardTile.ApplyEffect(currentPlayer, GetOtherPlayers(), effectType);

                    // Verifier s'il faut eliminer certain joueur en fonction de leur PV
                    CheckPlayerElimination();

                    // Verifier s'il y a un gagnant (le dernier joueur restant en vie)
                    if (CheckEndGame())
                    {
                        currentGameState = GameStates.EndGame;
                    }
                    else
                    {
                        currentPlayer = NextPlayer();
                        currentGameState = GameStates.RollingDice;
                    }

                    goto case GameStates.ControlableCamera;

                case GameStates.EndGame:;
                    dialogBox.Update(currentMouseState, gameTime.TotalGameTime.TotalMilliseconds);
                    dialogBox.Message = GetWinningPlayer().Name + " a gagne la partie!";
                    if (dialogBox.IsClicked())
                        this.Exit();
                    goto case GameStates.ControlableCamera;

                // Etat permettant le control de la camera
                case GameStates.ControlableCamera:
                    if (cameraControl)
                    {
                        view = CameraController.ControlCamera(view);
                    }
                    break;
            }
            base.Update(gameTime);
        }

        // Dessine a l'ecran les models et textures
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            SetupStates();
            spriteBatch.Begin();

            // Dessine a l'ecran le UI de chaque joueur
            DrawUI();

            switch (currentGameState)
            {
                // Etat ou le joueur doit bracer les dees
                case GameStates.RollingDice:
                    if (currentPlayer.IsHuman)
                    {
                        Assets.Instance.ButtonRollDice.Draw(spriteBatch);
                    }
                    goto default;

                // Etat pour afficher le resultat des dees
                case GameStates.ShowDiceResult: ;
                    dialogBox.Draw(spriteBatch);
                    goto default;

                // Etat pour le mouvement des pieces des joueurs
                case GameStates.MovingGamePiece:
                    goto default;

                // Etat qui affiche l'effet d'une case sur les joueurs
                case GameStates.ShowSquareEffect: ;
                    dialogBox.Draw(spriteBatch);
                    goto default;

                // Etat qui effectu l'effet d'une case sur les joueurs
                case GameStates.ApplySquareEffect: ;
                    goto default;

                case GameStates.EndGame: ;
                    dialogBox.Draw(spriteBatch);
                    goto default;

                default:
                    DrawModel(Ressources.Instance.Table, worldTable, view, projection);
                    DrawModel(Ressources.Instance.Board, worldBoard, view, projection);
                    if(player1.Active)
                        DrawModel(Ressources.Instance.GamePieceP1, player1.GamePiece.World, view, projection);
                    if(player2.Active)
                        DrawModel(Ressources.Instance.GamePieceP2, player2.GamePiece.World, view, projection);
                    if(player3.Active)
                        DrawModel(Ressources.Instance.GamePieceP3, player3.GamePiece.World, view, projection);
                    if(player4.Active)
                        DrawModel(Ressources.Instance.GamePieceP4, player4.GamePiece.World, view, projection);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Dessine le UI de chaque joueur
        private void DrawUI()
        {
            if(player1.Active)
                player1.PlayerUI.Draw(spriteBatch);
            if(player2.Active)
                player2.PlayerUI.Draw(spriteBatch);
            if(player3.Active)
                player3.PlayerUI.Draw(spriteBatch);
            if(player4.Active)
                player4.PlayerUI.Draw(spriteBatch);
        }

        // Fait la liste des autres joueurs a qui se n'est pas leur tour
        private List<Player> GetOtherPlayers()
        {
            List<Player> otherPlayerList = new List<Player>();

            if (currentPlayer != player1)
                otherPlayerList.Add(player1);
            if (currentPlayer != player2)
                otherPlayerList.Add(player2);
            if (currentPlayer != player3)
                otherPlayerList.Add(player3);
            if (currentPlayer != player4)
                otherPlayerList.Add(player4);

            return otherPlayerList;
        }

        private void CheckPlayerElimination()
        {
            if (player1.Active && player1.Hp <= 0)
                player1.Active = false;
            if (player2.Active && player2.Hp <= 0)
                player2.Active = false;
            if (player3.Active && player3.Hp <= 0)
                player3.Active = false;
            if (player4.Active && player4.Hp <= 0)
                player4.Active = false;
        }

        private bool CheckEndGame()
        {
            if (!player1.Active && !player2.Active && !player3.Active ||
                !player1.Active && !player2.Active && !player4.Active ||
                !player1.Active && !player3.Active && !player4.Active ||
                !player2.Active && !player3.Active && !player4.Active)
                return true;
            else
                return false;
        }

        private Player GetWinningPlayer()
        {
            if (player1.Active)
                return player1;
            else if (player2.Active)
                return player2;
            else if (player3.Active)
                return player3;
            else return player4;
        }

        // Retourne le prochain joueur (ex.: joueur1 -> joueur2)
        private Player NextPlayer()
        {
            if (currentPlayer == player1)
            {
                if (player2.Active)
                    return player2;
                else if (player3.Active)
                    return player3;
                else return player4;
            }
            else if (currentPlayer == player2)
            {
                if (player3.Active)
                    return player3;
                else if (player4.Active)
                    return player4;
                else return player1;
            }
            else if (currentPlayer == player3)
            {
                if (player4.Active)
                    return player4;
                else if (player1.Active)
                    return player1;
                else return player2;
            }
            else if (currentPlayer == player4)
            {
                if (player1.Active)
                    return player1;
                else if (player2.Active)
                    return player2;
                else return player3;
            }
            return player1;
        }

        // Configure les etats graphiques de la platforme
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

        // Dessine les models
        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
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