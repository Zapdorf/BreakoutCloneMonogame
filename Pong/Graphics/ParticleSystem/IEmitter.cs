using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public interface IEmitter
    {
        /*
         * Generic form of an emitter
         * 
         * emitter types
         * - single point
         * - box
         * - circle?
         */

        public Vector2 emissionPosition { get; set; }
    }
}
