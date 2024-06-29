using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.UI
{
    public class UiBar
    {
        /*
         * Progress bar
         * percentage based bar
         * 
         * image type bar too
         * -> this might need a class extension
         * 
         * Features
         * - tiling bar with end caps
         * - using images like hearts in zelda
         * - bar animation
         * - smooth bar transition (segment lost turns white then lerps to current amount)
         * - extend bar or shink it without creating a new texture
         * 
         */

        //public float percentage;

        protected float currentValue;
        protected float maxValue;

        protected Vector2 position;

        protected Texture2D foregroundTexture;
        protected Texture2D backgroundTexture;

        protected Rectangle segmentShowing;

        //Rectangle LeftBarSection;
        //Rectangle CenterBarSection;
        //Rectangle RightBarSection;


        public UiBar(Texture2D fg, Texture2D bg, Vector2 pos, float max)
        {
            foregroundTexture = fg;
            backgroundTexture = bg;
            maxValue = max;
            position = pos;

            currentValue = maxValue;

            segmentShowing = new Rectangle(0,0, foregroundTexture.Width, foregroundTexture.Height);
        }

        int multiplier = -1;
        public virtual void Update(GameTime gameTime)
        {
            currentValue += multiplier * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if((currentValue <= 0 && multiplier < 0) || (currentValue >= maxValue && multiplier > 0))
            {
                multiplier *= -1;
            }

            segmentShowing.Width = (int)((currentValue / maxValue) * (foregroundTexture.Width));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTexture, position, Color.White);
            spriteBatch.Draw(foregroundTexture, position, segmentShowing, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);


            // CODE FOR THE CAP PRESERVATION THING
            // draw segment then draw cap
            /*
            int segmentLength = _foregroundTexture.Width / 5;

            Rectangle everythingButCap = part;
            everythingButCap.Width -= segmentLength;
            spriteBatch.Draw(_foregroundTexture, _position, part, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            Rectangle cap = new(segmentLength*4, 0, segmentLength, _foregroundTexture.Height);
            spriteBatch.Draw(_foregroundTexture, _position + new Vector2(everythingButCap.Width + segmentLength,0), cap, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            */
        }

        public virtual void UpdateValue(float value)
        {
            currentValue = value;
            currentValue = (float)Globals.Clamp(currentValue, 0, maxValue);
        }

        public float GetValue()
        {
            // perhaps unused. mostly for debug.
            return currentValue;
        }
    }
}
