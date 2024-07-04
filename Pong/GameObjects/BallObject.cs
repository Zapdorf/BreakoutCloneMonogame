using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pong.Graphics;
using Pong.Graphics.ParticleSystem;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;


namespace Pong.GameObjects
{
    public class BallObject
    {
        private Vector2 _ballPosition;
        private Texture2D _ballTexture;

        private Vector2 _velocity; // direction, normal vector
        private float _bounceAccelerationAmount; // boost speed after hitting a block
        private float _speed;

        private bool _ballIsDead;

        private List<GameLogicTimer> _timers;

        private SoundEffect _ballDeadSoundEffect;

        private ParticleEmitter _emitter;
        private PointEmitter _pointEmitter;

        private RgbEffect rgbEffect;


        private const float DEFAULT_SPEED = 250;

        public BallObject() 
        {
            _ballIsDead = false;
            _ballTexture = Globals.Content.Load<Texture2D>("Images/ball");
            _ballPosition = new Vector2(0, Globals.ScreenHeight / 2);

            _velocity = new Vector2(0.5f, 0.5f);
            _velocity.Normalize();
            _speed = DEFAULT_SPEED;

            _ballDeadSoundEffect = Globals.Content.Load<SoundEffect>("Sound/synth"); ;

            Globals.ballCollider = new CollisionDetector(_ballTexture.Width, _ballTexture.Height);

            _timers = new List<GameLogicTimer>();


            // emission
            rgbEffect = new();
            _pointEmitter = new();
            _pointEmitter.emissionPosition = GetBallPositionCenter();
            ParticleData particle = new()
            {
                endScale = 1f,

                endColor = rgbEffect.GetCurrentColor(),
                startColor = rgbEffect.GetCurrentColor()
            };
            ParticleEmitterData particleEmitterData = new ParticleEmitterData()
            {
                particleData = particle,

                emissionInterval = 0.01f,
                emittedEveryInterval = 1,
                angleVariance = 12,
                lifespanMax = 1,
                lifespanMin = 1,
                speedMax = 0,
                speedMin = 0,
                sizeMax = 1,
                sizeMin = 1,

            };
            _emitter = new ParticleEmitter(_pointEmitter, particleEmitterData);
            ParticleManager.AddParticleEmitter(_emitter);
            _pointEmitter.emissionPosition = new Vector2(1000, 1000);
        }

        public void Update(GameTime gameTime)
        {
            Globals.ballCollider.position = _ballPosition;

            var hideParticleSystem = _ballIsDead || Globals.scoreMultiplier < 6;
            _pointEmitter.emissionPosition = hideParticleSystem ? new Vector2(1000, 1000) : GetBallPositionCenter();

            _speed = DEFAULT_SPEED + ((Globals.scoreMultiplier-1) * 50 );

            UpdateParticleTrailEffect(gameTime);
            

            // basic movement
            BasicMovement(gameTime);

            // check for wall bounces
            CheckForWallBounces();

            // update timers
            foreach(var timer in _timers)
            {
                timer.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch batch, float layer)
        {
            if (!_ballIsDead) { 
                batch.Draw(_ballTexture, _ballPosition, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
            }
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
            _velocity.Normalize();

            //TemporarilyDisableCollider();
        }

        public void BounceGap(Vector2 normal)
        {
            _ballPosition += 5 * normal;
        }

        public void PaddleBounce(Vector2 newDirection)
        {
            // completely changes velocity
            newDirection.Normalize();
            _velocity = newDirection;
        }

        public void NewLevelReset()
        {
            _ballIsDead = true;

            _ballPosition = new Vector2(0, Globals.ScreenHeight / 2);
            _velocity = Vector2.Zero;

            // start timer
            _timers.Add(new GameLogicTimer(1.5f, true, BallReset));
        }

        private void UpdateParticleTrailEffect(GameTime gameTime)
        {
            rgbEffect.Update(gameTime);
            _emitter.ChangeParticleColors(rgbEffect.GetCurrentColor());
        }

        private void BasicMovement(GameTime gameTime)
        {
            float speedAndTime = _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _ballPosition.X += _velocity.X * speedAndTime;
            _ballPosition.Y += _velocity.Y * speedAndTime;
        }

        private void CheckForWallBounces()
        {
            if (_ballIsDead) return;
            
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
                //_ballPosition.Y = maxY;
                //Bounce(new Vector2(0, -1));


                // dead
                BallDie();
            }
            if (_ballPosition.Y < minY) // top
            {
                _ballPosition.Y = minY;
                Bounce(new Vector2(0, 1));
            }
        }

        private void TemporarilyDisableCollider()
        {
            Globals.ballCollider.disabled = true;
            _timers.Add(new GameLogicTimer(0.05f, true, ReenableCollider));
        }

        private void ReenableCollider()
        {
            Globals.ballCollider.disabled = false;
        }

        private float DotProd(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X * vec2.X) + (vec1.Y * vec2.Y);
        }

        private void BallDie()
        {
            // play sound
            if(Globals.soundEnabled) _ballDeadSoundEffect.Play();

            _ballIsDead = true;
            _velocity = Vector2.Zero;

            Globals.livesLeft--;

            // start timer
            _timers.Add(new GameLogicTimer(1.5f, true, BallReset));
        }

        public void BallReset()
        {
            // check if the game is over
            if (Globals.livesLeft < 0)
            {
                // display game over stuff and allow for game reset
                Globals.inGameOverState = true;
            } 
            else
            {
                _ballIsDead = false;
                _ballPosition = new Vector2(0, Globals.ScreenHeight / 2);

                _velocity = new Vector2(0.5f, 0.5f);
                _velocity.Normalize();

                Globals.scoreMultiplier = 1;
                _speed = DEFAULT_SPEED;
            }
            
            
        }
    }
}
