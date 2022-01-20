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
using PASS3.Classes.Screen;
using MonoGame.Extended;
using Animation2D;
using PASS3.Classes.Components;

namespace PASS3.Classes.Levels
{
    class Level
    {
        // Store images for scrolling background
        protected Img[] bg = new Img[2];
        protected float[] bgPos = new float[2];
        protected float bgSpeed;

        protected int levelState;

        // Store player object
        protected Player player;

        // keep track of level state
        protected bool isLevelFinished;

        // store obstacles

        public Level(string bgPath, ContentManager content, LifeManager lifeManager)
        {
            // Create image objects for scrolling screen
            bg[0] = new Img(content.Load<Texture2D>(bgPath));
            bg[1] = new Img(bg[0].Image);

            bg[0].LoadContent( 0, 0);
            bg[1].LoadContent(0, Globals.GAME_HEIGHT);

            // Create player object
            player = new Player(lifeManager);
            player.LoadContent(content);
        }

        public virtual void LoadContent()
        {
            bgPos[0] = bg[0].Y;
            bgPos[1] = bg[1].Y;
            isLevelFinished = false;
        }

        public virtual void Update(GameTime gameTime, KeyboardState kb)
        {
            // Screen scrolling
            BgScroll(gameTime);

            // update player
            player.Update(gameTime, kb);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Img img in bg) { img.Draw(spriteBatch); }
            player.Draw(spriteBatch);
        }

        // This procedure controls the scrolling background
        public void BgScroll(GameTime gameTime)
        {
            if (!isLevelFinished)
            {
                if (bgPos[0] >= Globals.GAME_HEIGHT)
                {
                    bgPos[0] += -2 * Globals.GAME_HEIGHT;
                }
                else if (bgPos[1] >= Globals.GAME_HEIGHT)
                {
                    bgPos[1] += -2 * Globals.GAME_HEIGHT;
                }
            }

            bgSpeed = -1 * MainGame.BgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            bgPos[0] -= bgSpeed;
            bgPos[1] -= bgSpeed;
            bg[0].Y = (int)bgPos[0];
            bg[1].Y = (int)bgPos[1];
        }

        protected virtual void Reset()
        {

        }

        protected void AdjustBg()
        {
            bg[0].Y = -Globals.GAME_HEIGHT;
            bg[1].Y = -Globals.GAME_HEIGHT * 2;

            bgPos[0] = bg[0].Y;
            bgPos[1] = bg[1].Y;
        }

        protected virtual void OnLevelOver()
        {

        }

        public bool IsTransition()
        {
            if ((bg[1].Y >= Globals.GAME_HEIGHT) && bg[0].Y >= Globals.GAME_HEIGHT )
            {
                return false;
            }

            return true;
        }


        public void BgDraw(SpriteBatch spriteBatch)
        {
            bg[0].Draw(spriteBatch);
            bg[1].Draw(spriteBatch);
        }
        //public Img Bg
        //{
        //    get { return bg[1]; }
        //}
    }
}
