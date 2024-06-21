using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.GameObjects
{
    internal class BlockObject
    {
        public int blockId { get; set; }
        
        private Texture2D _blockTexture;
        private Color _blockColor;
        private Vector2 _blockPositon;

        private CollisionDetector collider;

        private bool broken;

        public BlockObject(Texture2D texture, Color blockColor, Vector2 blockPos) 
        {
            _blockTexture = texture;
            _blockColor = blockColor;
            _blockPositon = blockPos;

            collider = new CollisionDetector(_blockTexture.Width, _blockTexture.Height);
            collider.position = _blockPositon;

            broken = false;
        }

        public void Update(GameTime gameTime)
        {
            // check for collision or something against the ball
            // figure out normal based on the ball's velocity

            if (!broken && collider.IsColliding(Globals.ballCollider))
            {
                // hit ball
                Vector2 norm = new Vector2(0, 1); // temp
                Globals.theBall.BounceGap(norm);
                Globals.theBall.Bounce(norm);
                GetHit();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(!broken) batch.Draw(_blockTexture, _blockPositon, _blockColor);
        }

        private void GetHit()
        {
            // increase score
            // call block manager

            // either hide or delete entirely
            broken = true;

            // do a visual effect thing
            // play a sound
        }
    }
}
