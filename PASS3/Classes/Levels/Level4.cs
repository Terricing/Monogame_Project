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
    class Level4 : Level
    {
        public const int LEVEL = 5;

        private Obstacle[] obstacles = new Obstacle[3];
        private ObstacleManager obsMan;

        public Level4(ContentManager content, LifeManager lifeManager) : base("Screens/Game/Stage4/bg", content, lifeManager)
        {
            obstacles[0] = new Obstacle(content, "Screens/Game/Stage4/boulder", 0.5f);
            obstacles[1] = new Obstacle(content, "Screens/Game/Stage4/fire", 0.5f);
            obstacles[2] = new Obstacle(content, "Screens/Game/Stage4/rock", 0.5f);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            obsMan = new ObstacleManager(10, obstacles, 2, 500, true);
        }

        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);

            // if all obstacles are beat
            if (obsMan.IsLevelFinished())
            {
                obsMan.CreateObstacles(obstacles);
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
