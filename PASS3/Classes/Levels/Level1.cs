// Author: Eilay Katsnelson
// File Name: Level1.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A child level class for the first level

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
using PASS3.Classes.Screen.Cutscenes;

namespace PASS3.Classes.Levels
{
    class Level1 : Level
    {
        // set level state
        public const int LEVEL = 1;

        // Store level's obstacles
        private Obstacle[] obstacles = new Obstacle[4];
        private ObstacleManager obsMan;

        /// <summary>
        /// Create first level 
        /// </summary>
        /// <param name="bgPath">loaction of background</param>
        /// <param name="content">load content</param>
        /// <param name="lifeManager">keep track of lives</param>
        /// <param name="gd"></param>
        public Level1(string bgPath, ContentManager content, LifeManager lifeManager, GraphicsDevice gd) : base(bgPath, content, lifeManager)
        {
            // Assign level's obstacles
            obstacles[0] = new Obstacle(content, "Screens/Game/GarbageObstacle", 0.15f);
            obstacles[1] = new Obstacle(content, "Screens/Game/BackpackObstacle", 0.3f);
            obstacles[2] = new Obstacle(content, "Screens/Game/BananaObs", 0.3f);
            obstacles[3] = new Obstacle(content, "Screens/Game/waterObs", 0.3f);
        }

        /// <summary>
        /// Reset values
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            // Create obstacle manager
            obsMan = new ObstacleManager(15, obstacles, 3, 500);
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
