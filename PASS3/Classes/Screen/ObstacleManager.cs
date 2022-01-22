// Author: Eilay Katsnelson
// File Name: ObstacleManager.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Manages each level's obstacles
// This class is used for controlling the obstacles, allows for setting difficulty
// To increase difficuly: decrease timer and/or decrease spawn chance

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
using Animation2D;
using Helper;


namespace PASS3.Classes.Screen
{
    class ObstacleManager
    {
        // Create list for offscreen obstacles and onscreen obstacles
        private LinkedList offScreenQueue = new LinkedList();
        private LinkedList onScreenQueue = new LinkedList();

        // Store current node when iterating through list
        private Node curNode;

        // Store timer for spawning of obstacles
        private Timer spawnTimer;

        // Store changes of obstacles spawning
        private int spawnChance;
        private int currentSpawnChance;

        // Store whether level is over
        private bool isOver;

        // Timer for when to stop the level (to give a little bit of feedback after beating all obstacles)
        private Timer switchScene;

        // Store whether the obstaclemanager is for unlimited obstacles
        private bool isUnlimited;

        /// <summary>
        /// Create obstacle manager
        /// </summary>
        /// <param name="numObs">number of obstacles to create</param>
        /// <param name="obs">array of base obstacles to use</param>
        /// <param name="spawnChance">how likely is the obstacle to spawn</param>
        /// <param name="spawnTime">how long before trying to spawn an obstacle</param>
        /// <param name="isUnlimited">is the manager for an unlimited amount of obstacles</param>
        public ObstacleManager(int numObs, Obstacle[] obs, int spawnChance, float spawnTime, bool isUnlimited = false)
        {
            this.spawnChance = spawnChance;

            // create spawnTimer for obstacle spawning
            spawnTimer = new Timer(spawnTime, true);

            // Create a random queue of obstacles based on the level's obstacles 
            for (int i = 0; i < numObs; i++)
            {
                offScreenQueue.Enqueue(obs[Globals.Rand.Next(0, obs.Length)].Copy());
            }

            // Create feedback timer for when the level is over
            switchScene = new Timer(2500, false);

            // give inital value of false
            isOver = false;
            
            // save whether there are unlimited obstacles
            this.isUnlimited = isUnlimited;
        }

        /// <summary>
        /// Update obstacles
        /// </summary>
        /// <param name="gameTime">time between updates</param>
        /// <param name="player">player</param>
        public void Update(GameTime gameTime, Player player)
        {
            // update feedback timer
            switchScene.Update(gameTime.ElapsedGameTime.Milliseconds);

            // if level is not over
            if (!isOver)
            {
                // update timer
                spawnTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);

                // Check if timer is complete
                if (spawnTimer.IsFinished())
                {
                    // Algorithm to determine whether to spawn the next obstacle
                    currentSpawnChance -= Globals.Rand.Next(0, 2);

                    if (currentSpawnChance <= 0)
                    {
                        onScreenQueue.Enqueue(offScreenQueue.Dequeue());

                        // reset spawnChance
                        currentSpawnChance = spawnChance;
                    }

                    // reset timer
                    spawnTimer.ResetTimer(true);
                }
            }

            // Update onscreen obstacles
            curNode = onScreenQueue.Head;
            while (curNode != null)
            {
                // Check if next obstacle is offscreen, if so remove it
                if (curNode.Next != null && curNode.Next.Val.IsOffScreen())
                {
                    curNode.Next = curNode.Next.Next;
                    onScreenQueue.Count--;
                }

                // update obstacle
                curNode.Val.Update(gameTime);

                // check collision between onscreen obstacle and player
                if (Helper.Util.Intersects(curNode.Val.Image.ImgRect, player.Rect) && !player.IsHit)
                {
                    player.LoseLife();
                    break;
                }

                curNode = curNode.Next;
            }
        }

        /// <summary>
        /// Draw obstacles
        /// </summary>
        /// <param name="spriteBatch">conroller for screen's canvas</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Cycle through onsreen obstacles, draw each one
            curNode = onScreenQueue.Head;
            while (curNode != null)
            {
                curNode.Val.Draw(spriteBatch);
                curNode = curNode.Next;
            }
        }

        /// <summary>
        /// Check if level is finished
        /// </summary>
        /// <returns>bool based on whether all obstacles have finished spawning and are offscreen</returns>
        public bool IsLevelFinished()
        {
            // if there are no more obstacles to spawn
            if (offScreenQueue.Head == null)
            {
                // if there are a limited amount of obstacles, stop trying to spawn new ones and wait
                // until all obstacles are offscreen, then start feedback timer
                if (!isUnlimited)
                {
                    isOver = true;
                    
                    if (onScreenQueue.Count == 1)
                    {
                        if (!switchScene.IsActive())
                        {
                            switchScene.Activate();
                        }

                        if (switchScene.IsFinished())
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    // if there are unlimited obstacles, return true immidiately so more obstacles could be loaded
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Creates more obstacles when user beats them all
        /// </summary>
        public void CreateObstacles(Obstacle[] obs)
        {
            // Create a random queue of obstacles based on the level's obstacles 
            for (int i = 0; i < 10; i++)
            {
                offScreenQueue.Enqueue(obs[Globals.Rand.Next(0, obs.Length)].Copy());
            }
        }
    }
}
