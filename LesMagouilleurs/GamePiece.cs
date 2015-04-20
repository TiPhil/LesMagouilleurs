using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    public class GamePiece
    {
        // Positions X
        private const float POS_X1_L = -2.8f;
        private const float POS_X1_R = -2f;
        private const float POS_X2_L = -1.066f;
        private const float POS_X2_R = -0.266f;
        private const float POS_X3_L = 0.667f;
        private const float POS_X3_R = 1.467f;
        private const float POS_X4_L = 2.4f;
        private const float POS_X4_R = 3.2f;

        // Positions Z
        private const float POS_Z1_L = -3.8f;
        private const float POS_Z1_R = -3f;
        private const float POS_Z2_L = -2.066f;
        private const float POS_Z2_R = -1.266f;
        private const float POS_Z3_L = -0.333f;
        private const float POS_Z3_R = 0.467f;
        private const float POS_Z4_L = 1.4f;
        private const float POS_Z4_R = 2.2f;

        // Position Y
        private const float POS_Y = 0.3f;

        // Board squares
        //private const Vector3 SQUARE_1_P1 = new Vector3()



        private int squarePosition; // 1 to 12
        private Matrix world;
        private Model model;
        private float x;
        private float y;
        private float z;

        public int Position
        {
            get
            {
                return squarePosition;
            }
            set
            {
                squarePosition = value;
            }
        }
        public Matrix World
        {
            get
            {
                return world;
            }
            set
            {
                world = value;
            }
        }
        public Model Model
        {
            get
            {
                return model;
            }
        }
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        public GamePiece(PlayerNumber playerNumber)
        {
            squarePosition = 1; // starting position for all players

            switch (playerNumber)
            {
                case PlayerNumber.P1:
                    model = Ressources.Instance.GamePieceP1;
                    x = 2.4f;
                    y = 0.3f;
                    z = 1.4f;
                    break;

                case PlayerNumber.P2:
                    model = Ressources.Instance.GamePieceP2;
                    x = 3.2f;
                    y = 0.3f;
                    z = 1.4f;
                    break;

                case PlayerNumber.P3:
                    model = Ressources.Instance.GamePieceP3;
                    x = 2.4f;
                    y = 0.3f;
                    z = 2.2f;
                    break;

                case PlayerNumber.P4:
                    model = Ressources.Instance.GamePieceP4;
                    x = 3.2f;
                    y = 0.3f;
                    z = 2.2f;
                    break;

                default: ;
                    model = Ressources.Instance.GamePieceP1;
                    x = 2.4f;
                    y = 0.3f;
                    z = 1.4f;
                    break;
            }
            world = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(new Vector3(x, y, z));
        }
    }
}
