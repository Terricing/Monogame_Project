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
        public const int SCENESTATE = 5;

        public Scene4(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene4/bg"));
            bg.LoadContent(0, 0);

            fullText = new string[2];
            fullText[0] = " It is not over yet!";
            fullText[1] = " You will never get away with this!";
        }

    }
}
