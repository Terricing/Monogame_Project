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
using Helper;
using PASS3.Classes.Screen;

namespace PASS3.Classes.Components
{
    class LifeManager
    {
        private int numLives;
        private Texture2D heart;
        private Img[] hearts = new Img[3];
        private bool[] heartsActive = new bool[3];

        private int lifeTimer;
        private Timer invincibilityTimer;

        public LifeManager(ContentManager content)
        {
            numLives = 3;

            heart = content.Load<Texture2D>("Screens/Game/heart");
            lifeTimer = 1500;
            invincibilityTimer = new Timer(lifeTimer, false);

            float heartScale = 0.2f;

            for (int i = 0; i < hearts.Length; i++)
            {
                hearts[i] = new Img(heart);
                hearts[i].LoadContent(Globals.GAME_WIDTH - (int)(heart.Width * heartScale), (Globals.GAME_HEIGHT / 10) * i, heartScale);
                heartsActive[i] = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            invincibilityTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (heartsActive[i])
                {
                    hearts[i].Draw(spriteBatch);
                }
            }
        }

        public void LoseLife()
        {
            for (int i = heartsActive.Length - 1; i >= 0; i--)
            {
                if (heartsActive[i])
                {
                    heartsActive[i] = false;
                    numLives--;
                    break;
                }
            }

            if (numLives == 0)
            {
                Globals.GameState = GameOver.GAMESTATE;
            }
            else
            {
                invincibilityTimer.ResetTimer(true);
            }
        }

        public Timer InvincibilityTimer
        {
            get { return invincibilityTimer; }
        }

        public int Lives
        {
            get { return numLives; }
            set { numLives = value; }
        }

    }
}
