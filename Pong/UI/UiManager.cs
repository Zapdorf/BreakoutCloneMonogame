using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Pong.UI
{
    public class UiManager
    {
        /*
         * Should probably become a static class
         */

        // fonts
        SpriteFont bitwiseFont;
        SpriteFont arialTextFont;

        RgbEffect rgbEffect;

        IconUiBar ballIconBar;

        //UiBarAnimated testBar;

        private const string GAME_OVER_TEXT = "GAME OVER";
        private Vector2 _gameOverTextPosition;

        public UiManager(SpriteFont bit, SpriteFont arial)
        {
            bitwiseFont = bit;
            arialTextFont = arial;

            rgbEffect = new();

            var ballTexture = Globals.Content.Load<Texture2D>("Images/ball");
            ballIconBar = new(new Vector2(5, 5), ballTexture, false, 10, 5);

            //var fg = Globals.Content.Load<Texture2D>("Images/UI_Images/HealthBarTest");
            //var bg = Globals.Content.Load<Texture2D>("Images/UI_Images/HealthBarTestBackground");
            //testBar = new(fg, bg, Globals.ScreenCenter(), 10);

            SetGameOverTextPosition();
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            rgbEffect.Update(gameTime);
            //testBar.Update(gameTime);

            ballIconBar.UpdateValue(Globals.livesLeft);
        }

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            var colorToUse = Globals.scoreMultiplier >= 6 ? rgbEffect.GetCurrentColor() : Color.White;

            // score text
            string scoreText = Globals.score.ToString();
            var textSize = bitwiseFont.MeasureString(scoreText);
            var scoreTextPosition = new Vector2(Globals.ScreenWidth / 2 - textSize.X / 2, 5);
            spriteBatch.DrawString(bitwiseFont, scoreText, scoreTextPosition, colorToUse, 0f, Vector2.Zero, 1f,
                SpriteEffects.None, layer);

            // multiplier text
            string multiplierText = "x" + Globals.scoreMultiplier.ToString();
            spriteBatch.DrawString(bitwiseFont, multiplierText, new Vector2(Globals.ScreenWidth - 45, 5), colorToUse, 0f,
                Vector2.Zero, 1f, SpriteEffects.None, layer);

            // balls left
            ballIconBar.Draw(spriteBatch, layer);

            // game over text
            if (Globals.inGameOverState)
            {
                spriteBatch.DrawString(bitwiseFont, GAME_OVER_TEXT, _gameOverTextPosition, Color.White);
            }

            // debug text
            //spriteBatch.DrawString(bitwiseFont, Globals.debugValue, new Vector2(200, 300), Color.White);

            //testBar.Draw(spriteBatch);
        }

        private void SetGameOverTextPosition()
        {
            var textSize = bitwiseFont.MeasureString(GAME_OVER_TEXT);
            _gameOverTextPosition = new Vector2(
                Globals.ScreenWidth/2 - (textSize.X / 2),
                Globals.ScreenHeight / 2 - (textSize.Y/2)
            );
        }
    }
}
