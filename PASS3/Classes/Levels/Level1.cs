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
using MonoGame.Extended;
using Animation2D;
using PASS3.Classes.Components;

namespace PASS3.Classes.Levels
{
    class Level1 : Level
    {
        public const int LEVEL = 1;

        private Obstacle[] obstacles = new Obstacle[2];
        private ObstacleManager obsMan;

        public Level1(string bgPath, ContentManager content, LifeManager lifeManager) : base(bgPath, content, lifeManager)
        {
            // Create level's obstacle manager
            obstacles[0] = new Obstacle();
            obstacles[0].LoadContent(content, "Screens/Game/GarbageObstacle", 0.15f);
            obstacles[1] = new Obstacle();
            obstacles[1].LoadContent(content, "Screens/Game/BackpackObstacle", 0.3f);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            obsMan = new ObstacleManager(15, obstacles, 3, 500);
        }

        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);

            if (obsMan.IsLevelFinished())
            {
                // finish scrolling screen
                isLevelFinished = true;
                Globals.LevelState = Level2.LEVEL;
                //MainGame.BgScrollSpeed *= 1.5f;
                return;
            }

            obsMan.Update(gameTime, player);
    
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            obsMan.Draw(spriteBatch);

        }

        protected override void OnLevelOver()
        {
            Globals.LevelState = Level2.LEVEL;
        }
    }
}
