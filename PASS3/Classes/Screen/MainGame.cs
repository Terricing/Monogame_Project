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
using PASS3.Classes.Levels;
using MonoGame.Extended;
using Animation2D;
using PASS3.Classes.Components;
using PASS3.Classes;
using Helper;
using PASS3.Classes.Screen.Cutscenes;

namespace PASS3
{
    class MainGame
    {
        // Store gamestate
        public const int GAMESTATE = 1;

        // store scroll speed
        private static float bgScrollSpeed = 400f;

        // Store interfaces
        private ContentManager content;
        private GraphicsDevice gd;

        // Create level1 object
        private Level1 level1;
        private Level2 level2;

        private int score;
        private Timer scoreTimer;
        private SpriteFont scoreFont;
        private Vector2 scorePos;
        private string scoreText;

        private LifeManager lifeManager;

        private string playerName;

        public MainGame(ContentManager content, GraphicsDevice gd)
        {
            this.content = content;
            this.gd = gd;

            scoreTimer = new Timer(300f, true);
            scoreFont = content.Load<SpriteFont>("Fonts/ScoreFont");
            scorePos = new Vector2(0, 0);
            scoreText = "Score: ";

        }

        public void LoadContent()
        {
            lifeManager = new LifeManager(content);
            Globals.LevelState = Level1.LEVEL;

            level1 = new Level1("Screens/Game/classroom_bg", content, lifeManager);
            level1.LoadContent();

            level2 = new Level2("Screens/Game/Stage2/bg", content, lifeManager);
            level2.LoadContent();

            score = 0;
        }

        int levelState;
        KeyboardState kb;
        public void Update(GameTime gameTime)
        {
            kb = Keyboard.GetState();
            switch(Globals.LevelState)
            {
                case Level1.LEVEL:
                    level1.Update(gameTime, kb);
                    break;
                case Level2.LEVEL:
                    if (level1.IsTransition())
                    {
                        level1.BgScroll(gameTime);
                    }

                    level2.Update(gameTime, kb);
                    break;
            }

            // Increase the score
            if(scoreTimer.IsFinished())
            {
                // Increase the score based on the level and amount of lives
                score += (lifeManager.Lives) * Globals.LevelState;
                // reset the score
                scoreTimer.ResetTimer(true);
            }

            lifeManager.Update(gameTime);
            levelState = Globals.LevelState;
            scoreTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            switch(Globals.LevelState)
            {
                case Level1.LEVEL:
                    level1.Draw(spriteBatch);
                    break;
                case Level2.LEVEL:
                    if(level1.IsTransition())
                    {
                        level1.BgDraw(spriteBatch);
                    }

                    level2.Draw(spriteBatch);
                    break;
            }

            lifeManager.Draw(spriteBatch);
            spriteBatch.DrawString(scoreFont, scoreText + score, scorePos, Color.Blue);
            spriteBatch.End();
        }

        public void PassName(string name)
        {
            playerName = name;
        }

        public static float BgScrollSpeed
        {
            get { return bgScrollSpeed; }
            set { bgScrollSpeed = value; }
        }
    }
}
