using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameObjects
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        public Rectangle rect;
        
        public Sprite(Texture2D t, Vector2 p) 
        {
            texture = t;
            position = p;

            rect = new Rectangle();
            rect.Width = texture.Width;
            rect.Height = texture.Height;
            
        }
    }
}
