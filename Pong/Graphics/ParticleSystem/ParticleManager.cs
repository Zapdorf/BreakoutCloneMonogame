using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public static class ParticleManager
    {
        private static readonly List<Particle> _particles = new();
        private static readonly List<ParticleEmitter> _emitters = new();

        public static void Update()
        {
            UpdateParticles();
            UpdateEmitters();
        }

        public static void Draw(SpriteBatch batch)
        {
            DrawParticles(batch);
        }

        public static void AddParticle(Particle particle)
        {
            _particles.Add(particle);
        }

        public static void AddParticleEmitter(ParticleEmitter particleEmitter)
        {
            _emitters.Add(particleEmitter);
        }

        private static void UpdateEmitters()
        {
            foreach (var emitter in _emitters)
            {
                emitter.Update();
            }
        }

        private static void UpdateParticles()
        {
            foreach (var particle in _particles)
            {
                particle.Update();
            }

            _particles.RemoveAll(p => p.done);
        }

        private static void DrawParticles(SpriteBatch batch)
        {
            foreach (Particle particle in _particles)
            {
                particle.Draw(batch);
            }
        }
    }
}
