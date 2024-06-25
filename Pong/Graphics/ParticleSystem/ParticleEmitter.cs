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
        //public Vector2 emissionPosition { get; set; }
        public bool done;
        
        private readonly ParticleEmitterData _data;
        private float _intervalLeft;
        private IEmitter _emitter;


        public ParticleEmitter(IEmitter emitter, ParticleEmitterData data) 
        {
            _data = data;
            _intervalLeft = _data.emissionInterval;
            _emitter = emitter;
        }

        public void Update()
        {
            if(done) return;
            
            _intervalLeft -= Globals.ElapsedSeconds;
            if(_intervalLeft <= 0f)
            {
                _intervalLeft = _data.emissionInterval;
                for (int i=0; i<_data.emittedEveryInterval; i++)
                {
                    Emit(_emitter.emissionPosition); // position could be randomized
                }
            }

            if (_data.emitOnce) done = true;
        }

        private void Emit(Vector2 pos)
        {
            // start with defaults
            ParticleData particleData = new();
            particleData.Copy(_data.particleData);

            particleData.angle = _data.angle;
            particleData.gravityEnabled = _data.gravityEnabled;

            // randomize
            particleData.lifespan = Globals.RandomFloat(_data.lifespanMin, _data.lifespanMax);
            particleData.startScale = Globals.RandomFloat(_data.sizeMin, _data.sizeMax);
            particleData.speed = Globals.RandomFloat(_data.speedMin, _data.speedMax);
            //particleData.speed = _data.speedMax;

            float range = (float)(Globals.random.NextDouble() * 2) - 1; // random float from -1 to 1
            particleData.angle += _data.angleVariance * range;

            // instantiate particle
            Particle particle = new(pos, particleData);
            ParticleManager.AddParticle(particle);
        }
    }
}
