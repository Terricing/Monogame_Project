// Author: Eilay Katsnelson
// File Name: Head.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A class for boss's head

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
    class Head
    {
        // head's image
        private Img head;

        // store location info
        private int startPos;
        private int endPos;
        private float pos;

        // store move time info
        private float moveTime = 2f;
        private float moveTimer = 0f;

        // store whether head is currently shown on screen
        private bool isShown;

        /// <summary>
        /// Create boss's head
        /// </summary>
        /// <param name="content">load content</param>
        public Head (ContentManager content)
        {
            // set initial positions
            startPos = -Globals.GAME_HEIGHT;
            endPos = 0;

            // load head's image and create rectangle
            head = new Img(content.Load<Texture2D>("Screens/Game/Stage3/lane-boss"));
            head.LoadContent(0, startPos);
        }

        /// <summary>
        /// Set initial values of changing variables
        /// </summary>
        public void LoadContent()
        {
            pos = startPos;
            moveTimer = 0f;
            isShown = false;
        }

        /// <summary>
        /// Draw boss's head
        /// </summary>
        /// <param name="spriteBatch">controls screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            head.Draw(spriteBatch);
        }

        /// <summary>
        /// Make head appear with time
        /// </summary>
        /// <param name="gameTime">represents passing time</param>
        public void Appear(GameTime gameTime)
        {
            // if timer is running, make head move to required spot
            if (moveTimer < moveTime)
            {
                pos = MathHelper.Lerp(startPos, endPos, moveTimer / moveTime);
                moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // When head is finished moving, adjust values
                moveTimer = 0;
                pos = endPos;
                isShown = true;
            }

            // Adjust head's position
            head.Y = (int)pos;
        }

        /// <summary>
        /// Make head dissappear
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        public void Disappear(GameTime gameTime)
        {
            // If timer running, move head's position
            if (moveTimer < moveTime)
            {
                pos = MathHelper.Lerp(endPos, startPos, moveTimer / moveTime);
                moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                // If timer finished, adjust values
                moveTimer = 0;
                pos = startPos;
                isShown = false;
            }
            head.Y = (int)pos;
        }

        /// <summary>
        /// Accessor for whether head is currently on screen
        /// </summary>
        public bool IsShown
        {
            get { return isShown; }
        }

    }
}
