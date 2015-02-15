﻿using System;
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
    class Ressources
    {
        // Variables
        private ContentManager Content;
        private GameServiceContainer Services;

        private Texture2D rulesPanel;
        private SoundEffect buttonClicked;


        // Properties
        public Texture2D RulesPanel { get { return rulesPanel; } }
        public SoundEffect ButtonClicked { get { return buttonClicked; } }



        public Ressources(ContentManager Content, GameServiceContainer Services)
        {
            this.Content = Content;
            this.Services = Services;
        }

        public void Load()
        {
            Content = new ContentManager(Services, "Content");

            rulesPanel = Content.Load<Texture2D>("rulesPanel");
            buttonClicked = Content.Load<SoundEffect>("buttonSound");
        }
    }
}
