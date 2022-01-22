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

        private const int GAME = 0;
        private const int SCENE = 1;

        private int state;
        private int sceneState;

        // Store interfaces
        private ContentManager content;
        private GraphicsDevice gd;

        // store levels
        private Level1 level1;
        private Level2 level2;
        private Level3 level3;
        private Level4 level4;

        // store scenes
        Scene1 scene1;
        Scene2 scene2;
        Scene3 scene3;
        Scene4 scene4;

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

            // create scenes
            scene1 = new Scene1(content, gd);
            scene2 = new Scene2(content, gd);
            scene3 = new Scene3(content, gd);
            scene4 = new Scene4(content, gd);

        }

        public void LoadContent(string name)
        {
            playerName = name;

            lifeManager = new LifeManager(content);
            Globals.LevelState = Level1.LEVEL;

            level1 = new Level1("Screens/Game/classroom_bg", content, lifeManager, gd);
            level1.LoadContent();

            level2 = new Level2("Screens/Game/Stage2/bg", content, lifeManager);
            level2.LoadContent();

            level3 = new Level3(content, lifeManager);
            level3.LoadContent();

            level4 = new Level4(content, lifeManager);
            level4.LoadContent();

            // load scenes
            scene1.LoadContent(name);
            scene2.LoadContent();
            scene3.LoadContent();
            scene4.LoadContent();

            state = SCENE;
            sceneState = Scene1.SCENESTATE;

            //state = GAME;
            //Globals.LevelState = Level3.LEVEL;

            //state = GAME;
            //Globals.LevelState = Level4.LEVEL;

            score = 0;
        }

        int levelState;
        KeyboardState kb;
        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GAME:
                    kb = Keyboard.GetState();
                    switch (Globals.LevelState)
                    {
                        case Level1.LEVEL:
                            level1.Update(gameTime, kb);

                            if (level1.IsLevelFinished)
                            {
                                state = SCENE;
                                sceneState = Scene2.SCENESTATE;
                            }

                            break;
                        case Level2.LEVEL:
                            if (level2.IsLevelFinished)
                            {
                                state = SCENE;
                                sceneState = Scene3.SCENESTATE;
                            }

                            level2.Update(gameTime, kb);
                            break;
                        case Level3.LEVEL:
                            if (level3.IsOver)
                            {
                                state = SCENE;
                                sceneState = Scene4.SCENESTATE;
                            }

                            level3.Update(gameTime, kb);
                            break;
                        case Level4.LEVEL:
                            level4.Update(gameTime, kb);

                            break;
                    }

                    // Increase the score
                    if (scoreTimer.IsFinished())
                    {
                        // Increase the score based on the level and amount of lives
                        score += (lifeManager.Lives) * Globals.LevelState;
                        // reset the score
                        scoreTimer.ResetTimer(true);
                    }

                    lifeManager.Update(gameTime);
                    levelState = Globals.LevelState;
                    scoreTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    break;
                case SCENE:
                    switch (sceneState)
                    {
                        case Scene1.SCENESTATE:
                            scene1.Update(gameTime);
                            if (scene1.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level1.LEVEL;
                            }

                            break;
                        case Scene2.SCENESTATE:
                            scene2.Update(gameTime);
                            if (scene2.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level2.LEVEL;
                            }

                            break;
                        case Scene3.SCENESTATE:
                            scene3.Update(gameTime);
                            if (scene3.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level3.LEVEL;
                            }

                            break;
                        case Scene4.SCENESTATE:
                            scene4.Update(gameTime);
                            if (scene4.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level4.LEVEL;
                            }


                            break;
                    }
                    break;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            switch (state)
            {
                case GAME:
                    switch (Globals.LevelState)
                    {
                        case Level1.LEVEL:
                            level1.Draw(spriteBatch);
                            break;
                        case Level2.LEVEL:
                            //if(level1.IsTransition())
                            //{
                            //    level1.BgDraw(spriteBatch);
                            //}    
                            level2.Draw(spriteBatch);
                            break;
                        case Level3.LEVEL:
                            level3.Draw(spriteBatch);
                            break;
                        case Level4.LEVEL:
                            level4.Draw(spriteBatch);
                            break;
                    }

                    lifeManager.Draw(spriteBatch);
                    spriteBatch.DrawString(scoreFont, scoreText + score, scorePos, Color.Blue);

                    break;
                case SCENE:
                    switch(sceneState)
                    {
                        case Scene1.SCENESTATE:
                            scene1.Draw(spriteBatch);
                            break;
                        case Scene2.SCENESTATE:
                            scene2.Draw(spriteBatch);
                            break;
                        case Scene3.SCENESTATE:
                            scene3.Draw(spriteBatch);
                            break;
                        case Scene4.SCENESTATE:
                            scene4.Draw(spriteBatch);
                            break;
                    }

                    break;
            }


            spriteBatch.End();
        }

        public void PassName(string name)
        {
            playerName = name;
        }

    }
}
