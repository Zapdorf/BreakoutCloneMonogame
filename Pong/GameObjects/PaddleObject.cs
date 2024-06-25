using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Logic;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameObjects
{
    internal class PaddleObject
    {
        private Texture2D _paddleTexture;

        private Vector2 _paddlePosition;

        private int _paddleSpeed = 350;
        private int _paddleHalfWidth;

        private SoundEffect _paddleHitSound;

        public PaddleObject(Texture2D texture, SoundEffect paddleHit) {
            _paddleTexture = texture;

            _paddleHalfWidth = _paddleTexture.Width / 2;
            _paddlePosition = new Vector2((Globals.ScreenWidth / 2) - _paddleHalfWidth, Globals.ScreenHeight - 100);

            _paddleHitSound = paddleHit;

            Globals.paddleCollider = new CollisionDetector(_paddleTexture.Width, _paddleTexture.Height);
        }

        public void Update(GameTime gameTime, KeyboardState keyState)
        {
            Globals.paddleCollider.position = _paddlePosition;


            PaddleMovement(gameTime, keyState);


            // paddle collision
            if (Globals.paddleCollider.IsColliding(Globals.ballCollider))
            {
                BallCollision();
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {
            batch.Draw(_paddleTexture, _paddlePosition, Color.White);
        }

        private void BallCollision()
        {
            var newBallDirection = new Vector2(0, -1);

            // get new ball direction based on where it hits
            var ballX = Globals.theBall.GetBallPositionCenter().X;

            // deflection degrees range from 45 to 135, (0-90, +45 ) 
            // there's a 45 degree angle on both sides
            float angleNextToNormal = 60f; // <- this should be const
            float angleRange = angleNextToNormal * 2; 
            float angleAdjustment = 90 - angleNextToNormal;

            // get degree
            double positionDifference = Globals.Clamp(ballX - _paddlePosition.X, 0, _paddleTexture.Width);

            //Globals.debugValue = (_paddlePosition.X - ballX).ToString();

            if(positionDifference == 0)
            {
                //positionDifference = _paddleTexture.Width;
            }

            float percentage = (float)(positionDifference / _paddleTexture.Width);
            float angleDegrees = angleAdjustment + (percentage * angleRange);
            double angleInRadians = MathHelper.ToRadians(angleDegrees);

            newBallDirection = new Vector2(-(float)Math.Cos(angleInRadians), -(float)Math.Sin(angleInRadians));

            Globals.theBall.BounceGap(newBallDirection);
            Globals.theBall.PaddleBounce(newBallDirection);

            if (Globals.soundEnabled) _paddleHitSound.Play();
        }

        private void PaddleMovement(GameTime gameTime, KeyboardState keyState)
        {
            int paddleDirection = 0;
            if (keyState.IsKeyDown(Keys.Left)) paddleDirection -= 1;
            if (keyState.IsKeyDown(Keys.Right)) paddleDirection += 1;
            _paddlePosition.X += paddleDirection * _paddleSpeed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;


            // debug y movement
            /*paddleDirection = 0;
            if (keyState.IsKeyDown(Keys.Up)) paddleDirection -= 1;
            if (keyState.IsKeyDown(Keys.Down)) paddleDirection += 1;
            _paddlePosition.Y += paddleDirection * _paddleSpeed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;*/


            // boundaries
            float maxX = Globals.ScreenWidth - _paddleTexture.Width;
            float minX = 0;
            if (_paddlePosition.X > maxX) _paddlePosition.X = maxX;
            if (_paddlePosition.X < minX) _paddlePosition.X = minX;
        }
    }
}
