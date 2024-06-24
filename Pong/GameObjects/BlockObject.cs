using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        private SoundEffect brokeSound;

        private BlockManager _manager;

        public BlockObject(Texture2D texture, Color blockColor, Vector2 blockPos, SoundEffect hitSound, BlockManager manager) 
        {
            _blockTexture = texture;
            _blockColor = blockColor;
            _blockPositon = blockPos;

            collider = new CollisionDetector(_blockTexture.Width, _blockTexture.Height);
            collider.position = _blockPositon;

            broken = false;

            brokeSound = hitSound;

            _manager = manager;
        }

        public void Update(GameTime gameTime)
        {
            // check for collision or something against the ball
            // figure out normal based on the ball's velocity

            if (!broken && collider.IsColliding(Globals.ballCollider))
            {
                // hit ball
                Vector2 norm = DetermineNormal();


                Globals.theBall.BounceGap(norm);
                Globals.theBall.Bounce(norm);
                GetHit();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(!broken) batch.Draw(_blockTexture, _blockPositon, _blockColor);
        }

        public void Revive()
        {
            broken = false;
        }

        private Vector2 DetermineNormal()
        {
            var ballPosCent = Globals.theBall.GetBallPositionCenter();

            var result = new Vector2(0,1);

            // assuming collision get relative position
            var leftEdgeX = _blockPositon.X;
            var rightEdgeX = _blockPositon.X + _blockTexture.Width;
            if (ballPosCent.X < leftEdgeX)
            {
                result = new Vector2(-1, 0);
            }
            else if (ballPosCent.X > rightEdgeX)
            {
                result = new Vector2(1, 0);
            }
            else if (ballPosCent.Y < _blockPositon.Y)
            {
                result = new Vector2(0, -1);
            }
            else if (ballPosCent.Y > _blockPositon.Y + _blockTexture.Height)
            {
                result = new Vector2(0, 1);
            } 

            return result;
        }

        private void GetHit()
        {
            // increase score
            Globals.score += 10 * Globals.scoreMultiplier;

            // call block manager
            _manager.BlockDestroyed(0);

            // either hide or delete entirely
            broken = true;

            // do a visual effect thing

            // play a sound
            if (Globals.soundEnabled) brokeSound.Play();

            // score multiplier
            if (Globals.scoreMultiplier < 6) Globals.scoreMultiplier++;
        }
    }
}
