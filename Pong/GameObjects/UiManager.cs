using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace Pong.GameObjects
{
    internal class UiManager
    {
        /*
         * What do you think this manages?
         */

        // fonts
        SpriteFont bitwiseFont;
        SpriteFont arialTextFont;

        List<Color> rgb = new List<Color>() { Color.Red, Color.Chartreuse, Color.LightSkyBlue };
        Color rgbTextColor;
        float currentValue = 0;
        float timeBetweenSwitch = 1;
        int index = 0;

        public UiManager(SpriteFont bit, SpriteFont arial) 
        {
            bitwiseFont = bit;
            arialTextFont = arial;

            rgbTextColor = rgb[index];
        }

        public void Update(GameTime gameTime)
        {
            // this might get used later for UI effects
            currentValue += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (currentValue >= timeBetweenSwitch)
            {
                currentValue = 0;
                index++;
            }

            rgbTextColor = Color.Lerp(rgb[(index) % (rgb.Count)], rgb[(index+1) % (rgb.Count)], currentValue/timeBetweenSwitch);
        }

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            var colorToUse = (Globals.scoreMultiplier >= 6) ? rgbTextColor : Color.White;
            
            // score text
            string scoreText = Globals.score.ToString();
            var textSize = bitwiseFont.MeasureString(scoreText);
            var scoreTextPosition = new Vector2((Globals.ScreenWidth / 2) - textSize.X/2, 5);
            spriteBatch.DrawString(bitwiseFont, scoreText, scoreTextPosition, colorToUse, 0f, Vector2.Zero, 1f, 
                SpriteEffects.None, layer);

            // multiplier text
            string multiplierText = "x" + Globals.scoreMultiplier.ToString();
            spriteBatch.DrawString(bitwiseFont, multiplierText, new Vector2(Globals.ScreenWidth - 45, 5), colorToUse, 0f, 
                Vector2.Zero, 1f, SpriteEffects.None, layer);

            // debug text
            //spriteBatch.DrawString(bitwiseFont, Globals.debugValue, new Vector2(200, 300), Color.White);
        }
    }
}
