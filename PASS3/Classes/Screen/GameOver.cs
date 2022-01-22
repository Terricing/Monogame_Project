// Author: Eilay Katsnelson
// File Name: GameOver.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Game Over menu screen

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
using Animation2D;

namespace PASS3.Classes.Screen
{

    class GameOver
    {
        // create gamestate
        public const int GAMESTATE = 3;

        // Store bacground and button
        private Img bg;
        private Button gameOverBtn;

        // Store scores
        private ScoreKeeper scores;

        /// <summary>
        /// Create object
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="scores">control scores</param>
        public GameOver(ContentManager content, ScoreKeeper scores)
        {
            this.scores = scores;

            // Load background
            bg = new Img(content.Load<Texture2D>("Screens/GameOver/Bg"));
            bg.LoadContent(0,0);

            // Load button
            gameOverBtn = new Button(content, "Screens/GameOver/TryAgainBt/tryAgainBt", "Screens/GameOver/TryAgainBt/htTryAgainBt", 0, (int)(Globals.GAME_HEIGHT / 1.5f));
            gameOverBtn.X = Globals.GAME_WIDTH / 2 - gameOverBtn.BtRect.Width / 2;
        }

        /// <summary>
        /// Update screen
        /// </summary>
        /// <param name="mouseState">Keep track of pressed mousebuttons</param>
        public void Update(MouseState mouseState)
        { 
            // check for left button press
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // if button is pressed, check if it collides with the button
                if (gameOverBtn.CheckCollision(mouseState.Position))
                {
                    // if button is pressed, update scores, then go back to menu
                    scores.AddScore(Globals.PlayerName, Globals.Score);
                    scores.PerformWrite();
                    Globals.GameState = Menu.GAMESTATE;
                }
            }

            // update button
            gameOverBtn.Update(Mouse.GetState());
        }

        /// <summary>
        /// Draw game over screen
        /// </summary>
        /// <param name="spriteBatch">control screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Begin();

            // Draw background and button
            bg.Draw(spriteBatch);
            gameOverBtn.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
