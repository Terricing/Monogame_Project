// Author: Eilay Katsnelson
// File Name: Scene4.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for the fourth cutscene

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class Scene4 : Cutscene
    {
        // Set cutscene
        public const int SCENESTATE = 5;

        /// <summary>
        /// Create fourth scene
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public Scene4(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            // Load background art
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene4/bg"));
            bg.LoadContent(0, 0);

            // Create text to display
            fullText = new string[2];
            fullText[0] = " You have won this time!\nBut it is not over yet!";
            fullText[1] = " Let's see how long you can last...";
        }

    }
}
