// Class for the menu screen, this is the first screen the user sees

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
using PASS3.Classes;
using PASS3.Classes.Screen.Cutscenes;
using PASS3.Classes.Screen;

namespace PASS3
{
    //class Menu
    //{
    //    public const int GAMESTATE = 0;

    //    private Button playBt;

    //    private ContentManager content;

    //    // store background img
    //    private Img bg;
    //    private Animation laneAnim;
    //    private Texture2D lane;

    //    // Create mr Lane's eyes and their bounding rectangles
    //    private Img leftEye;
    //    private Rectangle leftRect;
        
    //    private Img rightEye;
    //    private Rectangle rightRect;

    //    private Vector2 eyeSpeed = new Vector2(0, 0);
    //    private Vector2 leftEyePos;
    //    private Vector2 rightEyePos;

    //    private int dirLeft;
    //    private int dirRight;

    //    private Point leftCenterPos;
    //    private Point rightCenterPos;

    //    private int maxSpeed = 10;

    //    public Menu(ContentManager content)
    //    {
    //        // Load background and place it in the centre of the screen
    //        bg = new Img(content.Load<Texture2D>("Screens/Menu/lane"));
    //        bg.LoadContent((int)((Globals.GAME_WIDTH / 2) - (bg.ImgRect.Width / 2)), 0);
    //        bg.X = (int)((Globals.GAME_WIDTH / 2) - (bg.ImgRect.Width / 2));

    //        lane = content.Load<Texture2D>("Screens/Menu/laneAnim");
    //        laneAnim = new Animation(lane, 6, 1, 6, 0, 0, Animation.ANIMATE_ONCE, 10, new Vector2(Globals.GAME_WIDTH / 2 - lane.Width / 7, 0), 0.438f, false);
    //        laneAnim.destRec.X = Globals.GAME_WIDTH / 2 - laneAnim.destRec.Width / 2;

    //        // Load eyes and place them in correct center
    //        leftEye = new Img(content.Load<Texture2D>("Screens/Menu/neweye"));
    //        leftEye.LoadContent(0, 0);
    //        leftCenterPos = new Point(bg.ImgRect.X + bg.ImgRect.Width / 3, (int)(bg.ImgRect.Y + bg.ImgRect.Height / 1.95));
    //        leftEye.ImgRect = new Rectangle(leftCenterPos, new Point(leftEye.Image.Width, leftEye.Image.Height));
    //        leftRect = new Rectangle(leftEye.ImgRect.X - leftEye.ImgRect.Width / 2, leftEye.ImgRect.Y, leftEye.ImgRect.Width * 2, leftEye.ImgRect.Height);

    //        rightEye = new Img(leftEye.Image);
    //        rightEye.LoadContent(rightCenterPos.X, rightCenterPos.Y);
    //        rightRect = new Rectangle(rightEye.ImgRect.X - rightEye.ImgRect.Width / 2, rightEye.ImgRect.Y, leftEye.ImgRect.Width * 2, leftEye.ImgRect.Height); //both rectangles need to be the same size
    //        rightCenterPos = new Point((int)(bg.ImgRect.X + bg.ImgRect.Width / 1.45), (int)(bg.ImgRect.Y + bg.ImgRect.Height / 2.02));
            

    //        leftEyePos.X = leftEye.ImgRect.X;
    //        rightEyePos.X = rightEye.ImgRect.X;
    //        leftEyePos.Y = leftEye.ImgRect.Y;
    //        rightEyePos.Y = rightEye.ImgRect.Y;

    //        playBt = new Button(content, "Screens/Menu/playBtn/playBtn", "Screens/Menu/playBtn/hPlayBtn", Globals.GAME_WIDTH / 5, Globals.GAME_HEIGHT / 2);
    //        playBt.X = Globals.GAME_WIDTH / 2 - playBt.BtRect.Width / 2;
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        spriteBatch.Begin();
    //        //bg.Draw(spriteBatch);
    //        laneAnim.Draw(spriteBatch, Color.White, Animation.FLIP_NONE);
    //        //leftEye.Draw(spriteBatch);
    //        //rightEye.Draw(spriteBatch);

    //        spriteBatch.DrawRectangle(leftRect, Color.Orange);
    //        spriteBatch.DrawRectangle(rightRect, Color.Orange);

    //        playBt.Draw(spriteBatch);
    //        spriteBatch.End();
    //    }

    //    MouseState mouse;
    //    MouseState prevMouse;
    
    //    public void Update(GameTime gameTime)
    //    {
    //        prevMouse = mouse;
    //        mouse = Mouse.GetState();

    //        // if clicking occurs
    //        if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
    //        {
    //            if (playBt.CheckCollision(mouse.Position))
    //            {
    //                Globals.GameState = StartScene.GAMESTATE; 
    //            }
    //        }

