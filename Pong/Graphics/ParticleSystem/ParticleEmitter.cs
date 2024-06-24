using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public class ParticleEmitter
    {
        public Vector2 emissionPosition { get; set; }
        
        private readonly ParticleEmitterData _data;
        private float _intervalLeft;

        
        public ParticleEmitter(ParticleEmitterData data) 
        {
            _data = data;
            _intervalLeft = _data.emissionInterval;
            emissionPosition = new Vector2(Globals.ScreenWidth/2, Globals.ScreenHeight/2);
        }

        public void Update()
        {
            _intervalLeft -= Globals.ElapsedSeconds;
            if(_intervalLeft <= 0f)
            {
                _intervalLeft = _data.emissionInterval;
                for (int i=0; i<_data.emittedEveryInterval; i++)
                {
                    Emit(emissionPosition); // position could be randomized
                }
            }
        }

        private void Emit(Vector2 pos)
        {
            // start with defaults
            ParticleData particleData = _data.particleData;

            // randomize
            particleData.lifespan = Globals.RandomFloat(_data.lifespanMin, _data.lifespanMax);
            particleData.speed = Globals.RandomFloat(_data.speedMin, _data.speedMax);

            float range = (float)(Globals.random.NextDouble() * 2) - 1; // random float from -1 to 1
            particleData.angle += _data.angleVariance * range;

            // instantiate particle
            Particle particle = new(pos, particleData);
            ParticleManager.AddParticle(particle);
        }
    }
}
