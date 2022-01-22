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
        private string playerName;
        
        public Scene1(ContentManager content, GraphicsDevice gd) : base(content, gd)
        {
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/Scene1/bg"));
            bg.LoadContent(0, 0);
        }

        public void LoadContent(string name)
        {
            base.LoadContent();

            fullText = new string[2];
            fullText[0] = $" Hello, {name}, where is your PASS3?";
            fullText[1] = " You think you can get away with not doing it????\nYou will not get away with this!";

        }
    }
}
