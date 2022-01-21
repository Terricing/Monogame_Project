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
        public LeftHand (ContentManager content) : base()
        {
            // set values
            startPos = -Globals.GAME_WIDTH;

            hand = new Img(content.Load<Texture2D>("Screens/Game/Stage3/lHand"));
            hand.LoadContent((int)startPos, 0);
        }

        


    }
}