    //        // if collision occurs between mouse and play button
    //        if (playBt.CheckCollision(mouse.Position))
    //        {
    //            // highlight play button
    //            playBt.IsSelected = true;

    //            // animate mr lane if not already animating
    //            if (!laneAnim.isAnimating)
    //            {
    //                laneAnim.isAnimating = true;
    //            }
    //        }
    //        else
    //        {
    //            playBt.IsSelected = false;
    //        }

    //        // Make mr lane's follow the mouse
    //        if (mouse.Position.X < leftEye.ImgRect.X)
    //        {
    //            dirLeft = -1;
    //            dirRight = -1;
    //        }
    //        else if (mouse.Position.X > rightEye.ImgRect.X + rightEye.ImgRect.Width)
    //        {
    //            dirLeft = 1;
    //            dirRight = 1;
    //        }
    //        else
    //        {
    //            if (Math.Abs(leftEye.ImgRect.X - leftCenterPos.X) < 2)
    //            {
    //                dirLeft = 0;
    //                dirRight = 0;
    //            }
    //            else
    //            {
    //                dirLeft = 1;
    //                dirRight = -1;
    //            }


    //            //if (Math.Abs(rightEye.ImgRect.X - rightCenterPos.X) < 2)
    //            //{
    //            //    dirRight = 0;
    //            //}
    //            //else
    //            //{
    //            //    dirRight = -1;
    //            //}
    //        }

    //        // move eyes
    //        eyeSpeed.X = dirLeft * (maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
    //        eyeSpeed.Y = dirRight * (maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
    //        //Console.WriteLine(leftEyePos.X);
    //        ////eyeSpeed.Y = dir.Y * (maxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
    //        leftEyePos.X = MathHelper.Clamp(leftEyePos.X + eyeSpeed.X, leftRect.X, leftRect.X + leftRect.Width - leftEye.ImgRect.Width);
    //        //leftEyePos.Y += eyeSpeed.Y;
    //        rightEyePos.X = MathHelper.Clamp(rightEyePos.X + eyeSpeed.Y, rightRect.X, rightRect.X + rightRect.Width - rightEye.ImgRect.Width);
    //        //rightEyePos.Y += eyeSpeed.Y;

    //        leftEye.X = (int)leftEyePos.X;
    //        rightEye.X = (int)rightEyePos.X;

    //        // ANIMATIONS
    //        laneAnim.Update(gameTime);
    //    }

    //    // is this allowed?
    //    //public static int GAMESTATE
    //    //{
    //    //    get { return MENU; }
    //    //}
    //}
   
    class Menu
    {
        public const int GAMESTATE = 0;
        private Button playBtn;
        private Button scoreBtn;
        private Button exitBtn;

        private Img bg;

        private MouseState mouseState;
        private MouseState prevMouseState;

        // game container, used for exit functionality
        private Game1 game;

        public Menu (ContentManager content, Game1 game)
        {
            this.game = game;

            bg = new Img(content.Load<Texture2D>("Screens/Menu/bg"));
            bg.LoadContent(0, 0);

            playBtn = new Button(content, "Screens/Menu/playBtn/btn", "Screens/Menu/playBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.8f));
            playBtn.X -= playBtn.BtRect.Width / 2;

            scoreBtn = new Button(content, "Screens/Menu/scoreBtn/btn", "Screens/Menu/scoreBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.55f) );
            scoreBtn.X -= scoreBtn.BtRect.Width / 2;

            exitBtn = new Button(content, "Screens/Menu/exitBtn/btn", "Screens/Menu/exitBtn/hBtn", Globals.GAME_WIDTH / 2, (int)(Globals.GAME_HEIGHT / 1.33f)); 
            exitBtn.X -= exitBtn.BtRect.Width / 2;
        }

        public void Update()
        {
            // obtain mouse state
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            // update buttons
            playBtn.Update(mouseState);
            scoreBtn.Update(mouseState);
            exitBtn.Update(mouseState);

            // if mouse is pressed
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed)
            {
                if (playBtn.CheckCollision(mouseState.Position))
                {
                    Globals.GameState = StartScene.GAMESTATE;
                }
                else if (scoreBtn.CheckCollision(mouseState.Position))
                {
                    Globals.GameState = ScoreBoard.GAMESTATE;
                }
                else if (exitBtn.CheckCollision(mouseState.Position))
                {
                    game.Exit();
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            // draw background
            bg.Draw(spriteBatch);

            // draw buttons
            playBtn.Draw(spriteBatch);
            scoreBtn.Draw(spriteBatch);
            exitBtn.Draw(spriteBatch);
            spriteBatch.End();
        }


    }

}
