// Author: Eilay Katsnelson
// File Name: Cutscene.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A parent class for creating cutscenes

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PASS3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3
{
    class Cutscene
    {
        // store cutscene image
        protected Img bg;

        // store which index of the text array is on
        protected int textIteration;

        // Store which index of the current text is on
        protected int iteration;
        
        // store full text and drawn text
        protected string [] fullText;
        protected string shownText;
        protected bool isGoing;
        
        // store textfont for displaying text
        protected SpriteFont textFont;

        // store time
        protected float timeIncrement;

        // maintain state
        protected bool isOver = false;

        // Get input
        private KeyboardState kb;
        private KeyboardState prevKb;
        
        // create rectangle to display text on
        private Rectangle primitiveRect;
        private Helper.GameRectangle rect;
        
        /// <summary>
        /// Create cutscene
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public Cutscene(ContentManager content, GraphicsDevice gd)
        {
            // Load textfont for displayed text
            textFont = content.Load<SpriteFont>("Fonts/cutsceneText");

            // Create rectangle to display text
            primitiveRect = new Rectangle(0, (int)(Globals.GAME_HEIGHT / 1.3), (int)(Globals.GAME_WIDTH / 1.3), (int)(Globals.GAME_HEIGHT / 6));
            primitiveRect.X = Globals.GAME_WIDTH / 2 - primitiveRect.Width / 2;
            rect = new Helper.GameRectangle(gd, primitiveRect);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public virtual void LoadContent()
        {
            iteration = 0;
            textIteration = 0;
            shownText = "";
            isOver = false;

            isGoing = true;
        }

        /// <summary>
        /// Update cutscene
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        public virtual void Update(GameTime gameTime)
        {
            // save previous keyboard state
            prevKb = kb;
            // current keyboard state
            kb = Keyboard.GetState();

            // skip cutscene if player presses escape
            if (kb.IsKeyDown(Keys.Escape))
            {
                isOver = true;
            }

            // animate text
            if (isGoing)
            {
                // Make new letters appear based on time
                if (timeIncrement > 20)
                {
                    // Load letter by letter
                    if (iteration < fullText[textIteration].Length - 1)
                    {
                        iteration++;
                        shownText += fullText[textIteration][iteration];
                    }
                    else
                    {
                        // if the end is reached, stop animating
                        isGoing = false;
                    }

                    // reset time
                    timeIncrement = 0;
                }

                // increase timer
                timeIncrement += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                // When text stops, wait for user input (enter key)
                if (kb.IsKeyDown(Keys.Enter) && !prevKb.IsKeyDown(Keys.Enter))
                {
                    // there is more text to display, ditsplay it
                    if (textIteration < fullText.Length-1)
                    {
                        textIteration++;
                        isGoing = true;
                        shownText = "";
                        timeIncrement = 0;
                        iteration = 0;
                    }
                    else
                    {
                        // if there is no more text, start level
                        isOver = true;
                    }
                }
            }
        }

        /// <summary>
        /// Draw cutscene
        /// </summary>
        /// <param name="spriteBatch">controls screen's canvas</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // draw background art
            bg.Draw(spriteBatch);

            // Draw recangle
            rect.Draw(spriteBatch, Color.ForestGreen, true);
            // Draw text on rectangle
            spriteBatch.DrawString(textFont, shownText, new Vector2(rect.Left + rect.Width / 50, rect.Top + rect.Height / 30), Color.White);
        }

        /// <summary>
        /// Accessor for whether the scene is over
        /// </summary>
        public bool IsOver
        {
            get { return isOver; }
        }

    }
}
