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
        public const int GAMESTATE = 5;
        private int sceneState;

        private StartScene startScene;
        private Scene1 scene1;

        MainGame mainGame; 

        public CutsceneManager(ContentManager content, MainGame mainGame, GraphicsDevice gd)
        {
            this.mainGame = mainGame;

            startScene = new StartScene(content);
            scene1 = new Scene1(content, gd);

            sceneState = StartScene.SCENESTATE;
        }

        public void Update (GameTime gameTime)
        {
            switch (sceneState)
            {
                case StartScene.SCENESTATE:
                    startScene.Update();
                    if (startScene.IsOver)
                    {
                        // switch state
                        scene1.LoadContent();
                        sceneState = Scene1.SCENESTATE;
                    }
                    break;
                case Scene1.SCENESTATE:
                    scene1.Update(gameTime);
                    if (scene1.IsOver)
                    {
                        mainGame.LoadContent(GetName());
                        Globals.GameState = MainGame.GAMESTATE;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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

        public string GetName()
        {
            return startScene.GetName();
        }

        public int SceneState
        {
            get { return sceneState; }
            set { sceneState = value; }
        }


    }
}
