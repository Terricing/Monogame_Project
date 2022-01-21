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
        public const int SCENESTATE = 2;
        
        public Scene1(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            //bg = new Img(content.Load<Texture2D>(""));
            //bg.LoadContent(0, 0);

            fullText = new string[] { "Hello, this is a long test test test test test test test \n test long test test test test test test \nteslong test test test test test test teslong test t\nest test test test test tes" };
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            base.Draw(spriteBatch);
            spriteBatch.End();
        }


    }
}
