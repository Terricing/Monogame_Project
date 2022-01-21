using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PASS3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3
{
    class Cutscene
    {
        // store cutscene image
        protected Img bg;

        // store which index of the text array is on
        protected int textIteration;

        // Store which index of the current text is on
        protected int iteration;
        
        // store full text and drawn text
        protected string [] fullText;
        protected string shownText;
        protected bool isGoing;
        
        // store textfont for displaying text
        protected SpriteFont textFont;

        // store time
        protected float timeIncrement;

        // maintain state
        protected bool isOver = false;

        // Get input
        private KeyboardState kb;
        private KeyboardState prevKb;
        
        // create rectangle to display text on
        private Rectangle primitiveRect;
        private Helper.GameRectangle rect;
        
        public Cutscene(ContentManager content, GraphicsDevice gd)
        {
            textFont = content.Load<SpriteFont>("Fonts/cutsceneText");
            primitiveRect = new Rectangle(0, (int)(Globals.GAME_HEIGHT / 1.3), (int)(Globals.GAME_WIDTH / 1.3), (int)(Globals.GAME_HEIGHT / 6));
            primitiveRect.X = Globals.GAME_WIDTH / 2 - primitiveRect.Width / 2;
            rect = new Helper.GameRectangle(gd, primitiveRect);
        }

        public void LoadContent()
        {
            iteration = 0;
            textIteration = 0;
            shownText = "";

            isGoing = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            prevKb = kb;
            kb = Keyboard.GetState();

            // animate text
            if (isGoing)
            {
                if (timeIncrement > 20)
                {
                    if (iteration < fullText[textIteration].Length - 1)
                    {
                        //if ((rect.Left + rect.Width / 50) + textFont.MeasureString(shownText).X > (rect.Right - rect.Width / 50))
                        //{
                        //    shownText += "\n";
                        //}

                        iteration++;
                        shownText += fullText[textIteration][iteration];
                    }
                    else
                    {
                        isGoing = false;
                    }
                    timeIncrement = 0;
                }

                timeIncrement += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                if (kb.IsKeyDown(Keys.Enter) && !prevKb.IsKeyDown(Keys.Enter))
                {
                    if (textIteration < fullText.Length-1)
                    {
                        textIteration++;
                        isGoing = true;
                        shownText = "";
                        timeIncrement = 0;
                        iteration = 0;
                    }
                    else
                    {
                        isOver = true;
                    }
                }
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            bg.Draw(spriteBatch);
            rect.Draw(spriteBatch, Color.ForestGreen, true);
            spriteBatch.DrawString(textFont, shownText, new Vector2(rect.Left + rect.Width / 50, rect.Top + rect.Height / 30), Color.White);
        }

        public bool IsOver
        {
            get { return isOver; }
        }

    }
}
