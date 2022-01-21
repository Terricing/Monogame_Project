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


        public Level3(ContentManager content, LifeManager lifeManager) : base ("Screens/Game/Stage3/bg", content, lifeManager)
        {
            // create head
            head = new Head(content);
            
            // Create hands
            leftHand = new LeftHand(content);
            rightHand = new RightHand(content);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            leftHand.LoadContent();
        }

        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);
            head.Appear(gameTime);
            leftHand.Appear(gameTime);
            rightHand.Appear(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            head.Draw(spriteBatch);
            leftHand.Draw(spriteBatch);
            rightHand.Draw(spriteBatch);
            //rightHand.Draw(spriteBatch);
        }

        public void DoLeftAttack()
        {
        }



        public void DissappearLeftHand()
        {

        }
    }
}
