// Author: Eilay Katsnelson
// File Name: Level3.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child level class for the fourth level

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
        // set level state
        public const int LEVEL = 4;

        // store level's obstacles
        private Obstacle[] obstacles = new Obstacle[3];
        private ObstacleManager obsMan;

        /// <summary>
        /// Create second level 
        /// </summary>
        /// <param name="bgPath">loaction of background</param>
        /// <param name="content">load content</param>
        /// <param name="lifeManager">keep track of lives</param>
        /// <param name="gd"></param>
        public Level4(ContentManager content, LifeManager lifeManager) : base("Screens/Game/Stage4/bg", content, lifeManager)
        {
            // Assign level's obstacles
            obstacles[0] = new Obstacle(content, "Screens/Game/Stage4/boulder", 0.5f);
            obstacles[1] = new Obstacle(content, "Screens/Game/Stage4/fire", 0.5f);
            obstacles[2] = new Obstacle(content, "Screens/Game/Stage4/rock", 0.5f);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            obsMan = new ObstacleManager(10, obstacles, 2, 500, true);
        }

        /// <summary>
        /// Update level
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        /// <param name="kb">keep track of keypresses</param>
        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);

            // if all obstacles are beat, generate more
            if (obsMan.IsLevelFinished())
            {
                obsMan.CreateObstacles(obstacles);
            }

            obsMan.Update(gameTime, player);
        }

        /// <summary>
        /// Draw level
        /// </summary>
        /// <param name="spriteBatch">controller for screen's canvas</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw background and obstacles
            base.Draw(spriteBatch);
            obsMan.Draw(spriteBatch);
        }
    }
}
