// Author: Eilay Katsnelson
// File Name: Scene.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for the second cutscene

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class Scene2 : Cutscene
    {
        // Set scenestate
        public const int SCENESTATE = 3;

        /// <summary>
        /// Create second scene
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public Scene2(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            // load backround art
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene2/bg"));
            bg.LoadContent(0, 0);

            // Create text to display
            fullText = new string[2];
            fullText[0] = " You managed to escape the school!";
            fullText[1] = " I cannot chase you anymore, but I assure you this is not the end...";
        }

    }
}
