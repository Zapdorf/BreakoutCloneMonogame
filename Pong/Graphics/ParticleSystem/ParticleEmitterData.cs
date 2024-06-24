using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public class ParticleEmitterData
    {
        public ParticleData particleData = new();
        public float angle = 0;
        public float angleVariance = 0;
        public float lifespanMin = 0.1f;
        public float lifespanMax = 2f;
        public float speedMin = 10f;
        public float speedMax = 100f;
        public float emissionInterval = 1f; // emit every ...
        public int emittedEveryInterval = 1; // amount emitted
        
        public ParticleEmitterData() { }
    }
}
