using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameObjects;
using Pong.Graphics.ParticleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class GameManager
    {
        public bool GamePaused;

        private SpaceBackgroundEffect spaceBackgroundEffect;

        public GameManager() 
        {
            spaceBackgroundEffect = new();
        }

        public void Update(GameTime gameTime)
        {
            ParticleManager.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            ParticleManager.Draw(batch);
        }
    }
}
