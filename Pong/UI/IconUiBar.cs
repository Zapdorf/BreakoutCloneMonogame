using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI
{
    public class IconUiBar
    {
        /*
         * A UI bar that operates like hearts do in Zelda
         * A row of icons appears representing the number of X the player has
         * Integer based rather than float based
         * 
         * new feature ideas
         * - specify point at which images stop appearing and you get "item x 5" or something
         */

        private Vector2 _position;

        private Texture2D _iconImageTexture;
        private bool _allowForHalves;

        private int _value;
        private int _maxValue; // max value includes halves (i.e. value of 8 with halves means max should be set as 16)

        private int _additionalSpacing;


        public IconUiBar(Vector2 pos, Texture2D texture, bool allowHalves, int maxValue, int spacing=0) 
        { 
            _position = pos;
            _iconImageTexture = texture;
            _allowForHalves = allowHalves;
            _maxValue = maxValue;
            _additionalSpacing = spacing;
        }

        public void Draw(SpriteBatch spriteBatch, float layer)
        {
            // TO DO
            
            // loop through icons
            int iterations = _allowForHalves ? _value/2 : _value;
            for(int i=0; i<iterations; i++)
            {
                PrintImage(spriteBatch, layer, i, null);
            }

            if (_allowForHalves && _value % 2 != 0)
            {
                Rectangle rect = new(0, 0, _iconImageTexture.Width/2, _iconImageTexture.Height);
                PrintImage(spriteBatch, layer, iterations, rect);
            }
        }

        public void UpdateValue(int value)
        {
            _value = MathHelper.Clamp(value, 0, _maxValue); 
        }

        private void PrintImage(SpriteBatch spriteBatch, float layer, int i, Rectangle? rect)
        {
            spriteBatch.Draw(
                _iconImageTexture, _position + new Vector2((i * (_iconImageTexture.Width + _additionalSpacing)) , 0),
                rect, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, layer
            );
        }
    }
}
