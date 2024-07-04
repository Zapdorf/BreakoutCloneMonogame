using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        private const float GAME_OVER_TEXT_SCALE = 1.25f;


        private const string START_TEXT = "SPACE BREAKOUT";
        private const string START_TEXT_BUTTON_PROMPT = "Press Space";
        private Vector2 _startTextPosition;
        private Vector2 _startTextButtonPromptPosition;
        private const float START_TEXT_SCALE = 1.5f;
        private const float START_TEXT_BUTTON_PROMPT_SCALE = 0.75f;

        private const string PAUSE_TEXT = "PAUSED";
        private Vector2 _pauseTextPosition;

        public UiManager()
        {
            // LOAD
            bitwiseFont = Globals.Content.Load<SpriteFont>("Fonts/Bitwise");
            arialTextFont = Globals.Content.Load<SpriteFont>("Fonts/galleryFont");


            // INITIALIZE
            rgbEffect = new();

            var ballTexture = Globals.Content.Load<Texture2D>("Images/ball");
            ballIconBar = new(new Vector2(5, 5), ballTexture, false, 10, 5);

            //var fg = Globals.Content.Load<Texture2D>("Images/UI_Images/HealthBarTest");
            //var bg = Globals.Content.Load<Texture2D>("Images/UI_Images/HealthBarTestBackground");
            //testBar = new(fg, bg, Globals.ScreenCenter(), 10);

            _gameOverTextPosition = GetTextPositionForCenterOfScreen(GAME_OVER_TEXT, GAME_OVER_TEXT_SCALE);

            _startTextButtonPromptPosition = GetTextPositionForCenterOfScreen(START_TEXT_BUTTON_PROMPT, START_TEXT_BUTTON_PROMPT_SCALE) + new Vector2(0, 40);
            _startTextPosition = GetTextPositionForCenterOfScreen(START_TEXT, START_TEXT_SCALE);

            _pauseTextPosition = GetTextPositionForCenterOfScreen(PAUSE_TEXT);
        }

        public void Update(GameTime gameTime)
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
            if (Globals.inGameOverState) DisplayGameOverText(spriteBatch, layer);

            // start game text
            if (!Globals.gameStarted) DisplayStartGameText(spriteBatch, layer);

            // pause text
            if(Globals.gamePaused) spriteBatch.DrawString(bitwiseFont, PAUSE_TEXT, _pauseTextPosition, Color.White);

            // debug text
            //spriteBatch.DrawString(bitwiseFont, Globals.debugValue, new Vector2(200, 300), Color.White);
        }

        private Vector2 GetTextPositionForCenterOfScreen(string input, float scale=1)
        {
            // takes a string, calculates the length, then returns a position that would center it in the center of the screen
            var textSize = bitwiseFont.MeasureString(input) * scale;
            return new Vector2(
                Globals.ScreenWidth / 2 - (textSize.X / 2),
                Globals.ScreenHeight / 2 - (textSize.Y / 2)
            );
        }

        private void DisplayStartGameText(SpriteBatch spriteBatch, float layer)
        {
            spriteBatch.DrawString(bitwiseFont, START_TEXT, _startTextPosition, Color.White, 0f,
                Vector2.Zero, START_TEXT_SCALE, SpriteEffects.None, layer);

            spriteBatch.DrawString(bitwiseFont, START_TEXT_BUTTON_PROMPT, _startTextButtonPromptPosition, Color.White, 0f,
                Vector2.Zero, START_TEXT_BUTTON_PROMPT_SCALE, SpriteEffects.None, layer);
        }

        private void DisplayGameOverText(SpriteBatch spriteBatch, float layer)
        {
            spriteBatch.DrawString(bitwiseFont, GAME_OVER_TEXT, _gameOverTextPosition, Color.White, 0f,
                Vector2.Zero, GAME_OVER_TEXT_SCALE, SpriteEffects.None, layer);

            spriteBatch.DrawString(bitwiseFont, START_TEXT_BUTTON_PROMPT, _startTextButtonPromptPosition, Color.White, 0f,
                Vector2.Zero, START_TEXT_BUTTON_PROMPT_SCALE, SpriteEffects.None, layer);
        }
    }
}
