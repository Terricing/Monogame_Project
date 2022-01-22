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
using PASS3.Classes;

namespace PASS3
{
    class Obstacle
    {
        private Img obs;
        private float truePos;

        //Movement data
        private float speed;
        private float scale;

        public Obstacle (ContentManager content, string path, float scale = 1f)
        {
            obs = new Img(content.Load<Texture2D>(path));
            obs.LoadContent(0, -obs.ImgRect.Height, scale);

            truePos = obs.Y;

            // store for copy
            this.scale = scale;

        }

        // override used for making copies
        private Obstacle()
        {

        }


        public void Update(GameTime gameTime)
        {
            // move obstacle
            speed = (float)gameTime.ElapsedGameTime.TotalSeconds * Globals.BgScrollSpeed;
            truePos += speed;
            obs.Y = (int)truePos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            obs.Draw(spriteBatch);
            spriteBatch.DrawRectangle(obs.ImgRect, Color.Orange);
        }

        public bool IsOffScreen()
        {
            if (obs.Y >= Globals.GAME_HEIGHT)
            {
                return true;
            }

            return false;
        }

        // Return a copy of the obstacle
        public Obstacle Copy()
        {
            Obstacle temp = new Obstacle();
            temp.Image = new Img(obs.Image);
            //temp.Image.LoadContent(obs.Image, MainGame.LanePos[Game1.Rand.Next(0, MainGame.LanePos.Length)], -obs.ImgRect.Height, scale);
            temp.Image.LoadContent(Globals.LanePos[Globals.Rand.Next(0, Globals.LanePos.Length)], 0, scale);
            temp.Image.X -= temp.Image.ImgRect.Width / 2;
            temp.truePos = -obs.ImgRect.Height;
            return temp;
        }

        // Accessor and setter for the obstacle's image
        public Img Image
        {
            get { return obs; }
            set { obs = value; }
        }

        // Accessor and setter for the obstacle's speed
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float TruePos
        {
            set { truePos = value; }
        }
    }
}
