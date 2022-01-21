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
        // set to menu on startup
        private static int gameState = Menu.GAMESTATE;

        // keep track of current level
        private static int levelState;

        private static float bgScrollSpeed = 400f;

        // store lane positions
        private static int[] lanePos = new int[] { (int)(Globals.GAME_WIDTH / 4f), (int)(Globals.GAME_WIDTH / 2.35f), (int)(Globals.GAME_WIDTH / 1.6f) };

        // Get random object
        public static Random Rand
        {
            get { return rng; }
        }

        public static int GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public static int LevelState
        {
            get { return levelState; }
            set { levelState = value; }
        }

        public static int[] LanePos
        {
            get { return lanePos; }
        }

        public static float BgScrollSpeed
        {
            get { return bgScrollSpeed; }
        }
    }
}
