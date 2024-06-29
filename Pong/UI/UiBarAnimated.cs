using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Pong.UI
{
    public class UiBarAnimated : UiBar
    {
        /*
         * This isn't going to be used in breakout
         * This is really just for a future game
         * Honestly this file should just get moved
         * 
         * 
         * Issues and shortcomings with the system:
         * 
         * - doesn't handle it when a second change occurs during a change.
         * What needs to happen is the change is factored into the equation and the timer is reset.
         * This means that the dark area might be twice as wide.
         * Alternatively, I could skip the current animation.
         * 
         * - The inherited bar cap issue
         */
        
        
        private float _targetValue;
        private float _animationSpeed = 10f;
        private Rectangle _animationSegment;
        private Vector2 _animationPosition;
        private Color _animationShade;

        private float delayTime = 0.5f;

        private bool delayOn;
        private GameLogicTimer delayTimer;

        public UiBarAnimated(Texture2D fg, Texture2D bg, Vector2 pos, float maxValue) : base(fg, bg, pos, maxValue)
        { 
            _targetValue = maxValue;
            _animationSegment = new(foregroundTexture.Width, 0, 0, foregroundTexture.Height);
            _animationPosition = pos;
            _animationShade = Color.DarkGray;

            delayOn = true;

            delayTimer = new GameLogicTimer(delayTime, false, TurnDelayOff);
        }

        public override void Update(GameTime gameTime)
        {
            delayTimer.Update(gameTime);
            
            if (_targetValue == currentValue) return;

            var elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_targetValue < currentValue)
            {
                // drop
                BarLerping(elapsed, -1);
            }
            else
            {
                // increase
                BarLerping(elapsed, 1);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            // animated part
            spriteBatch.Draw(foregroundTexture, _animationPosition, _animationSegment, _animationShade, 0, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }

        public override void UpdateValue(float value)
        {
            _targetValue = value;
            _targetValue = (float)Globals.Clamp(_targetValue, 0, maxValue);

            delayTimer.ActivateNow();
            delayTimer.Reset();
        }

        private void BarLerping(float elapsed, int sign)
        {
            // can drop or increase
            if(!delayOn) currentValue += sign * _animationSpeed * elapsed;

            int x;
            if(sign < 0)
            {
                _animationShade = Color.DarkGray * 0.75f;
                x = (int)((_targetValue / maxValue) * foregroundTexture.Width);
            } 
            else
            {
                _animationShade = Color.Gray;
                x = (int)((currentValue / maxValue) * foregroundTexture.Width);
            }


            // termination point
            if((sign < 0 && currentValue < _targetValue) || 
                (sign > 0 && currentValue > _targetValue))
            {
                currentValue = _targetValue;
                delayOn = true;
            }


            segmentShowing.Width = x;
            _animationSegment.X = x;
            _animationSegment.Width = (int)(Math.Abs(currentValue - _targetValue) / maxValue * foregroundTexture.Width);
            _animationPosition.X = position.X + x;
            
        }

        private void TurnDelayOff()
        {
            delayOn = false;
        }
    }
}
