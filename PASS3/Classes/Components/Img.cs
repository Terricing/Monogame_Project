// Author: Eilay Katsnelson
// File Name: Img.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Generic image class to simplify process of adding images.
// Designed to be modular so can be used in different scenearios.


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
using Animation2D;
using Microsoft.Xna.Framework.Content;

namespace PASS3
{
    class Img
    {
        // Store basic img data
        private Texture2D img;
        private Rectangle imgRect;

        /// <summary>
        /// Constructor for creating img objects
        /// </summary>
        /// <param name="img">The image that the object should hold</param>
        public Img (Texture2D img)
        {
            this.img = img;
        }

        /// <summary>
        /// Create rectangle for the image
        /// </summary>
        /// <param name="x">x-location</param>
        /// <param name="y">y-location</param>
        /// <param name="scale">optional scale</param>
        public void LoadContent(int x, int y, float scale = 1f)
        {
            // create rectangle
            imgRect = new Rectangle(x, y, img.Width, img.Height);

            // implement scale
            if (scale != 1f)
            {
                imgRect.Width = (int)(imgRect.Width * scale);
                imgRect.Height = (int)(imgRect.Height * scale);
            }
        }

        /// <summary>
        /// Draw image
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, imgRect, Color.White);
        }

        /// <summary>
        /// Draw, overload, where color is specified to allow custom modification
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="color">Represents color that should cover the image</param>
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(img, imgRect, color);
        }

        /// <summary>
        /// Property for the image's underlying texture
        /// </summary>
        public Texture2D Image
        {
            get { return img; }
            set { img = value; }
        }

        /// <summary>
        /// Property for the image's rectangle
        /// </summary>
        public Rectangle ImgRect
        {
            get { return imgRect; }
            set { imgRect = value; }
        }

        /// <summary>
        /// Property for image's x-location
        /// </summary>
        public int X
        {
            get { return imgRect.X; }
            set { imgRect.X = value; }
        }

        /// <summary>
        /// Property for image's y-location
        /// </summary>
        public int Y
        {
            get { return imgRect.Y; }
            set { imgRect.Y = value; }
        }
    }
}
