using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Graphics.ParticleSystem
{
    public class BoxEmitter : IEmitter
    {
        private Vector2 _topLeft;
        private Vector2 _bottomRight;
        public Vector2 emissionPosition { 
            get => new Vector2(Globals.RandomFloat(_topLeft.X, _bottomRight.X), Globals.RandomFloat(_topLeft.Y, _bottomRight.Y)); 
            set => throw new NotImplementedException(); 
        }

        public BoxEmitter(Vector2 topLeft, Vector2 bottomRight)
        { 
            SetBoxCoords(topLeft, bottomRight);
        }

        public void SetBoxCoords(Vector2 topLeft, Vector2 bottomRight)
        {
            _bottomRight = bottomRight;
            _topLeft = topLeft;
        }
    }
}
