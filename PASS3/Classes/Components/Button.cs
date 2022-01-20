// Class for creating buttons that are highlighted when pressed

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
        // What the button looks like
        private Texture2D bt;
        private Rectangle btRect;

        // What the button looks like when highlighted
        private Texture2D selectedBt;
        private Rectangle selectedBtRect;

        private bool isHighlighted = false;
        private bool isSelected = false;

        private Vector2 mousePosVect;

        public Button(ContentManager content, string btPath, string selectedBtPath, int x, int y, float scale = 1f)
        {
            // Load the buttons and create rectangles
            bt = content.Load<Texture2D>(btPath);
            btRect = new Rectangle(x, y, (int)(bt.Width * scale), (int)(bt.Height * scale));
            selectedBt = content.Load<Texture2D>(selectedBtPath);
            selectedBtRect = new Rectangle(x, y, (int)(scale * selectedBt.Width), (int)(scale * selectedBt.Height));

            mousePosVect = new Vector2(0, 0);

            if (scale != 1)
            {
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isSelected)
            {
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
                spriteBatch.Draw(selectedBt, selectedBtRect, Color.White);
            }

        }

        public void Update(MouseState mouseState)
        {
            if (CheckCollision(mouseState.Position))
            {
                isHighlighted = true;
            }
            else
            {
                isHighlighted = false;
            }
        }

        public bool CheckCollision(Point mousePos)
        {
            mousePosVect.X = mousePos.X;
            mousePosVect.Y = mousePos.Y;
            return Helper.Util.Intersects(btRect, mousePosVect);
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public Rectangle BtRect
        {
            get { return btRect; }
        }

        public int X
        {
            get { return btRect.X; }
            set
            {
                btRect.X = value;
                selectedBtRect.X = value;
            }
        }

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
