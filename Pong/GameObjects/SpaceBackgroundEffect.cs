using Microsoft.Xna.Framework;
using Pong.Graphics.ParticleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameObjects
{
    public class SpaceBackgroundEffect
    {
        private BoxEmitter _boxEmitter;
        private ParticleEmitter _emitter;

        public SpaceBackgroundEffect() 
        {
            var bottomRight = new Vector2(Globals.ScreenWidth, Globals.ScreenHeight);
            bottomRight = new Vector2(Globals.ScreenWidth, 0);
            _boxEmitter = new BoxEmitter(Vector2.Zero, bottomRight);

            ParticleData particleData = new ParticleData()
            {
                startColor = Color.White,
                endColor = Color.White,
                gravityFactor = 3,
                opacityEnd = 1f,
                endScale = 1f
            };
            ParticleEmitterData particleemiiterdata = new()
            {
                particleData = particleData,
                emissionInterval = 0.1f,
                emittedEveryInterval = 1,

                angle = 90,
                angleVariance = 0,

                speedMax = 300,
                speedMin = 300,
                sizeMax = 1f,
                sizeMin = 0.25f,
                lifespanMax = 3f,
                lifespanMin = 3f,

                prebaked = true
            };
            _emitter = new ParticleEmitter(_boxEmitter, particleemiiterdata);
            ParticleManager.AddParticleEmitter(_emitter);
        }
    }
}
