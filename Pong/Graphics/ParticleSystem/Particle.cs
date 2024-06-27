using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Pong.Graphics.ParticleSystem
{
    public class Particle
    {
        /*
         * 6/23/24
         * Single particle object
         * spawned by particle system
         * Initialized using data from ParticleData.cs
         */

        private readonly ParticleData _data;

        private Vector2 _position;
        private float _lifespanRemaining;
        private float _lifespanPercentage;

        private Color _color;
        private float _opacity;

        private Vector2 _direction;
        private Vector2 _origin;

        private float _scale;

        public bool done;
        
        public Particle(Vector2 pos, ParticleData data) 
        {
            _data = data;
            _position = pos;// - new Vector2(_data.texture.Width/2, _data.texture.Height / 2); //possible unecessary adjustment
            _lifespanRemaining = _data.lifespan;
            _lifespanPercentage = 1f;
            _color = _data.startColor;
            _opacity = _data.opacityStart;
            _scale = _data.startScale;

            done = false;

            _origin = new Vector2(_data.texture.Width/2, _data.texture.Height/2);

            if (_data.speed != 0)
            {
                // moving
                _data.angle = MathHelper.ToRadians(_data.angle);
                _direction = new Vector2((float)Math.Cos(_data.angle), (float)Math.Sin(_data.angle));
            }
            else
            {
                _direction = Vector2.Zero;
            }
        }

        public void Update(GameTime gameTime)
        {
            _lifespanRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_lifespanRemaining <= 0)
            {
                done = true;
                return;
            }

            // visual effect linear interpolation over lifespan
            _lifespanPercentage = MathHelper.Clamp(_lifespanRemaining / _data.lifespan, 0, 1);
            _color = Color.Lerp(_data.endColor, _data.startColor, _lifespanPercentage);
            _opacity = MathHelper.Clamp(MathHelper.Lerp(_data.opacityEnd, _data.opacityStart, _lifespanPercentage), 0, 1);
            _scale = MathHelper.Lerp(_data.endScale, _data.startScale, _lifespanPercentage);
            // scale lerp is broken

            // movement
            _position += _direction * _data.speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_data.gravityEnabled)
            {
                // downward force
                _direction.Y += _data.gravityFactor * (float)gameTime.ElapsedGameTime.TotalSeconds;
                //_direction.Normalize();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_data.texture, _position, null, _color * _opacity, 0f, _origin, _scale,
                SpriteEffects.None, _data.spriteLayer);
        }
    }
}
