using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Pong.GameObjects
{
    public class BallObject
    {
        private Vector2 _ballPosition;
        private Texture2D _ballTexture;

        private Vector2 _velocity; // direction, normal vector
        private float _bounceAccelerationAmount; // boost speed after hitting a block
        private float _speed;

        private Random rand;

        //public CollisionDetector collider;
        private double disableTimer;

        public BallObject(Texture2D texture) 
        {
            _ballTexture = texture;
            //_ballPosition = new Vector2((Globals.ScreenWidth / 2) - (_ballTexture.Width/2), Globals.ScreenHeight / 2);
            _ballPosition = new Vector2(0, Globals.ScreenHeight / 2);
            //_ballPosition = new Vector2(0, 0);

            //_velocity = new Vector2(0, 1);
            _velocity = new Vector2(0.5f, 0.5f);
            _velocity.Normalize();
            _speed = 250;

            Globals.ballCollider = new CollisionDetector(_ballTexture.Width, _ballTexture.Height);

            rand = new Random();
            var a = RandomNum(-5, -2); // <--- breaks and gives 0

            disableTimer = 0;
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

            // update disable timer
            UpdateDisableTimer(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_ballTexture, _ballPosition, Color.White);
        }

        public Vector2 GetBallPositionCenter()
        {
            return new Vector2(
                _ballPosition.X + (_ballTexture.Width / 2), 
                _ballPosition.Y + (_ballTexture.Height / 2)
            );
        }

        public void Bounce(Vector2 normal)
        {
            // take normal vector as input

            // add some jitter to the reflection

            normal.Normalize();
            _velocity.Normalize();
            _velocity = (_velocity - 2 * DotProd(normal, _velocity) * normal);
            //_velocity += (new Vector2((float)RandomNum(-0.5f, 0.5f), (float)RandomNum(-0.5f, 0.5f)))/4;// jitter
            _velocity.Normalize();

            TemporarilyDisableCollider();
        }

        public void BounceGap(Vector2 normal)
        {
            _ballPosition += 5 * normal;
        }

        private void UpdateDisableTimer(GameTime gameTime)
        {
            if(disableTimer > 0)
            {
                disableTimer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            if(disableTimer <= 0 && Globals.ballCollider.disabled)
            {
                Globals.ballCollider.disabled = false;
            }
        }

        private void TemporarilyDisableCollider()
        {
            Globals.ballCollider.disabled = true;
            disableTimer = 0.1f;
        }

        private double RandomNum(float min, float max)
        {
            double range = max - min;
            return (rand.NextDouble() * range) + min;
        }

        private float DotProd(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X * vec2.X) + (vec1.Y * vec2.Y);
        }
    }
}
