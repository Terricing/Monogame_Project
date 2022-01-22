// Author: Eilay Katsnelson
// File Name: Level.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A parent class for every level

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
using PASS3.Classes.Screen;
using MonoGame.Extended;
using Animation2D;
using PASS3.Classes.Components;

namespace PASS3.Classes.Levels
{
    class Level
    {
        // Store images for scrolling background
        protected Img[] bg = new Img[2];

        // Store positions for backgrounds
        protected float[] bgPos = new float[2];
        // Store scrolling speed
        protected float bgSpeed;

        // store whether the level is over
        protected bool isOver;

        // Store player object
        protected Player player;

        // keep track of level state
        protected bool isLevelFinished;

        /// <summary>
        /// Create a level with a custom background
        /// </summary>
        /// <param name="bgPath">background's location</param>
        /// <param name="content">load content</param>
        /// <param name="lifeManager">lifeManager representing number of available lives</param>
        public Level(string bgPath, ContentManager content, LifeManager lifeManager)
        {
            // Create image objects for scrolling screen
            bg[0] = new Img(content.Load<Texture2D>(bgPath));
            bg[1] = new Img(bg[0].Image);

            // Load background (create surrounding rectangles)
            bg[0].LoadContent( 0, 0);
            bg[1].LoadContent(0, Globals.GAME_HEIGHT);

            // Create player object and surrounding rectangle
            player = new Player(lifeManager);
            player.LoadContent(content);
        }

        /// <summary>
        /// Reset values back to initial values
        /// </summary>
        public virtual void LoadContent()
        {
            bgPos[0] = bg[0].Y;
            bgPos[1] = bg[1].Y;
            isLevelFinished = false;
        }

        /// <summary>
        /// Update level
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        /// <param name="kb">keep track of pressed keys</param>
        public virtual void Update(GameTime gameTime, KeyboardState kb)
        {
            // Screen scrolling
            BgScroll(gameTime);

            // update player
            player.Update(gameTime, kb);
        }

        /// <summary>
        /// Draw level
        /// </summary>
        /// <param name="spriteBatch">controls screen's canvas</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw backgrounds and player
            foreach (Img img in bg) { img.Draw(spriteBatch); }
            player.Draw(spriteBatch);
        }

        /// <summary>
        /// Controls scrolling background
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        public void BgScroll(GameTime gameTime)
        {
            // if background goes off screen, reset location
            if (!isLevelFinished)
            {
                if (bgPos[0] >= Globals.GAME_HEIGHT)
                {
                    bgPos[0] += -2 * Globals.GAME_HEIGHT;
                }
                else if (bgPos[1] >= Globals.GAME_HEIGHT)
                {
                    bgPos[1] += -2 * Globals.GAME_HEIGHT;
                }
            }

            // move backgrounds
            bgSpeed = -1 * Globals.BgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bgPos[0] -= bgSpeed;
            bgPos[1] -= bgSpeed;
            bg[0].Y = (int)bgPos[0];
            bg[1].Y = (int)bgPos[1];
        }

        /// <summary>
        /// Property for whether level is over
        /// </summary>
        public bool IsLevelFinished
        {
            get { return isLevelFinished; }
            set { isLevelFinished = value; }
        }
    }
}
