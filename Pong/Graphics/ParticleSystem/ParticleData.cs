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

        public float spriteLayer = 0;

        public float lifespan = 2f;

        public Color startColor = Color.Yellow;
        public Color endColor = Color.Red;

        public float opacityStart = 1f;
        public float opacityEnd = 0f;

        public float startScale = 1f;
        public float endScale = 0f;

        public float speed = 0f;
        public float angle = 0f;

        public bool gravityEnabled = false;
        public float gravityFactor = 2f;

        public ParticleData() { }

        public void Copy(ParticleData data)
        {
            texture = data.texture;

            spriteLayer = data.spriteLayer;

            lifespan = data.lifespan;

            startColor = data.startColor;
            endColor = data.endColor;   

            opacityStart = data.opacityStart;   
            opacityEnd = data.opacityEnd;   

            startScale = data.startScale;
            endScale = data.endScale;

            speed = data.speed;
            angle = data.angle;

            gravityEnabled = data.gravityEnabled;
            gravityFactor = data.gravityFactor;
        }
    }
}
