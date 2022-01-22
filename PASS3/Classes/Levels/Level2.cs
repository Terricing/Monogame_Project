using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PASS3.Classes.Components;
using PASS3.Classes.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Levels
{
    class Level2 : Level
    {
        public const int LEVEL = 2;

        private Obstacle[] obstacles = new Obstacle[2];
        private ObstacleManager obsMan;

        public Level2(String bgPath, ContentManager content, LifeManager lifeManager) : base (bgPath, content, lifeManager)
        {
            obstacles[0] = new Obstacle(content, "Screens/Game/Stage2/coneObs", 0.5f);
            obstacles[1] = new Obstacle(content, "Screens/Game/Stage2/poleObs", 0.7f);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            obsMan = new ObstacleManager(20, obstacles, 3, 300);
        }

        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);
            if (obsMan.IsLevelFinished())
            {
                isLevelFinished = true;
            }
            obsMan.Update(gameTime, player);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            obsMan.Draw(spriteBatch);
        }
    }
}
