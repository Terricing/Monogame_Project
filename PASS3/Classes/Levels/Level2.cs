// Author: Eilay Katsnelson
// File Name: Level2.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child level class for the second level

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
        // set level state
        public const int LEVEL = 2;

        // Store level's obstacles
        private Obstacle[] obstacles = new Obstacle[2];
        private ObstacleManager obsMan;

        /// <summary>
        /// Create second level 
        /// </summary>
        /// <param name="bgPath">loaction of background</param>
        /// <param name="content">load content</param>
        /// <param name="lifeManager">keep track of lives</param>
        /// <param name="gd"></param>
        public Level2(String bgPath, ContentManager content, LifeManager lifeManager) : base (bgPath, content, lifeManager)
        {
            // Assign level's obstacles
            obstacles[0] = new Obstacle(content, "Screens/Game/Stage2/coneObs", 0.5f);
            obstacles[1] = new Obstacle(content, "Screens/Game/Stage2/poleObs", 0.7f);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            // Create obstacle manager
            obsMan = new ObstacleManager(20, obstacles, 3, 300);
        }

        /// <summary>
        /// Update level
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        /// <param name="kb">keep track of keypresses</param>
        public override void Update(GameTime gameTime, KeyboardState kb)
        {
            base.Update(gameTime, kb);

            // Check whether level is finished
            if (obsMan.IsLevelFinished())
            {
                isLevelFinished = true;
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
