using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Animation2D;
using PASS3.Classes.Screen;
using PASS3.Classes;
using PASS3.Classes.Screen.Cutscenes;

namespace PASS3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Store different screens
        private Menu menu;
        private MainGame mainGame;
        private GameOver gameOver;
        private ScoreBoard scoreBoard;

        // Store score keeper
        private ScoreKeeper scores;
        // store cutscene manager
        private CutsceneManager cutsceneManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // make mouse visible
            this.IsMouseVisible = true;
            
            // Change resolution
            this.graphics.PreferredBackBufferWidth = Globals.GAME_WIDTH;
            this.graphics.PreferredBackBufferHeight = Globals.GAME_HEIGHT;
            this.graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create menu
            menu = new Menu(Content);

            // Create game assets
            mainGame = new MainGame(Content, graphics.GraphicsDevice);

            // create game over screen
            gameOver = new GameOver(Content);

            // Create scoreboard and score keeper
            scores = new ScoreKeeper();
            scoreBoard = new ScoreBoard(Content, scores, GraphicsDevice);

            // set inital gameState
            Globals.GameState = Menu.GAMESTATE;

            // create cutscene manager
            cutsceneManager = new CutsceneManager(Content, mainGame, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (Globals.GameState)
            {
                case Menu.GAMESTATE:
                    menu.Update(gameTime);

                    if (Globals.GameState == CutsceneManager.GAMESTATE)
                    {
                        cutsceneManager.LoadContent() ;
                    }
                    break;
                case MainGame.GAMESTATE:
                    mainGame.Update(gameTime);
                    break;
                case GameOver.GAMESTATE:
                    gameOver.Update(Mouse.GetState());
                    break;
                case ScoreBoard.GAMESTATE:
                    scoreBoard.Update(gameTime);
                    break;
                case CutsceneManager.GAMESTATE:
                    cutsceneManager.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            switch (Globals.GameState)
            {
                case Menu.GAMESTATE:
                    menu.Draw(spriteBatch);
                    break;
                case MainGame.GAMESTATE:
                    mainGame.Draw(spriteBatch);
                    break;
                case GameOver.GAMESTATE:
                    gameOver.Draw(spriteBatch);
                    break;
                case ScoreBoard.GAMESTATE:
                    scoreBoard.Draw(spriteBatch);
                    break;
                case CutsceneManager.GAMESTATE:
                    cutsceneManager.Draw(spriteBatch);
                    break;
            }


            base.Draw(gameTime);
        }
       
    }
}
