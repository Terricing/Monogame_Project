// Author: Eilay Katsnelson
// File Name: RightHand.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for boss's right hand

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Components.Boss
{
    class RightHand : Hand
    {
        /// <summary>
        /// Create right hand
        /// </summary>
        /// <param name="content">load content</param>
        public RightHand(ContentManager content) : base()
        {
            // Set initial value
            startPos = Globals.GAME_WIDTH;

            // Load texture and create rectangle
            hand = new Img(content.Load<Texture2D>("Screens/Game/Stage3/rHand"));
            hand.LoadContent((int)startPos, 0);
        }
    }
}
