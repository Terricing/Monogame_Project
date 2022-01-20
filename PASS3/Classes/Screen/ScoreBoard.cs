using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASS3.Classes.Screen
{
    class ScoreBoard
    {
        // store representation for gamestate
        public const int GAMESTATE = 4;

        // store representations for each button
        private const int NAME = 1;
        private const int SCORE = 2;

        // store graphics device
        GraphicsDevice gd;

        // store which button is selected
        private int buttonState;

        // store search data
        private string search;

        // store allowed values
        private string allowedName = "abcdefghijklmnopqrstuvwxyz";
        private string allowedScore = "D0D1D2D3D4D5D6D7D8D9";

        // store game's scorekeeper object
        private ScoreKeeper scores;

        // store background
        private Img bg;
        
        // store buttons
        private Button nameBtn;
        private Button scoreBtn;
        private Button searchBtn;

        // store mouse and keyboard state
        private MouseState mouseState;
        private KeyboardState kb;
        private KeyboardState prevKb;
        // store currently pressed keys
        private Keys[] keys;
        private Keys[] prevKeys;

        // searchText font
        private SpriteFont searchText;

        // Show search results
        private Rectangle resultRect;
        private Helper.GameRectangle resultBg;
        private bool showResult = false;
        private string searchResult;

        public ScoreBoard(ContentManager content, ScoreKeeper scores, GraphicsDevice gd)
        {
            // obtain scorekeeper and graphicsdevice objects
            this.scores = scores;
            this.gd = gd;

            // load background
            bg = new Img(content.Load<Texture2D>("Screens/ScoreBoard/bg"));
            bg.LoadContent(0, 0);

            // load buttons
            nameBtn = new Button(content, "Screens/ScoreBoard/NameBtn/btn", "Screens/ScoreBoard/NameBtn/hBtn", 0, (int)(Globals.GAME_HEIGHT / 3f), 0.45f);
            scoreBtn = new Button(content, "Screens/ScoreBoard/ScoreBtn/hBtn", "Screens/ScoreBoard/ScoreBtn/btn", 0, (int)(Globals.GAME_HEIGHT / 3f), 0.45f);
            searchBtn = new Button(content, "Screens/ScoreBoard/SearchBtn/btn", "Screens/ScoreBoard/SearchBtn/hBtn", 0, 0, 0.5f);

            // set button locations
            nameBtn.X = Globals.GAME_WIDTH / 3 - nameBtn.BtRect.Width / 2;
            scoreBtn.X = (Globals.GAME_WIDTH / 3 - nameBtn.BtRect.Width / 2) * 2;
            searchBtn.X = Globals.GAME_WIDTH / 2 - searchBtn.BtRect.Width / 2;
            searchBtn.Y = (int)(Globals.GAME_HEIGHT / 1.5);

            // initialize search with empty string
            search = "";

            // create spritefont
            searchText = content.Load<SpriteFont>("Fonts/SearchFont");

            // Create result rectangle
            resultRect = new Rectangle(0,0, (int)(Globals.GAME_WIDTH / 1.5), (int)(Globals.GAME_HEIGHT / 1.5));
            resultRect.X = Globals.GAME_WIDTH / 2 - resultRect.Width / 2;
            resultRect.Y = Globals.GAME_HEIGHT / 2 - resultRect.Height / 2;
            resultBg = new Helper.GameRectangle(gd, resultRect);

        }

        public void Update(GameTime gameTime)
        {
            // Obtain mouse state
            mouseState = Mouse.GetState();

            // obtain keyboard state
            prevKb = kb;
            kb = Keyboard.GetState();
            prevKeys = keys;
            keys = kb.GetPressedKeys();

            // Update buttons
            nameBtn.Update(mouseState);
            scoreBtn.Update(mouseState);
            searchBtn.Update(mouseState);

            // If mouse is pressed, check if any buttons were pressed
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (nameBtn.BtRect.Contains(mouseState.Position))
                {
                    // Select name button
                    nameBtn.IsSelected = true;
                    scoreBtn.IsSelected = false;

                    // clear search
                    if (buttonState != NAME)
                    {
                        search = "";
                    }
                    buttonState = NAME;

                }
                else if (scoreBtn.BtRect.Contains(mouseState.Position))
                {
                    // select score button
                    scoreBtn.IsSelected = true;
                    nameBtn.IsSelected = false;

                    // clear search
                    if (buttonState != SCORE)
                    {
                        search = "";
                    }
                    buttonState = SCORE;
                }
                else if (searchBtn.BtRect.Contains(mouseState.Position))
                {
                    // perform search
                    if(search.Length > 0)
                    {
                       switch (buttonState)
                        {
                            case SCORE:
                                searchResult = scores.ObtainName(Convert.ToInt32(search));
                                if (searchResult.Equals(""))
                                {
                                    searchResult = "No name found";
                                }
                                break;
                            case NAME:
                                searchResult = Convert.ToString(scores.ObtainScore(search));
                                if (searchResult.Equals("-1"))
                                {
                                    searchResult = "No score found";
                                }

                                break;
                        }

                        showResult = true;
                    }
                }
            }

            if (keys.Length > 0)
            {
                switch (buttonState)
                {
                    case NAME:
                        // check if input is in allowed keys
                        if (allowedName.Contains(keys[0].ToString().ToLower()) || keys[0].Equals(Keys.Space))
                        {
                            // check if input does not match prev input
                            if (prevKeys.Length > 0)
                            {
                                if (keys[0] != prevKeys[0])
                                {
                                    if (keys[0].Equals(Keys.Space))
                                    {
                                        search = search.PadRight(search.Length + 1);
                                    }
                                    else
                                    {
                                        // add input to search string
                                        search += keys[0].ToString();
                                    }

                                }
                            }
                            else
                            {
                                if (keys[0].Equals(Keys.Space))
                                {
                                    Console.WriteLine(true);
                                    search = search.PadRight(search.Length + 1);
                                }
                                else
                                {
                                    // add input to search string
                                    search += keys[0].ToString();
                                }

                            }
                        }
                        break;

                    case SCORE:
                        //Console.WriteLine(keys[0].ToString());
                        // check if input is allowed
                        if (allowedScore.Contains(keys[0].ToString()))
                        { 
                            // check if input matches previous input
                            if (prevKeys.Length > 0)
                            {
                                try
                                {
                                    if (keys[0] != prevKeys[0])
                                    {
                                        // add input to search string
                                        search += keys[0].ToString()[1];
                                    }
                                }
                                catch (IndexOutOfRangeException)
                                {

                                }
                            }
                            else
                            {
                                try
                                {
                                    // add input to search string
                                    search += keys[0].ToString()[1];
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    // do nothing if caught
                                }
                            }
                        }
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // draw background
            bg.Draw(spriteBatch);
            
            // draw buttons
            nameBtn.Draw(spriteBatch);
            scoreBtn.Draw(spriteBatch);
            searchBtn.Draw(spriteBatch);

            // Draw search text
            spriteBatch.DrawString(searchText, search, new Vector2(0, 0), Color.White);

            if (showResult)
            {
                //spriteBatch.DrawRectangle(resultRect, Color.Aqua, 5, 1);
                resultBg.Draw(spriteBatch, Color.Aqua, true);
                spriteBatch.DrawString(searchText, searchResult, new Vector2(Globals.GAME_WIDTH / 2 - searchText.MeasureString(searchResult).X / 2, Globals.GAME_HEIGHT / 2 - searchText.MeasureString(searchResult).Y / 2), Color.DarkGray);
            }

            spriteBatch.End();
        }


    }
}
