using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private Texture2D _ballTexture;
        private Texture2D _paddleTexture;
        private Texture2D _blockTexture;

        private SoundEffect brokenSound;
        private SoundEffect paddleHitSound;
        private SoundEffect ballDeadSound;
        private SoundEffect victorySound;

        SpriteFont bitwiseFont;
        SpriteFont arialTextFont;

        const int screenHeight = 800;
        const int screenWidth = 800;

        PaddleObject paddle;
        BlockManager blockMan;
        UiManager uiManager;

        TrailSegment testSegment;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            Window.Title = "Breakout™";

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.ScreenHeight = screenHeight;
            Globals.ScreenWidth = screenWidth;

            Globals.scoreMultiplier = 1;
            Globals.livesLeft = 3;
            Globals.inGameOverState = false;

            Globals.soundEnabled = true;

            Globals.debugValue = "debug";

            Globals.Content = Content;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _ballTexture = Content.Load<Texture2D>("Images/ball");
            _paddleTexture = Content.Load<Texture2D>("Images/paddle");
            _blockTexture = Content.Load<Texture2D>("Images/BlankBlock");

            bitwiseFont = Content.Load<SpriteFont>("Fonts/Bitwise");
            arialTextFont = Content.Load<SpriteFont>("Fonts/galleryFont");

            brokenSound = Content.Load<SoundEffect>("Sound/explosion");
            paddleHitSound = Content.Load<SoundEffect>("Sound/hitHurt");
            ballDeadSound = Content.Load<SoundEffect>("Sound/synth");
            victorySound = Content.Load<SoundEffect>("Sound/powerUp");


            // initialize game manager
            _gameManager = new();

            uiManager = new(bitwiseFont, arialTextFont);

            blockMan = new BlockManager(_blockTexture, brokenSound, victorySound);

            Globals.theBall = new BallObject(_ballTexture, ballDeadSound);

            // initialize paddle
            paddle = new PaddleObject(_paddleTexture, paddleHitSound);

            //testSegment = new(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //if (Keyboard.GetState().IsKeyDown(Keys.Space)) ParticleManager.AddParticle(new(Globals.theBall.GetBallPositionCenter(), new()));

            // object updates
            paddle.Update(gameTime, Keyboard.GetState());
            blockMan.Update(gameTime);
            Globals.theBall.Update(gameTime);


            // TODO: Add your update logic here
            Globals.Update(gameTime);
            _gameManager.Update(gameTime);
            uiManager.Update(gameTime, Keyboard.GetState());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //testSegment.Draw();

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.FrontToBack); // higher layer number means top

            paddle.Draw(_spriteBatch, 0.1f);
            blockMan.Draw(_spriteBatch, 0.5f); 
            Globals.theBall.Draw(_spriteBatch, 0.1f);

            _gameManager.Draw(_spriteBatch);
            
            uiManager.Draw(_spriteBatch, 1);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
