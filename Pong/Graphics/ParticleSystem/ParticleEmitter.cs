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

            if (data.prebaked)
            {
                Prebake();
            }
        }

        public void Update()
        {
            if(done) return;

            _intervalLeft -= Globals.ElapsedSeconds;
            if (_intervalLeft <= 0f)
            {
                _intervalLeft = _data.emissionInterval;
                SpawnWave();
            }

            if (_data.emitOnce) done = true;
        }

        private void SpawnWave()
        {
            for (int i = 0; i < _data.emittedEveryInterval; i++)
            {
                ParticleManager.AddParticle(Emit(_emitter.emissionPosition));
            }
        }

        private Particle Emit(Vector2 pos)
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
            return particle;
        }

        private void Prebake()
        {
            List<Particle> particles = new List<Particle>();
            
            // run simulation of 3 seconds or so instantly
            int prebakeTime = 3; //seconds
            float periods = prebakeTime / _data.emissionInterval;

            
            // how many intervals should have run?

            // call update on the particle with fake game time

            // advance fake clock


            // subtract period, spawn amount, add to list
            // update existing list by the same time amount
            int floorPeriod = (int)Math.Floor(periods);
            for(int i=0; i<floorPeriod; i++)
            {
                // advance existing particles
                foreach(Particle particle in particles)
                {
                    GameTime gameTime = new GameTime();

                    float value = _data.emissionInterval;
                    int integerPart = (int)Math.Floor(value);
                    double decimalPart = value - integerPart;

                    var prebakeTimeSpanObj = new TimeSpan(0, 0, 0, integerPart, (int)(1000 * decimalPart));
                    gameTime.ElapsedGameTime = prebakeTimeSpanObj;
                    particle.Update(gameTime);
                }

                // emit new particles
                for (int j = 0; j < _data.emittedEveryInterval; j++)
                {
                    particles.Add(Emit(_emitter.emissionPosition));
                }
            }

            foreach (var item in particles)
            {
                ParticleManager.AddParticle(item);
            }
        }

    }
}
