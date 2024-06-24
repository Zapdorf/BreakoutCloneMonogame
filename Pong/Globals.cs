using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameObjects;
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
        public static float ElapsedSeconds { get; set; }

        public static CollisionDetector ballCollider { get; set; }
        public static CollisionDetector paddleCollider { get; set; }

        public static BallObject theBall { get; set; }  

        public static bool soundEnabled { get; set; }

        public static string debugValue { get; set; }

        public static int scoreMultiplier { get; set; }
        public static int score { get; set; }
        public static int livesLeft { get; set; } // display as balls
        public static int level { get; set; } // might go unused


        public static Random random { get; set; } = new();


        public static void Update(GameTime gameTime)
        {
            ElapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static double DotProd(Vector2 vec1, Vector2 vec2)
        {
            return (vec1.X * vec2.X) + (vec1.Y * vec2.Y);
        }

        public static double Clamp(double value, double min, double max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public static float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min)) + min;
        }
    }
}
