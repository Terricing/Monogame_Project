// Author: Eilay Katsnelson
// File Name: Hand.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A parent class for boss's hands

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Components.Boss
{
    class Hand
    {
        // Store hand's image
        protected Img hand;

        // store time movement should take, and current time in translation
        protected float moveTime;
        protected float handTimer;

        // store location related info
        protected float startPos;
        protected float endPos;
        protected float handPos;

        // Store whether hand is currently on screen
        protected bool isShown;

        protected Hand()
        {
            // inital values
            endPos = 0f;
            moveTime = 2f;
        }

        public void LoadContent()
        {
            // set initial values (that change often)
            handTimer = 0;
            handPos = startPos;

            // set inital state
            isShown = false;
        }

        /// <summary>
        /// Draw boss's hand
        /// </summary>
        /// <param name="spriteBatch">controls screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            hand.Draw(spriteBatch);
        }

        /// <summary>
        /// Makes hand appear on screen over a set period of time
        /// </summary>
        /// <param name="gameTime">Keep track of time passing</param>
        public void Appear(GameTime gameTime)
        {
            // Make hand move to required area based on time
            if (handTimer < moveTime)
            {
                handPos = MathHelper.Lerp(startPos, endPos, handTimer / moveTime);
                handTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // when finished translation, set values accordingly (because lerp function can be imperfect)
                handPos = endPos;
                isShown = true;
                handTimer = 0;
            }

            // change hand's location
            hand.X = (int)handPos;
        }

        /// <summary>
        /// Makes hand disappear from screen over a set period of time
        /// </summary>
        /// <param name="gameTime">Keep track of time passing</param>
        public void Disappear(GameTime gameTime)
        {
            // Make hand moved to required area based on time
            if (handTimer < moveTime)
            {
                handPos = MathHelper.Lerp(endPos, startPos, handTimer / moveTime);
                handTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // when finished translation, set values accordingly
                handPos = startPos;
                isShown = false;
                handTimer = 0;
            }

            // change hand's location
            hand.X = (int)handPos;
        }
        
        /// <summary>
        /// Accesor for whether hand is onscreen
        /// </summary>
        public bool IsShown
        {
            get { return isShown; }
        }

    }



}
