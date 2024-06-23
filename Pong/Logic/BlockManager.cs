using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameObjects;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace Pong.Logic
{
    internal class BlockManager
    {
        /*
         * This is a class that manages all the blocks in the game
         * this will spawn them, count them, and respawn them
         */

        public List<BlockObject> blockList;

        private List<Color> _colorOptionList;
        private Texture2D _blockTexture;

        private SoundEffect _brokenSoundEffect;

        public BlockManager(Texture2D texture, SoundEffect sound) 
        {
            blockList = new List<BlockObject>();
            _blockTexture = texture;

            _colorOptionList = new List<Color>() { Color.Purple, Color.BlueViolet, Color.MediumVioletRed };

            _brokenSoundEffect = sound;

            GenerateBlocks();
        }

        public void Update(GameTime gameTime)
        {
            // update all blocks (?)
            foreach (BlockObject block in blockList)
            {
                block.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            // call draw on all the blocks
            foreach (BlockObject block in blockList)
            {
                block.Draw(batch);
            }
        }

        public void GenerateBlocks()
        {
            // create a bunch of new block objects

            int blocksWide = 8;
            int blocksHigh = 8;

            Vector2 startPosition = new Vector2((Globals.ScreenWidth/2) - ((blocksWide/2) * _blockTexture.Width), _blockTexture.Height * 4);
            Vector2 currentPosition = startPosition;

            int colorIndex = 0;

            for (int row=0; row < blocksHigh; row++)
            {
                for (int col=0; col < blocksWide; col++)
                {
                    BlockObject block = new BlockObject(_blockTexture, _colorOptionList[colorIndex % _colorOptionList.Count], currentPosition, _brokenSoundEffect);
                    blockList.Add(block);
                    
                    colorIndex++;

                    currentPosition.X += _blockTexture.Width;
                }

                currentPosition.X = startPosition.X;
                currentPosition.Y += _blockTexture.Height;
            }
        }

        public void BlockDestroyed(int blockId)
        {
            // called by block object. Removes from list
        }
    }
}
