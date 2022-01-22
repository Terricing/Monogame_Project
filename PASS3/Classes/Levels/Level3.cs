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
        public const int LEVEL = 3;
        const int NONE = 0;
        const int LEFT = 1;
        const int RIGHT = 2;
        const int BOTH = 3;

        // store boss body parts
        private LeftHand leftHand;
        private RightHand rightHand;
        private Head head;

        // Store attack manager
        private AttackManager attacks;

        private float attackTimer;
        private float attackTimeInterval = 5f;
        private int numAttacks;

        private bool isOver;


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

        public override void LoadContent()
        {
            base.LoadContent();
            leftHand.LoadContent();
            rightHand.LoadContent();
            head.LoadContent();

            attackTimer = 3;
            isOver = false;
        }

        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);
            attacks.Update(gameTime);
            //attacks.DoLeftAttack();
            ////attacks.DoRightAttack();
            //attacks.DoCenterAttack();

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
                    attackTimeInterval = 3f;
                }

                if (numAttacks == 15)
                {
                    isOver = true;
                }
            }

            //head.Appear(gameTime);
            //leftHand.Appear(gameTime);
            //rightHand.Appear(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            head.Draw(spriteBatch);
            leftHand.Draw(spriteBatch);
            rightHand.Draw(spriteBatch);

            attacks.Draw(spriteBatch);
            //rightHand.Draw(spriteBatch);
        }

        public bool IsOver
        {
            get { return isOver; }
        }
    }
}
