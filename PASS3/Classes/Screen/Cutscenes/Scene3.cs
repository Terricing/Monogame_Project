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
        public const int SCENESTATE = 4;

        public Scene3(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene3/bg"));
            bg.LoadContent(0, 0);

            fullText = new string[3];
            fullText[0] = " Welcome to my realm!";
            fullText[1] = " You cannot escape me here!";
            fullText[2] = " Mwahahahaha!";
        }

    }
}
