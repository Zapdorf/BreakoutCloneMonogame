using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public class PointEmitter : IEmitter
    {
        public Vector2 emissionPosition { get; set; } // just a single location
    }
}
