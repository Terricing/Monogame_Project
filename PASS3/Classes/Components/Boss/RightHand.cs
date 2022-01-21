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
        public RightHand(ContentManager content) : base()
        {
            startPos = Globals.GAME_WIDTH;
            hand = new Img(content.Load<Texture2D>("Screens/Game/Stage3/rHand"));
            hand.LoadContent((int)startPos, 0);
        }
    }
}
