// Author: Eilay Katsnelson
// File Name: MainGame.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A class for the main game loop

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
        // create gamestate
        public const int GAMESTATE = 1;

        // create identifiers for different main game states
        private const int GAME = 0;
        private const int SCENE = 1;

        // keep track of states
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

        // Score
        private Timer scoreTimer;
        private SpriteFont scoreFont;
        private Vector2 scorePos;
        private string scoreText;

        // Store info about lvies
        private LifeManager lifeManager;

        // keep track of keypresses
        private KeyboardState kb;

        /// <summary>
        /// Create MainGame
        /// </summary>
        /// <param name="content">load content</param>
        /// <param name="gd"></param>
        public MainGame(ContentManager content, GraphicsDevice gd)
        {
            // Assign interfaces
            this.content = content;
            this.gd = gd;

            // Set initial score values
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

        /// <summary>
        /// Values and commands to be run every time the game restarts
        /// </summary>
        public void LoadContent()
        {
            // Create life manager
            lifeManager = new LifeManager(content);
            Globals.LevelState = Level1.LEVEL;

            // Create levels
            level1 = new Level1("Screens/Game/classroom_bg", content, lifeManager, gd);
            level1.LoadContent();

            level2 = new Level2("Screens/Game/Stage2/bg", content, lifeManager);
            level2.LoadContent();

            level3 = new Level3(content, lifeManager);
            level3.LoadContent();

            level4 = new Level4(content, lifeManager);
            level4.LoadContent();

            // load scenes
            scene1.LoadContent();
            scene2.LoadContent();
            scene3.LoadContent();
            scene4.LoadContent();

            // Set inital mainGame state
            state = SCENE;
            // set initial scene
            sceneState = Scene1.SCENESTATE;

            // Set inital score
            Globals.Score = 0;
        }

        /// <summary>
        /// Update game
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        public void Update(GameTime gameTime)
        {
            // Update game or cutscene, depending on what is currently being shown
            switch (state)
            {
                case GAME:
                    // Get keyboard state
                    kb = Keyboard.GetState();

                    // Update level that is being shown
                    switch (Globals.LevelState)
                    {
                        case Level1.LEVEL:
                            // When level finished, load next cutscene
                            if (level1.IsLevelFinished)
                            {
                                state = SCENE;
                                sceneState = Scene2.SCENESTATE;
                            }

                            // Update level 1
                            level1.Update(gameTime, kb);
                            break;

                        case Level2.LEVEL:
                            // When level finished, load next cutscene
                            if (level2.IsLevelFinished)
                            {
                                state = SCENE;
                                sceneState = Scene3.SCENESTATE;
                            }

                            // update level2
                            level2.Update(gameTime, kb);
                            break;

                        case Level3.LEVEL:
                            // When level finished, load next cutscene
                            if (level3.IsOver)
                            {
                                state = SCENE;
                                sceneState = Scene4.SCENESTATE;
                            }

                            // Update level 3
                            level3.Update(gameTime, kb);
                            break;

                        case Level4.LEVEL:
                            // Update level 4
                            level4.Update(gameTime, kb);
                            break;
                    }

                    // If score's timer is finished, increase the score
                    if (scoreTimer.IsFinished())
                    {
                        // Increase the score based on the level and amount of lives
                        Globals.Score += (lifeManager.Lives) * Globals.LevelState;
                        // reset the score timer
                        scoreTimer.ResetTimer(true);
                    }

                    // Update life keeper
                    lifeManager.Update(gameTime);

                    // update score's timer
                    scoreTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
                    break;

                case SCENE:
                    // Perform updates dpeneding on which scene is currently being displayed
                    switch (sceneState)
                    {
                        case Scene1.SCENESTATE:
                            // update scene1
                            scene1.Update(gameTime);

                            // If scene over, load next level
                            if (scene1.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level1.LEVEL;
                            }
                            break;

                        case Scene2.SCENESTATE:
                            // Update scene2
                            scene2.Update(gameTime);

                            // If scene over, load next level
                            if (scene2.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level2.LEVEL;
                            }
                            break;

                        case Scene3.SCENESTATE:
                            // Update scene3
                            scene3.Update(gameTime);

                            // If scene over, load next level
                            if (scene3.IsOver)
                            {
                                state = GAME;
                                Globals.LevelState = Level3.LEVEL;
                            }
                            break;

                        case Scene4.SCENESTATE:
                            // update scene4
                            scene4.Update(gameTime);

                            // If scene over, load next level
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

        /// <summary>
        /// Draw game
        /// </summary>
        /// <param name="spriteBatch">control screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw screen depending on level or cutscene being displayed
            switch (state)
            {
                case GAME:
                    // Draw screen depending on level being displayed
                    switch (Globals.LevelState)
                    {
                        case Level1.LEVEL:
                            level1.Draw(spriteBatch);
                            break;
                        case Level2.LEVEL:
                            level2.Draw(spriteBatch);
                            break;
                        case Level3.LEVEL:
                            level3.Draw(spriteBatch);
                            break;
                        case Level4.LEVEL:
                            level4.Draw(spriteBatch);
                            break;
                    }

                    // Draw lives and score
                    lifeManager.Draw(spriteBatch);
                    spriteBatch.DrawString(scoreFont, scoreText + Globals.Score, scorePos, Color.MediumSeaGreen);
                    break;

                case SCENE:
                    switch(sceneState)
                    {
                        // Draw screen based on scene being displayed
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

    }
}
