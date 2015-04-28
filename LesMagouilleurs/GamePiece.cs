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

            // Positionne le model de la piece du joueur en fonction du numero de joueur (1,2,3 ou 4)
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

        // Bouge la piece d'un joueur
        public bool Move(int positionToMove)
        {
            Vector3 coord = new Vector3(x, y, z);
            
            // En fonctione du numero de joueur, verifier sa position de case et
            // le faire bouger dans la bonne direction
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

        // Methode permettant de faire le mouvement de la piece d'un joueur
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

        // Suite a un brasser de dee, retourne la position final de la piece d'un joueur
        // Ex.: Case 11 + un brasse de dee de 2 = Case 1
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
