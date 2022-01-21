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

        public CutsceneManager(ContentManager content)
        {
            startScene = new StartScene(content);

            sceneState = StartScene.SCENESTATE;

        }

        public void Update ()
        {
            switch (sceneState)
            {
                case StartScene.SCENESTATE:
                    startScene.Update();
                    if (startScene.IsOver)
                    {
                        // switch state
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
