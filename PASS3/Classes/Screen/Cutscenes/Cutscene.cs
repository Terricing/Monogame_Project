using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        
        public Cutscene(ContentManager content)
        {
            textFont = content.Load<SpriteFont>("Fonts/cutsceneText");
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
            // animate text
            if (isGoing)
            {
                if (timeIncrement > 20)
                {
                    if (iteration < fullText[textIteration].Length - 1)
                    {
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

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //bg.Draw(spriteBatch);
            spriteBatch.DrawString(textFont, shownText, new Vector2(), Color.White);
        }

        public bool IsOver
        {
            get { return isOver; }
        }

    }
}
