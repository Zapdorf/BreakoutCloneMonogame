using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameObjects;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
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

        private SoundEffect _brokenSoundEffect;
        private SoundEffect _victorySound;

        private int _blocksRemaining;

        private const int BLOCKS_WIDE = 8;
        private const int BLOCKS_HIGH = 8;

        private List<GameLogicTimer> timers;

        private Texture2D _blockTexture;

        public BlockManager() 
        {
            blockList = new List<BlockObject>();
            timers = new List<GameLogicTimer>();

            _colorOptionList = new List<Color>() { Color.Purple, Color.BlueViolet, Color.MediumVioletRed };

            _victorySound = SoundEffectManager.VictorySound;

            _blocksRemaining = BLOCKS_HIGH * BLOCKS_WIDE;

            _blockTexture = Globals.Content.Load<Texture2D>("Images/BlankBlock");

            GenerateBlocks();
        }

        public void Update(GameTime gameTime)
        {
            // update all blocks (?)
            foreach (BlockObject block in blockList)
            {
                block.Update(gameTime);
            }

            foreach (var timer in timers) { timer.Update(gameTime); }
        }

        public void Draw(SpriteBatch batch, float layer)
        {
            // call draw on all the blocks
            foreach (BlockObject block in blockList)
            {
                block.Draw(batch, layer);
            }
        }

        public void GenerateBlocks()
        {
            // create a bunch of new block objects

            Vector2 startPosition = new Vector2((Globals.ScreenWidth/2) - ((BLOCKS_WIDE/2) * _blockTexture.Width), _blockTexture.Height * 4);
            Vector2 currentPosition = startPosition;

            int colorIndex = 0;

            for (int row=0; row < BLOCKS_HIGH; row++)
            {
                for (int col=0; col < BLOCKS_WIDE; col++)
                {
                    BlockObject block = new BlockObject(_blockTexture, _colorOptionList[colorIndex % _colorOptionList.Count], currentPosition, _brokenSoundEffect, this);
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

            _blocksRemaining--;

            if(_blocksRemaining < 1)
            {
                if (Globals.soundEnabled) _victorySound.Play();
                Globals.theBall.NewLevelReset(); // reset ball
                _blocksRemaining = BLOCKS_HIGH * BLOCKS_WIDE;
                timers.Add(new GameLogicTimer(1, true, ResetBlocks));
            }
        }

        public void ResetBlocks()
        {
            foreach (BlockObject block in blockList)
            {
                block.Revive();
            }
        }
    }
}
