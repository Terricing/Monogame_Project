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
        private LinkedList offScreenQueue = new LinkedList();
        private LinkedList onScreenQueue = new LinkedList();

        // Store current node when iterating through list
        private Node curNode;

        private int numObs;

        private Timer spawnTimer;

        private int spawnChance;
        private int currentSpawnChance;

        public ObstacleManager(int numObs, Obstacle[] obs, int spawnChance, float spawnTime)
        {
            this.numObs = numObs;
            this.spawnChance = spawnChance;

            spawnTimer = new Timer(spawnTime, true);

            // Create a random queue of obstacles based on the level's obstacles 
            for (int i = 0; i < numObs; i++)
            {
                offScreenQueue.Enqueue(obs[Globals.Rand.Next(0, obs.Length)].Copy());
            }
        }

        public void Update(GameTime gameTime, Player player)
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

                curNode.Val.Update(gameTime);

                // check collision between onscreen obstacle and player
                if (Helper.Util.Intersects(curNode.Val.Image.ImgRect, player.Rect) && !player.IsHit)
                {
                    player.LoseLife();
                    //Console.WriteLine("True" + " " + player.IsHit);
                    break;
                }

                curNode = curNode.Next;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            curNode = onScreenQueue.Head;
            while (curNode != null)
            {
                curNode.Val.Draw(spriteBatch);
                curNode = curNode.Next;
            }
        }

        public bool IsLevelFinished ()
        {
            if (offScreenQueue.Head == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
