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
        private Img head;

        private int startPos;
        private int endPos;
        private float pos;

        private float moveTime = 2f;
        private float moveTimer = 0f;

        private bool isAppearing;
        private bool isDisappearing;
        private bool isShown;

        public Head (ContentManager content)
        {
            startPos = -Globals.GAME_HEIGHT;
            endPos = 0;

            head = new Img(content.Load<Texture2D>("Screens/Game/Stage3/lane-boss"));
            head.LoadContent(0, startPos);
        }

        public void LoadContent()
        {
            pos = startPos;
            moveTimer = 0f;
            isAppearing = false ;
            isDisappearing = false;
            isShown = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            head.Draw(spriteBatch);
        }

        public void Appear(GameTime gameTime)
        {
            if (moveTimer < moveTime)
            {
                pos = MathHelper.Lerp(startPos, endPos, moveTimer / moveTime);
                moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveTimer = 0;
                pos = endPos;
                isAppearing = false;
                isShown = true;
            }

            head.Y = (int)pos;
        }

        public void Disappear(GameTime gameTime)
        {
            if (moveTimer < moveTime)
            {
                pos = MathHelper.Lerp(endPos, startPos, moveTimer / moveTime);
                moveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveTimer = 0;
                pos = startPos;
                isDisappearing = false;
                isShown = false;
            }
            head.Y = (int)pos;
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
