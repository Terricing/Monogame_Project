// Author: Eilay Katsnelson
// File Name: LifeManager.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Class for managing leftover lives

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
using Helper;
using PASS3.Classes.Screen;

namespace PASS3.Classes.Components
{
    class LifeManager
    {
        // store number of lives
        private int numLives;

        // store data relevant to heart's image
        private Texture2D heart;
        private Img[] hearts = new Img[3];

        // store booleans representing active / available lives
        private bool[] heartsActive = new bool[3];

        // store time to maintain invincibility
        private int lifeTimer;

        // store how long invincibility has been available for
        private Timer invincibilityTimer;

        /// <summary>
        /// Create LifeManager object
        /// </summary>
        /// <param name="content"></param>
        public LifeManager(ContentManager content)
        {
            // number of available lives
            numLives = 3;

            // load heart image
            heart = content.Load<Texture2D>("Screens/Game/heart");

            // store time-related info
            lifeTimer = 1500;
            invincibilityTimer = new Timer(lifeTimer, false);
            
            // scale hearts
            float heartScale = 0.2f;

            // create heart image objects
            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = new Img(heart);
                hearts[i].LoadContent(Globals.GAME_WIDTH - (int)(heart.Width * heartScale), (Globals.GAME_HEIGHT / 10) * i, heartScale);
                heartsActive[i] = true;
            }
        }

        /// <summary>
        /// Update life manager
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            invincibilityTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        /// <summary>
        /// Draw hearts
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw active hearts
            for (int i = 0; i < hearts.Length; i++)
            {
                if (heartsActive[i])
                {
                    hearts[i].Draw(spriteBatch);
                }
            }
        }


        /// <summary>
        /// Lose a heart and start invincibility timer
        /// </summary>
        public void LoseLife()
        {
            // loop through hearts, deactivate next heart
            for (int i = heartsActive.Length - 1; i >= 0; i--)
            {
                if (heartsActive[i])
                {
                    heartsActive[i] = false;
                    numLives--;
                    break;
                }
            }

            // end game if no more lives
            if (numLives == 0)
            {
                Globals.GameState = GameOver.GAMESTATE;
            }
            else
            {
                // if lives are still available, start the invincibility timer
                invincibilityTimer.ResetTimer(true);
            }
        }

        /// <summary>
        /// Property for invincibility timer
        /// </summary>
        public Timer InvincibilityTimer
        {
            get { return invincibilityTimer; }
        }

        /// <summary>
        /// Property for number of available lives
        /// </summary>
        public int Lives
        {
            get { return numLives; }
            set { numLives = value; }
        }

    }
}
