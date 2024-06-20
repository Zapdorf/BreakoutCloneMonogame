using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameObjects
{
    internal class BallObject
    {
        private Vector2 _ballPosition;
        private Texture2D _ballTexture;

        private Vector2 _velocity; // direction, normal vector
        private float _bounceAccelerationAmount; // boost speed after hitting a block
        private float _speed;

        //public CollisionDetector collider;

        public BallObject(Texture2D texture) 
        {
            _ballTexture = texture;
            //_ballPosition = new Vector2((Globals.ScreenWidth / 2) - (_ballTexture.Width/2), Globals.ScreenHeight / 2);
            _ballPosition = new Vector2(0, Globals.ScreenHeight / 2);

            //_velocity = new Vector2(0, 1);
            _velocity = new Vector2(0.5f, 0.5f);
            _velocity.Normalize();
            _speed = 250;

            Globals.ballCollider = new CollisionDetector(_ballTexture.Width, _ballTexture.Height);
        }

        public void Update(GameTime gameTime)
        {
            Globals.ballCollider.position = _ballPosition;
            
            
            // basic movement
            float speedAndTime = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _ballPosition.X += _velocity.X * speedAndTime;
            _ballPosition.Y += _velocity.Y * speedAndTime;


            // check for wall bounces

            // boundaries
            float maxX = Globals.ScreenWidth - _ballTexture.Width;
            float minX = 0;
            float maxY = Globals.ScreenHeight - _ballTexture.Height;
            float minY = 0;

            if (_ballPosition.X > maxX) // right wall
            {
                _ballPosition.X = maxX;
                Bounce(new Vector2(-1, 0));

            }
            if (_ballPosition.X < minX) // left wall
            { 
                _ballPosition.X = minX;
                Bounce(new Vector2(1, 0));
            }
            if (_ballPosition.Y > maxY) // bottom
            {
                _ballPosition.Y = maxY;
                Bounce(new Vector2(0, -1));
            }
            if (_ballPosition.Y < minY) // top
            {
                _ballPosition.Y = minY;
                Bounce(new Vector2(0, 1));
            }

            // paddle collision
            if (Globals.paddleCollider.IsColliding(Globals.ballCollider))
            {
                _ballPosition.Y -= 10;
                Bounce(new Vector2(0, -1));
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_ballTexture, _ballPosition, Color.White);
        }

        public void Bounce(Vector2 normal)
        {
            // take normal vector as input

            // add some jitter to the reflection

            normal.Normalize();
            _velocity.Normalize();
            _velocity = (_velocity - 2 * DotProd(normal, _velocity) * normal);
            _velocity.Normalize();
        }

        private float DotProd(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X * vec2.X) + (vec1.Y * vec2.Y);
        }
    }
}
