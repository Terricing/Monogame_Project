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
using MonoGame.Extended;
using Animation2D;
using Helper;
using PASS3.Classes.Screen;
using PASS3.Classes.Components;
using PASS3.Classes;

namespace PASS3
{
    class Player
    {
        private Animation player;
        private int lane;

        // control moving lane to lane
        private float maxSpeed = 1500f;
        private float playerSpeed;
        private int dir;
        private float actualPos;

        private int curLanePos;

        private bool drawCharacter;
        private bool isHit;

        private LifeManager lifeManager;

        public Player(LifeManager lifeManager)
        {
            // set default lane
            lane = 1;
            isHit = false;

            drawCharacter = true;

            this.lifeManager = lifeManager;
        }

        public void LoadContent(ContentManager content)
        {
            player = new Animation(content.Load<Texture2D>("Screens/Game/running_player"), 2, 1, 2, 0, Animation.NO_IDLE, Animation.ANIMATE_FOREVER, 6, new Vector2(Globals.LanePos[1]), 0.6f, true);
            player.destRec.X -= player.destRec.Width / 2;
            actualPos = player.destRec.X;
        }

        KeyboardState prevKb;
        public void Update(GameTime gameTime, KeyboardState kb)
        {
            player.Update(gameTime);

            // change lane on request
            if (kb.IsKeyDown(Keys.Left) && !prevKb.IsKeyDown(Keys.Left) && lane != 0 && dir == 0)
            {
                lane--;
                curLanePos = Globals.LanePos[lane] - player.destRec.Width / 2;
                dir = -1;
            }
            else if (kb.IsKeyDown(Keys.Right) && !prevKb.IsKeyDown(Keys.Right) && lane != 2 && dir == 0)
            {
                lane++;
                curLanePos = Globals.LanePos[lane] - player.destRec.Width / 2;
                dir = 1;
            }

            // stop player when he reaches lane
            if (Math.Abs(actualPos - curLanePos) < 10 && dir != 0)
            {
                dir = 0;
            }

            // move player
            playerSpeed = dir * (maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            actualPos += playerSpeed;
            player.destRec.X = (int)actualPos;

            if (lifeManager.InvincibilityTimer.IsActive())
            {
                drawCharacter = !drawCharacter;
            }
            else if (lifeManager.InvincibilityTimer.IsFinished())
            {
                drawCharacter = true;
                isHit = false;
                lifeManager.InvincibilityTimer.Deactivate();
            }

            // store kb state
            prevKb = kb;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawCharacter)
            {
                player.Draw(spriteBatch, Color.White, Animation.FLIP_NONE);
            }

            spriteBatch.DrawRectangle(player.destRec, Color.Orange);
        }

        public Rectangle Rect
        {
            get { return player.destRec; }
        }

        public void LoseLife()
        {
            isHit = true;
            // if lives are less than 0, game over
            if (lifeManager.Lives <= 0)
            {
                Globals.GameState = GameOver.GAMESTATE;
            }

            // if invincibility timer is inactive, decrement lives and start timer
            if (lifeManager.InvincibilityTimer.IsInactive())
            {
                isHit = true;
                lifeManager.LoseLife();
            }

        }

        public bool IsHit
        {
            get { return isHit; }
        }

        public bool DrawCharacer
        {
            get { return drawCharacter; }
            set { drawCharacter = value; }
        }

        public int Lane
        {
            get { return lane; }
        }
    }
}
