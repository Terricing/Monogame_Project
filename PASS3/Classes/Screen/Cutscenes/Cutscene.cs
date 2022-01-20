using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3
{
    class Cutscene
    {
        protected Img bg;
        
        public Cutscene()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            bg.Draw(spriteBatch);
        }

    }
}
