using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class StartScene
    {
        public const int SCENESTATE = 1;

        // store background
        private Img bg;

        // create typing functionality
        protected string allowedChars = "abcdefghijklmnopqrstuvwxyz";
        protected KeyboardState kb;
        protected KeyboardState prevKb;
        protected Keys[] prevKeys;
        protected Keys[] keys;
        protected SpriteFont nameFont;
        protected string name = "";

        // Store continue button
        private Button continueButton;

        public StartScene(ContentManager content)
        {
            bg = new Img(content.Load<Texture2D>(""));
            bg.LoadContent(0, 0);

            // create name font
            nameFont = content.Load<SpriteFont>("Fonts/nameFont");

            // create button
            continueButton = new Button(content, "Screens/Cutscenes/StartScene/btn", "Screens/Cutscenes/StartScene/hBtn", 0, (int)(Globals.GAME_HEIGHT / 1.3));
            continueButton.X = Globals.GAME_WIDTH / 2 - continueButton.BtRect.Width / 2;

        }

        public void Update ()
        {
            // get pressed keys
            prevKeys = keys;
            keys = Keyboard.GetState().GetPressedKeys();

            // Manage user input
            if (keys[0] != prevKeys[0])
            {
                if (allowedChars.Contains(keys[0].ToString().ToLower()))
                {
                    name += keys[0];
                }
                else if (keys[0] == Keys.Space)
                {
                    name = name.PadRight(name.Length + 1);
                }
                else if (keys[0] == Keys.Back)
                {
                    name = name.Remove(name.Length - 2);
                }
            }


        } 

        public void Draw(SpriteBatch spriteBatch)
        {
            bg.Draw(spriteBatch);
            spriteBatch.DrawString(nameFont, name, new Vector2(Globals.GAME_WIDTH / 2 - nameFont.MeasureString(name).X / 2, Globals.GAME_HEIGHT / 2), Color.White);
            continueButton.Draw(spriteBatch);
        }

    }
}
