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

        private float compteurFacteur = 1.76f;
        private enum GoingDirection
        {
            LEFT, TOP, RIGHT, BOTTOM
        }
        //private GoingDirection goingDirection = GoingDirection.LEFT; TO DELETE

        // Positions X
        private const float POS_X1_L = -3.15f;
        private const float POS_X1_R = -2.35f;
        private const float POS_X2_L = -1.3f;
        private const float POS_X2_R = -0.5f;
        private const float POS_X3_L = 0.55f;
        private const float POS_X3_R = 1.35f;
        private const float POS_X4_L = 2.4f;
        private const float POS_X4_R = 3.2f;

        // Positions Z
        private const float POS_Z1_T = -4.15f;
        private const float POS_Z1_B = -3.35f;
        private const float POS_Z2_T = -2.3f;
        private const float POS_Z2_B = -1.5f;
        private const float POS_Z3_T = -0.45f;
        private const float POS_Z3_B = 0.35f;
        private const float POS_Z4_T = 1.4f;
        private const float POS_Z4_B = 2.2f;

        // Position Y
        private const float POS_Y = 0.3f;

        // Board squares
        /* TO DELETE
        private static Vector3 SQUARE_1_P1 = new Vector3(POS_X4_L, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_1_P2 = new Vector3(POS_X4_R, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_1_P3 = new Vector3(POS_X4_L, POS_Y, POS_Z4_B);
        private static Vector3 SQUARE_1_P4 = new Vector3(POS_X4_R, POS_Y, POS_Z4_B);

        private static Vector3 SQUARE_2_P1 = new Vector3(POS_X3_L, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_2_P2 = new Vector3(POS_X3_R, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_2_P3 = new Vector3(POS_X3_L, POS_Y, POS_Z4_B);
        private static Vector3 SQUARE_2_P4 = new Vector3(POS_X3_R, POS_Y, POS_Z4_B);

        private static Vector3 SQUARE_3_P1 = new Vector3(POS_X2_L, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_3_P2 = new Vector3(POS_X2_R, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_3_P3 = new Vector3(POS_X2_L, POS_Y, POS_Z4_B);
        private static Vector3 SQUARE_3_P4 = new Vector3(POS_X2_R, POS_Y, POS_Z4_B);

        private static Vector3 SQUARE_4_P1 = new Vector3(POS_X1_L, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_4_P2 = new Vector3(POS_X1_R, POS_Y, POS_Z4_T);
        private static Vector3 SQUARE_4_P3 = new Vector3(POS_X1_L, POS_Y, POS_Z4_B);
        private static Vector3 SQUARE_4_P4 = new Vector3(POS_X1_R, POS_Y, POS_Z4_B);

        private static Vector3 SQUARE_5_P1 = new Vector3(POS_X1_L, POS_Y, POS_Z3_T);
        private static Vector3 SQUARE_5_P2 = new Vector3(POS_X1_R, POS_Y, POS_Z3_T);
        private static Vector3 SQUARE_5_P3 = new Vector3(POS_X1_L, POS_Y, POS_Z3_B);
        private static Vector3 SQUARE_5_P4 = new Vector3(POS_X1_R, POS_Y, POS_Z3_B);

        private static Vector3 SQUARE_6_P1 = new Vector3(POS_X1_L, POS_Y, POS_Z2_T);
        private static Vector3 SQUARE_6_P2 = new Vector3(POS_X1_R, POS_Y, POS_Z2_T);
        private static Vector3 SQUARE_6_P3 = new Vector3(POS_X1_L, POS_Y, POS_Z2_B);
        private static Vector3 SQUARE_6_P4 = new Vector3(POS_X1_R, POS_Y, POS_Z2_B);

        private static Vector3 SQUARE_7_P1 = new Vector3(POS_X1_L, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_7_P2 = new Vector3(POS_X1_R, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_7_P3 = new Vector3(POS_X1_L, POS_Y, POS_Z1_B);
        private static Vector3 SQUARE_7_P4 = new Vector3(POS_X1_R, POS_Y, POS_Z1_B);

        private static Vector3 SQUARE_8_P1 = new Vector3(POS_X2_L, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_8_P2 = new Vector3(POS_X2_R, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_8_P3 = new Vector3(POS_X2_L, POS_Y, POS_Z1_B);
        private static Vector3 SQUARE_8_P4 = new Vector3(POS_X2_R, POS_Y, POS_Z1_B);

        private static Vector3 SQUARE_9_P1 = new Vector3(POS_X3_L, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_9_P2 = new Vector3(POS_X3_R, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_9_P3 = new Vector3(POS_X3_L, POS_Y, POS_Z1_B);
        private static Vector3 SQUARE_9_P4 = new Vector3(POS_X3_R, POS_Y, POS_Z1_B);

        private static Vector3 SQUARE_10_P1 = new Vector3(POS_X4_L, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_10_P2 = new Vector3(POS_X4_R, POS_Y, POS_Z1_T);
        private static Vector3 SQUARE_10_P3 = new Vector3(POS_X4_L, POS_Y, POS_Z1_B);
        private static Vector3 SQUARE_10_P4 = new Vector3(POS_X4_R, POS_Y, POS_Z1_B);

        private static Vector3 SQUARE_11_P1 = new Vector3(POS_X4_L, POS_Y, POS_Z2_T);
        private static Vector3 SQUARE_11_P2 = new Vector3(POS_X4_R, POS_Y, POS_Z2_T);
        private static Vector3 SQUARE_11_P3 = new Vector3(POS_X4_L, POS_Y, POS_Z2_B);
        private static Vector3 SQUARE_11_P4 = new Vector3(POS_X4_R, POS_Y, POS_Z2_B);

        private static Vector3 SQUARE_12_P1 = new Vector3(POS_X4_L, POS_Y, POS_Z3_T);
        private static Vector3 SQUARE_12_P2 = new Vector3(POS_X4_R, POS_Y, POS_Z3_T);
        private static Vector3 SQUARE_12_P3 = new Vector3(POS_X4_L, POS_Y, POS_Z3_B);
        private static Vector3 SQUARE_12_P4 = new Vector3(POS_X4_R, POS_Y, POS_Z3_B);
        */
        // Class attributes
        private PlayerNumber playerNumber;
        private int position; // 1 to 12
        private Matrix world;
        private Model model;
        private float x;
        private float y;
        private float z;
        private float compteur = 0;

        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
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
            this.playerNumber = playerNumber;
            position = 1; // starting position for all players

            switch (playerNumber)
            {
                case PlayerNumber.P1:
                    model = Ressources.Instance.GamePieceP1;
                    x = POS_X4_L;
                    y = POS_Y;
                    z = POS_Z4_T;
                    break;

                case PlayerNumber.P2:
                    model = Ressources.Instance.GamePieceP2;
                    x = POS_X4_R;
                    y = POS_Y;
                    z = POS_Z4_T;
                    break;

                case PlayerNumber.P3:
                    model = Ressources.Instance.GamePieceP3;
                    x = POS_X4_L;
                    y = POS_Y;
                    z = POS_Z4_B;
                    break;

                case PlayerNumber.P4:
                    model = Ressources.Instance.GamePieceP4;
                    x = POS_X4_R;
                    y = POS_Y;
                    z = POS_Z4_B;
                    break;

                default: ;
                    model = Ressources.Instance.GamePieceP1;
                    x = POS_X4_L;
                    y = POS_Y;
                    z = POS_Z4_T;
                    break;
            }
            world = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(new Vector3(x, y, z));
        }

        public bool Move(int positionToMove)
        {
            Vector3 coord = new Vector3(x, y, z);
            switch (playerNumber)
            {
                case PlayerNumber.P1:

                    switch (position)
                    {
                        case 1:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X3_L);
                            break;
                        case 2:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X2_L);
                            break;
                        case 3:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X1_L);
                            break;
                        case 4:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z3_T);
                            break;
                        case 5:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z2_T);
                            break;
                        case 6:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z1_T);
                            break;
                        case 7:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X2_L);
                            break;
                        case 8:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X3_L);
                            break;
                        case 9:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X4_L);
                            break;
                        case 10:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z2_T);
                            break;
                        case 11:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z3_T);
                            break;
                        case 12:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z4_T);
                            break;
                        default: ;
                            break;
                    }
                    break;
                case PlayerNumber.P2:

                    switch (position)
                    {
                        case 1:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X3_R);
                            break;
                        case 2:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X2_R);
                            break;
                        case 3:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X1_R);
                            break;
                        case 4:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z3_T);
                            break;
                        case 5:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z2_T);
                            break;
                        case 6:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z1_T);
                            break;
                        case 7:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X2_R);
                            break;
                        case 8:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X3_R);
                            break;
                        case 9:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X4_R);
                            break;
                        case 10:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z2_T);
                            break;
                        case 11:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z3_T);
                            break;
                        case 12:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z4_T);
                            break;
                        default: ;
                            break;
                    }
                    break;
                case PlayerNumber.P3:

                    switch (position)
                    {
                        case 1:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X3_L);
                            break;
                        case 2:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X2_L);
                            break;
                        case 3:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X1_L);
                            break;
                        case 4:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z3_B);
                            break;
                        case 5:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z2_B);
                            break;
                        case 6:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z1_B);
                            break;
                        case 7:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X2_L);
                            break;
                        case 8:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X3_L);
                            break;
                        case 9:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X4_L);
                            break;
                        case 10:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z2_B);
                            break;
                        case 11:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z3_B);
                            break;
                        case 12:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z4_B);
                            break;
                        default: ;
                            break;
                    }
                    break;
                case PlayerNumber.P4:

                    switch (position)
                    {
                        case 1:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X3_R);
                            break;
                        case 2:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X2_R);
                            break;
                        case 3:
                            MoveCoord(coord, GoingDirection.LEFT, POS_X1_R);
                            break;
                        case 4:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z3_B);
                            break;
                        case 5:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z2_B);
                            break;
                        case 6:
                            MoveCoord(coord, GoingDirection.TOP, POS_Z1_B);
                            break;
                        case 7:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X2_R);
                            break;
                        case 8:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X3_R);
                            break;
                        case 9:
                            MoveCoord(coord, GoingDirection.RIGHT, POS_X4_R);
                            break;
                        case 10:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z2_B);
                            break;
                        case 11:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z3_B);
                            break;
                        case 12:
                            MoveCoord(coord, GoingDirection.BOTTOM, POS_Z4_B);
                            break;
                        default: ;
                            break;
                    }
                    break;
                default: ;
                    break;
            }


            if (position == positionToMove)
            {
                return true;
            }
            else
            {
                world = Matrix.CreateScale(0.5f) * Matrix.CreateTranslation(coord);
            }

            return false;
        }

        private void MoveCoord(Vector3 coord, GoingDirection goingDirection, float POS)
        {
            compteur += 0.05f;
            switch (goingDirection)
            {
                case GoingDirection.LEFT: ;
                    x -= 0.05f;
                    y = (float)Math.Sin(compteur * compteurFacteur) + POS_Y;
                    if (x <= POS)
                    {
                        compteur = 0;
                        position = GetNextPosition(1);
                    }
                    break;
                case GoingDirection.TOP: ;
                    z -= 0.05f;
                    y = (float)Math.Sin(compteur * compteurFacteur) + POS_Y;
                    if (z <= POS)
                    {
                        compteur = 0;
                        position = GetNextPosition(1);
                    }
                    break;
                case GoingDirection.RIGHT: ;
                    x += 0.05f;
                    y = (float)Math.Sin(compteur * compteurFacteur) + POS_Y;
                    if (x >= POS)
                    {
                        compteur = 0;
                        position = GetNextPosition(1);
                    }
                    break;
                case GoingDirection.BOTTOM: ;
                    z += 0.05f;
                    y = (float)Math.Sin(compteur * compteurFacteur) + POS_Y;
                    if (z >= POS)
                    {
                        compteur = 0;
                        position = GetNextPosition(1);
                    }
                    break;
                default:
                    x -= 0.05f;
                    y = (float)Math.Sin(compteur * compteurFacteur) + POS_Y;
                    if (x <= POS)
                    {
                        compteur = 0;
                        position = GetNextPosition(1);
                    }
                    break;
            }

        }

        public int GetNextPosition(int diceResults)
        {
            int newPosition = 0;
            if (diceResults + position > 12)
            {
                newPosition = diceResults + position - 12;
            }
            else
            {
                newPosition = diceResults + position;
            }

            return newPosition;
        }
    }
}
