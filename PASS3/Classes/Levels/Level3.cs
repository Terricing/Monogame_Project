// Author: Eilay Katsnelson
// File Name: Level2.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child level class for the third level

using Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PASS3.Classes.Components;
using PASS3.Classes.Components.Boss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Levels
{
    class Level3 : Level
    {
        // set level state
        public const int LEVEL = 3;

        // store boss body parts
        private LeftHand leftHand;
        private RightHand rightHand;
        private Head head;

        // Store attack manager
        private AttackManager attacks;

        // Store attack timer info
        private float attackTimer;
        private float attackTimeInterval = 5f;

        // store number of attacks (for controlling difficulty)
        private int numAttacks;

        // Keep track of whether level is over
        private bool isOver;

        /// <summary>
        /// Create third level
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="lifeManager">Keep track of lives</param>
        public Level3(ContentManager content, LifeManager lifeManager) : base ("Screens/Game/Stage3/bg", content, lifeManager)
        {
            // create head
            head = new Head(content);
            
            // Create hands
            leftHand = new LeftHand(content);
            rightHand = new RightHand(content);

            // create attack manager
            attacks = new AttackManager(content, leftHand, rightHand, head, player);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            leftHand.LoadContent();
            rightHand.LoadContent();
            head.LoadContent();

            attackTimer = 3;
            isOver = false;
        }

        /// <summary>
        /// Update level
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        /// <param name="kb">keep track of keypresses</param>
        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);
            attacks.Update(gameTime);

            // Attack algorithm
            // increase attack timer
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackTimer >= attackTimeInterval)
            {
                // if attack timer greater than interval, perform random attack
                attacks.RandomAttack();

                // reset attack timer
                attackTimer = 0;

                // increment number of attacks
                numAttacks++;

                // set time between attacks based on the number of occured attacks
                // this gradually increases difficulty
                if (numAttacks == 3)
                {
                    attackTimeInterval = 4.5f;
                }
                else if (numAttacks == 6)
                {
                    attackTimeInterval = 4f;
                }
                else if (numAttacks == 8)
                {
                    attackTimeInterval = 3.3f;
                }

                // if 15 attacks were reached, level is over
                if (numAttacks == 15)
                {
                    isOver = true;
                }
            }
        }

        /// <summary>
        /// Draw third level
        /// </summary>
        /// <param name="spriteBatch">controls screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            // Draw boss's parts
            head.Draw(spriteBatch);
            leftHand.Draw(spriteBatch);
            rightHand.Draw(spriteBatch);

            // draw attacks
            attacks.Draw(spriteBatch);
        }

        /// <summary>
        /// Accessor for whether level is over
        /// </summary>
        public bool IsOver
        {
            get { return isOver; }
        }
    }
}
