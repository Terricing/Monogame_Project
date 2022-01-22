// Author: Eilay Katsnelson
// File Name: Button.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Class for creating buttons that are highlighted when hovered

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
using Animation2D;

namespace PASS3
{
    class Button
    {
       // Store button's normal image
        private Texture2D bt;
        private Rectangle btRect;

        // Store button's highlighted image
        private Texture2D selectedBt;
        private Rectangle selectedBtRect;

        // store button's state
        private bool isHighlighted = false;
        private bool isSelected = false;

        // Store mouse's location
        private Vector2 mousePosVect = new Vector2(0, 0);

        /// <summary>
        /// Button constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="btPath"></param>
        /// <param name="selectedBtPath"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public Button(ContentManager content, string btPath, string selectedBtPath, int x, int y, float scale = 1f)
        {
            // Load the buttons and create rectangles
            bt = content.Load<Texture2D>(btPath);
            btRect = new Rectangle(x, y, (int)(bt.Width * scale), (int)(bt.Height * scale));
            selectedBt = content.Load<Texture2D>(selectedBtPath);
            selectedBtRect = new Rectangle(x, y, (int)(scale * selectedBt.Width), (int)(scale * selectedBt.Height));
        }

        /// <summary>
        /// Draw button
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // if button is not selected
            if (!isSelected)
            {
                // if button is hovered over, draw regular button, else draw highlighted button
                if (!isHighlighted)
                {
                    spriteBatch.Draw(bt, btRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(selectedBt, selectedBtRect, Color.White);
                }
            }
            else
            {
                // if button is selected, draw highlighted version
                spriteBatch.Draw(selectedBt, selectedBtRect, Color.White);
            }

        }

        /// <summary>
        /// Update button
        /// </summary>
        /// <param name="mouseState"></param>
        public void Update(MouseState mouseState)
        {
            // Change button state depending on whether it is hovered on
            if (CheckCollision(mouseState.Position))
            {
                isHighlighted = true;
            }
            else
            {
                isHighlighted = false;
            }
        }

        /// <summary>
        /// Check whether a mouse point collides with the button
        /// </summary>
        /// <param name="mousePos">represents mouse's location</param>
        /// <returns></returns>
        public bool CheckCollision(Point mousePos)
        {
            // return the result of the collision check
            mousePosVect.X = mousePos.X;
            mousePosVect.Y = mousePos.Y;
            return Helper.Util.Intersects(btRect, mousePosVect);
        }

        /// <summary>
        /// Property for whether button is selected
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        /// <summary>
        /// Property for accessing button's rectangle
        /// </summary>
        public Rectangle BtRect
        {
            get { return btRect; }
        }

        /// <summary>
        /// Property for button's x location
        /// </summary>
        public int X
        {
            get { return btRect.X; }
            set
            {
                btRect.X = value;
                selectedBtRect.X = value;
            }
        }

        /// <summary>
        /// Property for button's y location
        /// </summary>
        public int Y
        {
            get { return btRect.Y; }
            set
            {
                btRect.Y = value;
                selectedBtRect.Y = value;
            }
        }
    }
}
