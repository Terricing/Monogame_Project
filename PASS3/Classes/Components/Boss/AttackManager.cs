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
        private Img leftLaser;
        private Img rightLaser;
        private Img centerLaser;

        private LeftHand leftHand;
        private RightHand rightHand;
        private Head head;

        private Player player;

        private float attackTime = 3f;

        private float leftAttackTimer;
        private bool leftAttack;
        private bool showLeftAttack;

        private float rightAttackTimer;
        private bool rightAttack;
        private bool showRightAttack;

        private float centerAttackTimer;
        private bool centerAttack;
        private bool showCenterAttack;

        public AttackManager(ContentManager content, LeftHand leftHand, RightHand rightHand, Head head, Player player)
        {
            this.leftHand = leftHand;
            this.rightHand = rightHand;
            this.head = head;

            this.player = player;

            leftLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/left-laser"));
            leftLaser.LoadContent(0, 0);
            rightLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/right-laser"));
            rightLaser.LoadContent(0, 0);
            centerLaser = new Img(content.Load<Texture2D>("Screens/Game/Stage3/center-laser"));
            centerLaser.LoadContent(0, 0);
        }

        public void Update(GameTime gameTime)
        {
            // when per
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

        public void DoLeftAttack()
        {
            //if left attack is not already happening, do left attack
            if (!leftAttack)
            {
                leftAttack = true;
                leftAttackTimer = 0;
            }
            
        }

        public void DoRightAttack()
        {
            //if right attack is not already happening, do right attack
            if (!rightAttack)
            {
                rightAttack = true;
                rightAttackTimer = 0;
            }
        }

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
        /// <param name="gameTime"></param>
        private void LeftAttack(GameTime gameTime)
        {
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

        private void RightAttack(GameTime gameTime)
        {
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

        private void CenterAttack(GameTime gameTime)
        {
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

        public void RandomAttack()
        {
            int randNum = Globals.Rand.Next(1, 4);

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

        public void Draw(SpriteBatch spriteBatch)
        {
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
