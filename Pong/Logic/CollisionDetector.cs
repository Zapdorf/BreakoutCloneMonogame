using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Logic
{
    public class CollisionDetector
    {
        public float halfWidth;
        public float halfHeight;
        public Vector2 position { get; set; }

        public bool disabled;

        // test collision with color changing when you move the paddle onto the ball or something


        // to determine bounce normal on a block
        // calculate rays out from object center to corners
        // this subdivides into 4 spots in a radial fashion
        // calculate the direction from block center to ball center
        // figure out based on degree which quad it falls into

        public CollisionDetector(float w, float h)
        {
            halfWidth = w/2;
            halfHeight = h/2;
            disabled = false;
        }
        
        public bool IsColliding(CollisionDetector other)
        {
            if (disabled || other.disabled) return false;
            
            var otherCenter = other.GetCenter();
            var thisCenter = GetCenter();

            // x align
            bool xAlign =
                (otherCenter.X + other.halfWidth > thisCenter.X - halfWidth) &&
                (otherCenter.X - other.halfWidth < thisCenter.X + halfWidth);

            bool yAlign =
                (otherCenter.Y + other.halfHeight > thisCenter.Y - halfHeight) &&
                (other.position.Y - other.halfHeight < position.Y + halfHeight);


            return xAlign && yAlign;
                
        }

        private Vector2 GetCenter()
        {
            return new Vector2(position.X + halfWidth, position.Y+ halfHeight);
        }
    }
}
