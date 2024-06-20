using Microsoft.Xna.Framework;
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


        public PaddleObject(Texture2D texture) {
            _paddleTexture = texture;

            _paddleHalfWidth = _paddleTexture.Width / 2;
            _paddlePosition = new Vector2((Globals.ScreenWidth / 2) - _paddleHalfWidth, Globals.ScreenHeight - 100);

            Globals.paddleCollider = new CollisionDetector(_paddleTexture.Width, _paddleTexture.Height);
        }

        public void Update(GameTime gameTime, KeyboardState keyState)
        {
            Globals.paddleCollider.position = _paddlePosition;
            
            int paddleDirection = 0;
            if (keyState.IsKeyDown(Keys.Left)) paddleDirection -= 1;
            if (keyState.IsKeyDown(Keys.Right)) paddleDirection += 1;
            _paddlePosition.X += paddleDirection * _paddleSpeed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;


            // debug y movement
            paddleDirection = 0;
            if (keyState.IsKeyDown(Keys.Up)) paddleDirection -= 1;
            if (keyState.IsKeyDown(Keys.Down)) paddleDirection += 1;
            _paddlePosition.Y += paddleDirection * _paddleSpeed *
                (float)gameTime.ElapsedGameTime.TotalSeconds;


            // boundaries
            float maxX = Globals.ScreenWidth - _paddleTexture.Width;
            float minX = 0;
            if (_paddlePosition.X > maxX) _paddlePosition.X = maxX;
            if (_paddlePosition.X < minX) _paddlePosition.X = minX;
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch batch)
        {

            /*if (collider.IsColliding(Globals.ballCollider)) {
                batch.Draw(_paddleTexture, _paddlePosition, Color.Green);
            } 
            else
            {
                batch.Draw(_paddleTexture, _paddlePosition, Color.White);
            }*/
            batch.Draw(_paddleTexture, _paddlePosition, Color.White);
        }
    }
}
