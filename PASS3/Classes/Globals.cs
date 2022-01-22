// Author: Eilay Katsnelson
// File Name: Globals.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: Static class to keep track of objects and variables that are required across
// a wide range of classes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes
{
    static class Globals
    {
        // Game resolution:
        public const int GAME_WIDTH = 1024;
        public const int GAME_HEIGHT = 768;

        // create Random object
        private static Random rng = new Random();

        // keep track of screen
        private static int gameState = Menu.GAMESTATE;

        // keep track of current level
        private static int levelState;

        // how fast screens and obstacles should scroll
        private static float bgScrollSpeed = 400f;

        // keep track of name and score
        private static string playerName;
        private static int score;

        // store lane positions
        private static int[] lanePos = new int[] { (int)(Globals.GAME_WIDTH / 3.3f), (int)(Globals.GAME_WIDTH / 2f), (int)(Globals.GAME_WIDTH / 1.45f) };

        /// <summary>
        /// Accessor for random object
        /// </summary>
        public static Random Rand
        {
            get { return rng; }
        }

        /// <summary>
        /// Property for gamestate
        /// </summary>
        public static int GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        /// <summary>
        /// Property for levelstate
        /// </summary>
        public static int LevelState
        {
            get { return levelState; }
            set { levelState = value; }
        }
        /// <summary>
        /// Property for lane positions
        /// </summary>
        public static int[] LanePos
        {
            get { return lanePos; }
        }

        /// <summary>
        /// Property for backgrond scroll speed
        /// </summary>
        public static float BgScrollSpeed
        {
            get { return bgScrollSpeed; }
        }

        /// <summary>
        /// Property for player's name
        /// </summary>
        public static string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        /// <summary>
        /// Property for score
        /// </summary>
        public static int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
