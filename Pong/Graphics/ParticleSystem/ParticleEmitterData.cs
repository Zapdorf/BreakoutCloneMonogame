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

        public float lifespanMin = 2f;
        public float lifespanMax = 2f;
        /*
         * Don't randomize the lifespan
         * if you do and have lerped opacity and/or color
         * you'll get flashing particles
         * 
         * you also get a weird situation sometimes in which the particles are really small
         * for some reason
         */

        public float speedMin = 10f;
        public float speedMax = 100f;
        public float emissionInterval = 1f; // emit every ...
        public int emittedEveryInterval = 1; // amount emitted

        public float sizeMin = 0.5f;
        public float sizeMax = 1f;

        public bool gravityEnabled = false;
        public bool emitOnce = false;
        
        public ParticleEmitterData() { }
    }
}
