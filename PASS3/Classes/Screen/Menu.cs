// Author: Eilay Katsnelson
// File Name: Menu.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Class for the menu screen, the first screen that the user sees

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using Animation2D;
using PASS3.Classes;
using PASS3.Classes.Screen.Cutscenes;
using PASS3.Classes.Screen;

namespace PASS3
{
  
    class Menu
    {
        // create gamestate
        public const int GAMESTATE = 0;

        // Store buttons
        private Button playBtn;
        private Button scoreBtn;
        private Button exitBtn;

        // store bacjground
        private Img bg;

        // store mouse states
        private MouseState mouseState;
        private MouseState prevMouseState;

        // game container, used for exit functionality
        private Game1 game;

        /// <summary>
        /// Create menu
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="game"></param>
        public Menu (ContentManager content, Game1 game)
        {
            this.game = game;

            // Load background
            bg = new Img(content.Load<Texture2D>("Screens/Menu/bg"));
            bg.LoadContent(0, 0);

            // Load buttons
            playBtn = new Button(content, "Screens/Menu/playBtn/btn", "Screens/Menu/playBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.8f));
            playBtn.X -= playBtn.BtRect.Width / 2;

            scoreBtn = new Button(content, "Screens/Menu/scoreBtn/btn", "Screens/Menu/scoreBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.55f) );
            scoreBtn.X -= scoreBtn.BtRect.Width / 2;

            exitBtn = new Button(content, "Screens/Menu/exitBtn/btn", "Screens/Menu/exitBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.33f)); 
            exitBtn.X -= exitBtn.BtRect.Width / 2;
        }

        /// <summary>
        /// Update menu
        /// </summary>
        public void Update()
        {
            // obtain mouse state
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            // update buttons
            playBtn.Update(mouseState);
            scoreBtn.Update(mouseState);
            exitBtn.Update(mouseState);

            // if mouse is pressed
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed)
            {
                // Perform action based on if and which button is pressed
                if (playBtn.CheckCollision(mouseState.Position))
                {
                    Globals.GameState = StartScene.GAMESTATE;
                }
                else if (scoreBtn.CheckCollision(mouseState.Position))
                {
                    Globals.GameState = ScoreBoard.GAMESTATE;
                }
                else if (exitBtn.CheckCollision(mouseState.Position))
                {
                    game.Exit();
                }
            }
        }

        /// <summary>
        /// Draw menu
        /// </summary>
        /// <param name="spriteBatch">controller for screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // draw background
            bg.Draw(spriteBatch);

            // draw buttons
            playBtn.Draw(spriteBatch);
            scoreBtn.Draw(spriteBatch);
            exitBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
