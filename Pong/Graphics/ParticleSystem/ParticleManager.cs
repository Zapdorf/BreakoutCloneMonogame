using Microsoft.Xna.Framework;
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

        public static void Update(GameTime gameTime)
        {
            UpdateParticles(gameTime);
            UpdateEmitters(gameTime);
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

        private static void UpdateEmitters(GameTime gameTime)
        {
            foreach (var emitter in _emitters)
            {
                emitter.Update();
            }

            _emitters.RemoveAll(e => e.done);
        }

        private static void UpdateParticles(GameTime gameTime)
        {
            foreach (var particle in _particles)
            {
                particle.Update(gameTime);
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
