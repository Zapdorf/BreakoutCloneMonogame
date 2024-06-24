using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public UiManager(SpriteFont bit, SpriteFont arial) 
        {
            bitwiseFont = bit;
            arialTextFont = arial;

            
        }

        public void Update(GameTime gameTime)
        {
            // this might get used later for UI effects
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // score text
            string scoreText = Globals.score.ToString();
            var textSize = bitwiseFont.MeasureString(scoreText);
            var scoreTextPosition = new Vector2((Globals.ScreenWidth / 2) - textSize.X/2, 5);
            spriteBatch.DrawString(bitwiseFont, scoreText, scoreTextPosition, Color.White);

            // multiplier text
            string multiplierText = "x" + Globals.scoreMultiplier.ToString();
            spriteBatch.DrawString(bitwiseFont, multiplierText, new Vector2(Globals.ScreenWidth - 45, 5), Color.White);

            // debug text
            //spriteBatch.DrawString(bitwiseFont, Globals.debugValue, new Vector2(200, 300), Color.White);
        }
    }
}
