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

namespace PASS3.Classes.Screen
{
    class GameOver
    {
        public const int GAMESTATE = 3;
        private Img bg;
        private Button gameOverBtn;
        int selected;

        public GameOver(ContentManager content)
        {
            bg = new Img(content.Load<Texture2D>("Screens/GameOver/Bg"));
            bg.LoadContent(0,0);

            gameOverBtn = new Button(content, "Screens/GameOver/TryAgainBt/tryAgainBt", "Screens/GameOver/TryAgainBt/htTryAgainBt", 0, (int)(Globals.GAME_HEIGHT / 1.5f));
            gameOverBtn.X = Globals.GAME_WIDTH / 2 - gameOverBtn.BtRect.Width / 2;
        }

        public void Update(MouseState mouseState)
        { 
            // check for Button press
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (gameOverBtn.CheckCollision(mouseState.Position))
                {
                    Globals.GameState = Menu.GAMESTATE;
                }
            }

            gameOverBtn.Update(Mouse.GetState());
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Begin();
            bg.Draw(spriteBatch);
            gameOverBtn.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
