using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public GameManager() 
        {
            
        }

        public void Update(GameTime gameTime)
        {
            ParticleManager.Update();
        }

        public void Draw(SpriteBatch batch)
        {
            ParticleManager.Draw(batch);
        }
    }
}
