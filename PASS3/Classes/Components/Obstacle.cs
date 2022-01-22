// Author: Eilay Katsnelson
// File Name: Obstacle.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A class representing obstacles

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
        // store obstacle image
        private Img obs;

        // store obstacles true y position
        private float truePos;

        // store speed at which to move obstacle
        private float speed;

        // scale obstacle
        private float scale;

        /// <summary>
        /// Create obstacle object
        /// </summary>
        /// <param name="content"></param>
        /// <param name="path">represents location of the obstacle's image</param>
        /// <param name="scale">represents size of the obstacle relative to original size</param>
        public Obstacle (ContentManager content, string path, float scale = 1f)
        {
            // load image based on given path
            obs = new Img(content.Load<Texture2D>(path));

            // Create surrounding rectangle
            obs.LoadContent(0, -obs.ImgRect.Height, scale);

            // Set true y-position
            truePos = obs.Y;

            // store for copy
            this.scale = scale;

        }

        /// <summary>
        /// override used for making copies 
        /// </summary>
        private Obstacle()
        {

        }

        /// <summary>
        /// Update obstacle data
        /// </summary>
        /// <param name="gameTime">elapsed time between updates</param>
        public void Update(GameTime gameTime)
        {
            // move obstacle
            speed = (float)gameTime.ElapsedGameTime.TotalSeconds * Globals.BgScrollSpeed;
            truePos += speed;
            obs.Y = (int)truePos;
        }

        /// <summary>
        /// Draw obstacle
        /// </summary>
        /// <param name="spriteBatch">controller for canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw obstacle
            obs.Draw(spriteBatch);
        }

        /// <summary>
        /// Checks if the obstacle is offscreen
        /// </summary>
        /// <returns>bool value representing whether the obstacle is off screen</returns>
        public bool IsOffScreen()
        {
            // if obstacle is offscreen, return true
            if (obs.Y >= Globals.GAME_HEIGHT)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates a copy of the obstacle
        /// </summary>
        /// <returns>Copy of the obstacle</returns>
        public Obstacle Copy()
        {
            // create temporary obstacle and load image
            Obstacle temp = new Obstacle();
            temp.Image = new Img(obs.Image);
            
            // Create rectangle, give it a random lane location
            temp.Image.LoadContent(Globals.LanePos[Globals.Rand.Next(0, Globals.LanePos.Length)], 0, scale);
            temp.Image.X -= temp.Image.ImgRect.Width / 2;
            temp.truePos = -obs.ImgRect.Height;

            // return created obstacle
            return temp;
        }

        /// <summary>
        /// Accessor and setter for the obstacle's image
        /// </summary>
        public Img Image
        {
            get { return obs; }
            set { obs = value; }
        }

        /// <summary>
        /// Accessor and setter for the obstacle's speed
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Accessor for obstacle's y-position
        /// </summary>
        public float TruePos
        {
            set { truePos = value; }
        }
    }
}
