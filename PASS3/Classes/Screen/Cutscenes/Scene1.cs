// Author: Eilay Katsnelson
// File Name: Scene1.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for the first cutscene

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class Scene1 : Cutscene
    {
        // Set scenestate
        public const int SCENESTATE = 2;
        
        /// <summary>
        /// Create first scene
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public Scene1(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            // load background art
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene1/bg"));
            bg.LoadContent(0, 0);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            // Create text to display
            fullText = new string[2];
            fullText[0] = $" Hello, {Globals.PlayerName}, where is your PASS3?";
            fullText[1] = " You think you can get away with not doing it????\nYou will not get away with this!";
        }
    }
}
