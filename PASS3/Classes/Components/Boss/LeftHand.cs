// Author: Eilay Katsnelson
// File Name: LeftHand.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child class for boss's left hand

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Components.Boss
{
    class LeftHand : Hand
    {
        /// <summary>
        /// Create left hand
        /// </summary>
        /// <param name="content">load content</param>
        public LeftHand (ContentManager content) : base()
        {
            // set initial values
            startPos = -Globals.GAME_WIDTH;

            // load hand and create rectangle
            hand = new Img(content.Load<Texture2D>("Screens/Game/Stage3/lHand"));
            hand.LoadContent((int)startPos, 0);
        }
    }
}
