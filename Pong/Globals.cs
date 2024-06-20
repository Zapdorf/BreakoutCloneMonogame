using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public static class Globals
    {
        public static int ScreenHeight { get; set; }
        public static int ScreenWidth { get; set; }
        
        public static ContentManager Content { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        public static float ElapsedSeconds { get; set; }

        public static CollisionDetector ballCollider { get; set; }
        public static CollisionDetector paddleCollider { get; set; }

        public static void Update(GameTime gameTime)
        {
            ElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
