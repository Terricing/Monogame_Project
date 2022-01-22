// Author: Eilay Katsnelson
// File Name: Game1.cs
// Project Name: PASS3
// Creation Date: January 6, 2022
// Modified Date: January 21, 2022
// Description: A game titled Stay in Your LANE featuring Mr. Lane and a student

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

        // Create start scene
        StartScene startScene;

        // Store score keeper
        private ScoreKeeper scores;

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
            menu = new Menu(Content, this);

            // Create game assets
            mainGame = new MainGame(Content, graphics.GraphicsDevice);

            // Create scoreboard and score keeper
            scores = new ScoreKeeper();
            scoreBoard = new ScoreBoard(Content, scores, GraphicsDevice);

            // create game over screen
            gameOver = new GameOver(Content, scores);

            // create start scene, whcih can obtain player's name
            startScene = new StartScene(Content);

            // set inital gameState
            Globals.GameState = Menu.GAMESTATE;

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
            // Update part of the game that is currently running
            switch (Globals.GameState)
            {
                case Menu.GAMESTATE:
                    menu.Update();

                    // if menu switches to a different part of the game, load (reset) that component
                    if (Globals.GameState == StartScene.GAMESTATE)
                    {
                        startScene.LoadContent();
                    }
                    else if (Globals.GameState == ScoreBoard.GAMESTATE)
                    {
                        scoreBoard.LoadContent();
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
                case StartScene.GAMESTATE:
                    startScene.Update();

                    // if start scene is finished, load (reset) mainGame component, then switch the gamestate
                    if (startScene.IsOver)
                    {
                        mainGame.LoadContent();
                        Globals.GameState = MainGame.GAMESTATE;
                    }

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

            // Draw componenet that is currently running

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
                case StartScene.GAMESTATE:
                    startScene.Draw(spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
       
    }
}
