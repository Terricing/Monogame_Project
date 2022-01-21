using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen.Cutscenes
{
    class CutsceneManager
    {
        // store gamestate for cutscene manager
        public const int GAMESTATE = 5;

        // store currently playing scene
        private int sceneState;

        // store scenes
        private StartScene startScene;
        private Scene1 scene1;

        // store mainGame to allow for smooth transition from cutscene to game
        private MainGame mainGame; 

        public CutsceneManager(ContentManager content, MainGame mainGame, GraphicsDevice gd)
        {
            // store main game
            this.mainGame = mainGame;

            // create scenes
            startScene = new StartScene(content);
            scene1 = new Scene1(content, gd);

            // inital sceneState 
            sceneState = StartScene.SCENESTATE;
        }

        /// <summary>
        /// Reset cutscene manager's state and individual cutscenes
        /// </summary>
        public void LoadContent()
        {
            startScene.LoadContent();
            scene1.LoadContent();
        }

        /// <summary>
        /// Update cutscene manager
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update (GameTime gameTime)
        {
            // Update currently playing cutscene

            switch (sceneState)
            {
                case StartScene.SCENESTATE:
                    startScene.Update();
                    if (startScene.IsOver)
                    {
                        // switch state if over
                        scene1.LoadContent();
                        sceneState = Scene1.SCENESTATE;
                    }
                    break;
                case Scene1.SCENESTATE:
                    scene1.Update(gameTime);
                    if (scene1.IsOver)
                    {
                        // switch state if over
                        mainGame.LoadContent(GetName());
                        Globals.GameState = MainGame.GAMESTATE;
                    }
                    break;
            }
        }

        /// <summary>
        /// Draw currently playing cutscene
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw currently playing cutscene
            switch (sceneState)
            {
                case StartScene.SCENESTATE:
                    startScene.Draw(spriteBatch);
                    break;

                case Scene1.SCENESTATE:
                    scene1.Draw(spriteBatch);
                    break;
            }
        }

        /// <summary>
        /// Obtain name
        /// </summary>
        /// <returns>string representing user's chosen name</returns>
        public string GetName()
        {
            return startScene.GetName();
        }

        // Property for sceneState
        public int SceneState
        {
            get { return sceneState; }
            set { sceneState = value; }
        }


    }
}
