// Generic img class to simplify process of adding images.
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

        // Constructor overload where Texture2D is already loaded (from another image)
        // probably not needed
        public Img (Texture2D img)
        {
            this.img = img;
        }


        public void LoadContent(int x, int y, float scale = 1f)
        {
            imgRect = new Rectangle(x, y, img.Width, img.Height);

            if (scale != 1f)
            {
                imgRect.Width = (int)(imgRect.Width * scale);
                imgRect.Height = (int)(imgRect.Height * scale);
            }
        }

        //public void LoadContent(ContentManager content, string path, int x, int y, float scale = 1f)
        //{
        //    img = content.Load<Texture2D>(path);
        //    imgRect = new Rectangle(x, y, img.Width, img.Height);
        //    if (scale != 1f)
        //    {
        //        imgRect.Width = (int)(imgRect.Width * scale);
        //        imgRect.Height = (int)(imgRect.Height * scale);
        //    }
        //}
        //// Overload 1, loadcontent where rectangle is specified immidiately
        //public void LoadContent(ContentManager content, string path, Rectangle imgRect)
        //{
        //    img = content.Load<Texture2D>(path);
        //    this.imgRect = imgRect;
        //}

        //// Overload 2, loadcontent where rectangle is not specified to allow flexibility and create it after img is loaded
        //public void LoadContent(ContentManager content, string path)
        //{
        //    img = content.Load<Texture2D>(path);
        //}

        //// Overload 3, where texture2D has already been loaded (from another image) and only the rectangle remains
        //public void LoadContent(ContentManager contentManager, Rectangle imgRect)
        //{
        //    this.imgRect = imgRect;
        //}        //// Overload 1, loadcontent where rectangle is specified immidiately
        //public void LoadContent(ContentManager content, string path, Rectangle imgRect)
        //{
        //    img = content.Load<Texture2D>(path);
        //    this.imgRect = imgRect;
        //}

        //// Overload 2, loadcontent where rectangle is not specified to allow flexibility and create it after img is loaded
        //public void LoadContent(ContentManager content, string path)
        //{
        //    img = content.Load<Texture2D>(path);
        //}

        //// Overload 3, where texture2D has already been loaded (from another image) and only the rectangle remains
        //public void LoadContent(ContentManager contentManager, Rectangle imgRect)
        //{
        //    this.imgRect = imgRect;
        //}

        // Draw, overload 1, where no color is specified so that no change is made to the img
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, imgRect, Color.White);
        }

        // Draw, overload 2, where color is specified to allow custom modification
        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(img, imgRect, color);
        }

        // Properties
        public Texture2D Image
        {
            get { return img; }
            set { img = value; }
        }

        public Rectangle ImgRect
        {
            get { return imgRect; }
            set { imgRect = value; }
        }

        // remove this if possible
        public int X
        {
            get { return imgRect.X; }
            set { imgRect.X = value; }
        }

        public int Y
        {
            get { return imgRect.Y; }
            set { imgRect.Y = value; }
        }
    }
}
