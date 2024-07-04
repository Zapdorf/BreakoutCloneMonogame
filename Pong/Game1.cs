using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pong.GameObjects;
using Pong.Graphics.ParticleSystem;
using Pong.Graphics.TrailRenderer;
using Pong.Logic;
using Pong.UI;
using SharpDX.X3DAudio;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private GameManager _gameManager;

        const int screenHeight = 800; //900;
        const int screenWidth = 800; //1600;

        Song soundtrack;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;

            //_graphics.IsFullScreen = true;

            Window.Title = "Space Breakout";

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.ScreenHeight = screenHeight;
            Globals.ScreenWidth = screenWidth;

            Globals.scoreMultiplier = 1;
            Globals.livesLeft = 2;
            Globals.inGameOverState = false;

            Globals.soundEnabled = true;

            Globals.debugValue = "debug";

            Globals.Content = Content;

            Globals.gamePaused = false;
            Globals.gameStarted = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            soundtrack = Content.Load<Song>("Sound/SpaceBreakoutSoundtrackDraft");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(soundtrack);

            // initialize game manager
            _gameManager = new();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Space)) ParticleManager.AddParticle(new(Globals.theBall.GetBallPositionCenter(), new()));

            Globals.Update(gameTime);
            _gameManager.Update(gameTime, Keyboard.GetState());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin(SpriteSortMode.FrontToBack); // higher layer number means top

            _gameManager.Draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
