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
        // Left hand and attack
        protected Img hand;
        protected float handTimer;
        protected float startPos;
        protected float endPos;
        protected float handPos;
        protected float moveTime;

        protected bool isAppearing;
        protected bool isShown;
        protected bool isDisappearing;

        protected Hand()
        {
            endPos = 0f;
            moveTime = 2f;
        }

        public void LoadContent()
        {
            // set initial values (that change often)
            handTimer = 0;
            handPos = startPos;

            // set inital states
            isAppearing = true;
            isShown = false;
            isDisappearing = false;
        }

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
            if (handTimer < moveTime)
            {
                handPos = MathHelper.Lerp(startPos, endPos, handTimer / moveTime);
                handTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // when finished translation, set values accordingly
                handPos = endPos;
                isAppearing = false;
                isShown = true;
                handTimer = 0;
            }

            hand.X = (int)handPos;
        }

        /// <summary>
        /// Makes hand disappear from screen over a set period of time
        /// </summary>
        /// <param name="gameTime">Keep track of time passing</param>
        public void Disappear(GameTime gameTime)
        {
            if (handTimer < moveTime)
            {
                handPos = MathHelper.Lerp(endPos, startPos, handTimer / moveTime);
                handTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // when finished translation, set values accordingly
                handPos = startPos;
                isAppearing = false;
                isShown = false;
                handTimer = 0;
            }

            hand.X = (int)handPos;
        }

        public bool IsAppearing
        {
            get { return isAppearing; }
        }

        public bool IsDissappearing
        {
            get { return isDisappearing; }
        }

        public bool IsShown
        {
            get { return isShown; }
        }

    }



}
