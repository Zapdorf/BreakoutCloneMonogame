using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.GameObjects;
using Pong.Graphics.ParticleSystem;
using Pong.Logic;
using Pong.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public class GameManager
    {
        public bool GamePaused;

        private SpaceBackgroundEffect spaceBackgroundEffect;

        private PaddleObject paddle;
        private BlockManager blockMan;
        private UiManager uiManager;

        private bool canPause = true;

        public GameManager() 
        {
            spaceBackgroundEffect = new();

            Globals.theBall = new BallObject();
            paddle = new PaddleObject();
            blockMan = new BlockManager();
            uiManager = new UiManager();
        }

        public void Update(GameTime gameTime, KeyboardState keyState)
        {
            // every game object should be here

            // Manage inputs like the pause button or start button
            ManageInputs(keyState);

            // Update UI
            uiManager.Update(gameTime);

            if (Globals.gamePaused) return;

            // particles run before the game starts but pause with everything else
            ParticleManager.Update(gameTime);

            if (!Globals.gameStarted) return;

            // update objects
            paddle.Update(gameTime, keyState);
            blockMan.Update(gameTime);
            Globals.theBall.Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            paddle.Draw(batch, 0.1f);
            blockMan.Draw(batch, 0.5f);

            uiManager.Draw(batch, 1);

            ParticleManager.Draw(batch);


            if (!Globals.gameStarted) return;

            Globals.theBall.Draw(batch, 0.1f);
        }

        private void ManageInputs(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && canPause)
            {
                // restart it if in a game over state
                if (Globals.inGameOverState)
                {
                    RestartGame();
                } 
                else if(Globals.gameStarted)
                {
                    PauseGame();
                }

                // start the game
                Globals.gameStarted = true;

                canPause = false;
            } 
            else if (keyState.IsKeyUp(Keys.Space))
            {
                // wait for key up before allowing another pause input
                canPause = true;
            }
        }

        private void PauseGame()
        {
            // also called to unpause
            Globals.gamePaused = !Globals.gamePaused;
        }

        private void RestartGame()
        {
            // this code is repeated from Game1.cs
            Globals.scoreMultiplier = 1;
            Globals.livesLeft = 2;
            Globals.inGameOverState = false;
            Globals.score = 0;

            // reset the ball
            Globals.theBall.BallReset();

            // reset paddle
            paddle.ResetPaddle();

            // reset blocks
            blockMan.ResetBlocks();
        }
    }
}
