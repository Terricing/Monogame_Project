// Author: Eilay Katsnelson
// File Name: Player.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A class representing the player

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
using PASS3.Classes.Components;
using PASS3.Classes;

namespace PASS3
{
    class Player
    {
        // create player animation
        private Animation player;

        // store player's lane
        private int lane;

        // control moving lane to lane
        private float maxSpeed = 1500f;
        private float playerSpeed;
        private int dir;
        private float actualPos;
        private int curLanePos;

        // store player state info
        private bool drawCharacter;
        private bool isHit;

        // store game's life manager
        private LifeManager lifeManager;

        // maintain keyboard's state
        private KeyboardState prevKb;

        /// <summary>
        /// Create player object
        /// </summary>
        /// <param name="lifeManager">controls remaining lives</param>
        public Player(LifeManager lifeManager)
        {
            // set default value
            lane = 1;
            isHit = false;
            drawCharacter = true;
            this.lifeManager = lifeManager;
        }

        /// <summary>
        /// load player and create rectangle
        /// </summary>
        /// <param name="content">Allows for loading of content</param>
        public void LoadContent(ContentManager content)
        {
            // load player and create rectangle
            player = new Animation(content.Load<Texture2D>("Screens/Game/running_player"), 2, 1, 2, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 6, new Vector2(Globals.LanePos[1]), 0.6f, true);
            player.destRec.X -= player.destRec.Width / 2;
            actualPos = player.destRec.X;
        }

        /// <summary>
        /// Update player
        /// </summary>
        /// <param name="gameTime">calculate time between updates</param>
        /// <param name="kb">information about current keypresses</param>
        public void Update(GameTime gameTime, KeyboardState kb)
        {
            // update player's animation
            player.Update(gameTime);

            // change lane on request
            if (kb.IsKeyDown(Keys.Left) && !prevKb.IsKeyDown(Keys.Left) && lane != 0 && dir == 0)
            {
                lane--;
                curLanePos = Globals.LanePos[lane] - player.destRec.Width / 2;
                dir = -1;
            }
            else if (kb.IsKeyDown(Keys.Right) && !prevKb.IsKeyDown(Keys.Right) && lane != 2 && dir == 0)
            {
                lane++;
                curLanePos = Globals.LanePos[lane] - player.destRec.Width / 2;
                dir = 1;
            }

            // stop player when he reaches lane
            if (Math.Abs(actualPos - curLanePos) < 10 && dir != 0)
            {
                dir = 0;
            }

            // move player
            playerSpeed = dir * (maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            actualPos += playerSpeed;
            player.destRec.X = (int)actualPos;

            // Create visual invincibility effect when a life is lost
            if (lifeManager.InvincibilityTimer.IsActive())
            {
                drawCharacter = !drawCharacter;
            }
            else if (lifeManager.InvincibilityTimer.IsFinished())
            {
                // stop invincibility effect when invincibility timer is finished
                drawCharacter = true;
                isHit = false;
                lifeManager.InvincibilityTimer.Deactivate();
            }

            // store previous frame's keyboard state
            prevKb = kb;
        }

        /// <summary>
        /// Draw player
        /// </summary>
        /// <param name="spriteBatch">Controller for screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw character or flicker character
            if (drawCharacter)
            {
                player.Draw(spriteBatch, Color.White, Animation.FLIP_NONE);
            }
        }

        /// <summary>
        /// Accessor for character's bounding rectangle
        /// </summary>
        public Rectangle Rect
        {
            get { return player.destRec; }
        }

        /// <summary>
        /// Lose a life
        /// </summary>
        public void LoseLife()
        {
            isHit = true;

            // if invincibility timer is inactive, decrement lives and start timer
            if (lifeManager.InvincibilityTimer.IsInactive())
            {
                isHit = true;
                lifeManager.LoseLife();
            }
        }

        /// <summary>
        /// Accessor for whether the player is currently hit and has invincibility
        /// </summary>
        public bool IsHit
        {
            get { return isHit; }
        }

        /// <summary>
        /// Accessor for character's current lane
        /// </summary>
        public int Lane
        {
            get { return lane; }
        }
    }
}
