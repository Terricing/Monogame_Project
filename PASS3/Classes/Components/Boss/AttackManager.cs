// Author: Eilay Katsnelson
// File Name: AttackManager.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A class that manages attacks from Mr. Lane

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
    class AttackManager
    {
        // Store images for attacks
        private Img leftLaser;
        private Img rightLaser;
        private Img centerLaser;

        // Store boss body parts
        private LeftHand leftHand;
        private RightHand rightHand;
        private Head head;

        // store player's object
        private Player player;

        // attackTime representing how long an attack takes
        private float attackTime = 3f;

        // store state info for left, right, and center attacks
        private float leftAttackTimer;
        private bool leftAttack;
        private bool showLeftAttack;

        private float rightAttackTimer;
        private bool rightAttack;
        private bool showRightAttack;

        private float centerAttackTimer;
        private bool centerAttack;
        private bool showCenterAttack;

        /// <summary>
        /// Create object for attack manager
        /// </summary>
        /// <param name="content">allows for loading content</param>
        /// <param name="leftHand">boss's left hand</param>
        /// <param name="rightHand"><boss's right hand/param>
        /// <param name="head">boss's head</param>
        /// <param name="player">the player</param>
        public AttackManager(ContentManager content, LeftHand leftHand, RightHand rightHand, Head head, Player player)
        {
            // Assign values
            this.leftHand = leftHand;
            this.rightHand = rightHand;
            this.head = head;
            this.player = player;

            // Create attack images and rectangles
            leftLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/left-laser"));
            leftLaser.LoadContent(0, 0);
            rightLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/right-laser"));
            rightLaser.LoadContent(0, 0);
            centerLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/center-laser"));
            centerLaser.LoadContent(0, 0);
        }

        /// <summary>
        /// Update attack manager to coordinate attacks
        /// </summary>
        /// <param name="gameTime">track time between updates</param>
        public void Update(GameTime gameTime)
        {
            // Perform attack when supposed to
            if (leftAttack)
            {
                LeftAttack(gameTime);
            }

            if (rightAttack)
            {
                RightAttack(gameTime);
            }

            if (centerAttack)
            {
                CenterAttack(gameTime);
            }
        }

        /// <summary>
        /// Start the process of a left attack
        /// </summary>
        public void DoLeftAttack()
        {
            // if left attack is not already happening, do left attack
            if (!leftAttack)
            {
                leftAttack = true;
                leftAttackTimer = 0;
            }
            
        }

        /// <summary>
        /// Start the process of a right attack
        /// </summary>
        public void DoRightAttack()
        {
            //if right attack is not already happening, do right attack
            if (!rightAttack)
            {
                rightAttack = true;
                rightAttackTimer = 0;
            }
        }

        /// <summary>
        /// Start the process of a center attack
        /// </summary>
        public void DoCenterAttack()
        {
           // if center is not happening, do center attack
           if (!centerAttack)
            {
                centerAttack = true;
                centerAttackTimer = 0f;
            }
        }

        /// <summary>
        /// Perform attack on the left side
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        private void LeftAttack(GameTime gameTime)
        {
            // If left hand is not already on screen, make it appear
            if (!leftHand.IsShown)
            {
                leftHand.Appear(gameTime);
            }
            else if (leftHand.IsShown)
            {          
                // if left hand is on screen, show left attack for specified time
                if (leftAttackTimer < attackTime)
                {
                    // increment attack timer
                    leftAttackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    showLeftAttack = true;

                    if (player.Lane == 0)
                    {
                        // if player is in left lane when attack happens, lose a life
                        player.LoseLife();
                    }
                }
                else
                {
                    // if attack is over, do not show it anymore
                    showLeftAttack = false;
                    leftHand.Disappear(gameTime);

                    // if hand dissappears, stop attack so it can it be done again
                    if (!leftHand.IsShown)
                    {
                        leftAttack = false;
                    }
                }
            }
        }

        /// <summary>
        /// Perform attack on right lane
        /// </summary>
        /// <param name="gameTime">Time between updates</param>
        private void RightAttack(GameTime gameTime)
        {
            // if right hand is not on screen, make it appear
            if (!rightHand.IsShown)
            {
                rightHand.Appear(gameTime);
            }
            else
            {
                // if right hand on screen, show right attack for specified time
                if (rightAttackTimer < attackTime)
                {
                    // increment attack timer
                    rightAttackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    showRightAttack = true;

                    // if player is in the attack's lane, lose a life
                    if (player.Lane == 2)
                    {
                        player.LoseLife();
                    }
                }
                else
                {
                    // if attack is over, do not show it anymore
                    showRightAttack = false;
                    rightHand.Disappear(gameTime);

                    // if hand dissappears, stop attack so it can it be done again
                    if (!rightHand.IsShown)
                    {
                        rightAttack = false;
                    }
                }

            }
        }

        /// <summary>
        /// Perform attack on center lane
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        private void CenterAttack(GameTime gameTime)
        {
            // if head is not yet shown, make it appear
            if (!head.IsShown)
            {
                head.Appear(gameTime);
            }
            else
            {
                // if head is on screen, show center attack for specified time
                if (centerAttackTimer < attackTime)
                {
                    // increment attack timer
                    centerAttackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    showCenterAttack = true;

                    // if player is in attack's lane, lose life
                    if (player.Lane == 1)
                    {
                        player.LoseLife();
                    }

                }
                else
                {
                    // if attack is over, do not show it anymore
                    showCenterAttack = false;
                    head.Disappear(gameTime);

                    // if head dissappears, stop attack so it can it be done again
                    if (!head.IsShown)
                    {
                        centerAttack = false;
                    }
                }
            }
        }

        /// <summary>
        /// Perform a random attack
        /// </summary>
        public void RandomAttack()
        {
            // generate random number that represents one of the attacks
            int randNum = Globals.Rand.Next(1, 4);

            // Perform attack depending on the generated number
            if (randNum == 1)
            {
                DoLeftAttack();
            }
            else if (randNum == 2)
            {
                DoRightAttack();
            }
            else if (randNum == 3)
            {
                DoCenterAttack();
            }
        }

        /// <summary>
        /// Draw boss's attacks
        /// </summary>
        /// <param name="spriteBatch">represents screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw boss's attacks
            if (showLeftAttack)
            {
                leftLaser.Draw(spriteBatch);
            }

            if (showRightAttack)
            {
                rightLaser.Draw(spriteBatch);
            }

            if (showCenterAttack)
            {
                centerLaser.Draw(spriteBatch);
            }   
        }
    }
}
