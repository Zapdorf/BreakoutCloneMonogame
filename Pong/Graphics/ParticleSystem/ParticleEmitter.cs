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
        
        public ParticleEmitterData emitterData;

        private float _intervalLeft;
        private IEmitter _emitter;


        public ParticleEmitter(IEmitter emitter, ParticleEmitterData data) 
        {
            emitterData = data;
            _intervalLeft = emitterData.emissionInterval;
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
                _intervalLeft = emitterData.emissionInterval;
                SpawnWave();
            }

            if (emitterData.emitOnce) done = true;
        }

        public void ChangeParticleColors(Color newColor)
        {
            /*
             * This method is hyper specific for use with the RGB color effect
             * Ideally this method will be made generic later
             * or maybe I'll edit the particle system to have a built in option for color changing
             */
            emitterData.particleData.endColor = newColor;
            emitterData.particleData.startColor = newColor;
        }

        private void SpawnWave()
        {
            for (int i = 0; i < emitterData.emittedEveryInterval; i++)
            {
                ParticleManager.AddParticle(Emit(_emitter.emissionPosition));
            }
        }

        private Particle Emit(Vector2 pos)
        {
            // start with defaults
            ParticleData particleData = new();
            particleData.Copy(emitterData.particleData);

            particleData.angle = emitterData.angle;
            particleData.gravityEnabled = emitterData.gravityEnabled;

            // randomize
            particleData.lifespan = Globals.RandomFloat(emitterData.lifespanMin, emitterData.lifespanMax);
            particleData.startScale = Globals.RandomFloat(emitterData.sizeMin, emitterData.sizeMax);
            particleData.speed = Globals.RandomFloat(emitterData.speedMin, emitterData.speedMax);
            //particleData.speed = _data.speedMax;

            float range = (float)(Globals.random.NextDouble() * 2) - 1; // random float from -1 to 1
            particleData.angle += emitterData.angleVariance * range;

            // instantiate particle
            Particle particle = new(pos, particleData);
            return particle;
        }

        private void Prebake()
        {
            List<Particle> particles = new List<Particle>();
            
            // run simulation of 3 seconds or so instantly
            int prebakeTime = 3; //seconds
            float periods = prebakeTime / emitterData.emissionInterval;

            
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

                    float value = emitterData.emissionInterval;
                    int integerPart = (int)Math.Floor(value);
                    double decimalPart = value - integerPart;

                    var prebakeTimeSpanObj = new TimeSpan(0, 0, 0, integerPart, (int)(1000 * decimalPart));
                    gameTime.ElapsedGameTime = prebakeTimeSpanObj;
                    particle.Update(gameTime);
                }

                // emit new particles
                for (int j = 0; j < emitterData.emittedEveryInterval; j++)
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
