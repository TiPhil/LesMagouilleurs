using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    // Permet le control de la camera
    class CameraController
    {
        public static Matrix ControlCamera(Matrix view)
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

            return view;
        }
    }
}
