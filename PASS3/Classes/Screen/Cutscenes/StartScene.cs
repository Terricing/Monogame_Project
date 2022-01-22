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
        public const int GAMESTATE = 5;

        // store background
        private Img bg;

        // Store allowed values
        protected string allowedChars = "abcdefghijklmnopqrstuvwxyz";

        // Store pressed keys
        protected Keys[] prevKeys;
        protected Keys[] keys;

        // Store font for the name
        protected SpriteFont nameFont;

        // Store entered name
        protected string name = "";

        // Store mouse state
        private MouseState mouse;

        // Store continue button
        private Button continueButton;

        // store state
        private bool isOver;

        public StartScene(ContentManager content)
        {
            // Create and load background
            bg = new Img(content.Load<Texture2D>("Screens/Cutscenes/StartScene/bg"));
            bg.LoadContent(0, 0);

            // create name font
            nameFont = content.Load<SpriteFont>("Fonts/nameFont");

            // create button
            continueButton = new Button(content, "Screens/Cutscenes/StartScene/btn", "Screens/Cutscenes/StartScene/hBtn", 0, (int)(Globals.GAME_HEIGHT / 1.3));
            continueButton.X = Globals.GAME_WIDTH / 2 - continueButton.BtRect.Width / 2;

            // Give initial value of false
            isOver = false;

        }

        /// <summary>
        /// Reset State of start scene
        /// </summary>
        public void LoadContent()
        {
            // Reset state to be reused
            isOver = false;
            name = "";
        }

        /// <summary>
        /// Update start scene
        /// </summary>
        public void Update()
        {
            // get pressed keys
            prevKeys = keys;
            keys = Keyboard.GetState().GetPressedKeys();
            mouse = Mouse.GetState();

            // Manage user input
            if (keys.Length > 0 && (prevKeys.Length == 0 || keys[0] != prevKeys[0]))
            {  
                if (allowedChars.Contains(keys[0].ToString().ToLower()))
                {
                    // If input is part of allowed input, add it to the name string
                    name += keys[0];
                }
                else if (keys[0] == Keys.Space)
                {
                    // If space is pressed, add space to name string
                    name = name.PadRight(name.Length + 1);
                }
                else if (keys[0] == Keys.Back && name.Length > 0)
                {
                    // if backspace is pressed, remove the last char of name string
                    name = name.Remove(name.Length - 1);
                }
            }

            // on left button press, check if continue button is pressed
            if (mouse.LeftButton == ButtonState.Pressed && continueButton.CheckCollision(mouse.Position))
            {
                // if name input is long enough, proceed to cutscene
                if (name.Length > 0)
                {
                    Globals.PlayerName = name;
                    isOver = true;
                }

            }
            
            // update button
            continueButton.Update(Mouse.GetState());
        } 

        /// <summary>
        /// Draw start scene's assets
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // draw background
            bg.Draw(spriteBatch);

            // draw name string
            spriteBatch.DrawString(nameFont, name, new Vector2(Globals.GAME_WIDTH / 2 - nameFont.MeasureString(name).X / 2, Globals.GAME_HEIGHT / 2), Color.White);

            // draw continue button
            continueButton.Draw(spriteBatch);
            spriteBatch.End();
        }

        // property for name
        public string GetName()
        {
            return name;
        }

        // property is isOver bool
        public bool IsOver
        {
            get { return isOver; }
        }
    }
}
