using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public class ParticleData
    {
        /*
         * 6/23/24
         * Data for a particle
         */

        public Texture2D texture = Globals.Content.Load<Texture2D>("Images/particle");

        public float lifespan = 2f;

        public Color startColor = Color.Yellow;
        public Color endColor = Color.Red;

        public float opacityStart = 1f;
        public float opacityEnd = 0f;

        public float startSize = 32f;
        public float endSize = 4f;

        public float speed = 0f;
        public float angle = 0f;

        public ParticleData() { }
    }
}
