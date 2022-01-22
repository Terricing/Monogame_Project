// Author: Eilay Katsnelson
// File Name: Scene3.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for the third cutscene

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class Scene3 : Scene1
    {
        // set scenestate
        public const int SCENESTATE = 4;

        /// <summary>
        /// Create first scene
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public Scene3(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            // Load background
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene3/bg"));
            bg.LoadContent(0, 0);

            // Create text to display
            fullText = new string[3];
            fullText[0] = " Welcome to my realm!";
            fullText[1] = " You cannot escape me here!";
            fullText[2] = " Mwahahahaha!";
        }
    }
}
