using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.GameObjects;
using Pong.Logic;

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

        SpriteFont bitwiseFont;
        SpriteFont arialTextFont;

        Vector2 paddlePosition;
        int paddleSpeed = 350;

        int paddleHalfWidth;

        const int screenHeight = 800;
        const int screenWidth = 800;


        PaddleObject paddle;
        BlockManager blockMan;


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


            // initialize game manager
            _gameManager = new();

            blockMan = new BlockManager(_blockTexture, brokenSound);

            Globals.theBall = new BallObject(_ballTexture);

            // initialize paddle
            paddle = new PaddleObject(_paddleTexture, paddleHitSound);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // object updates
            paddle.Update(gameTime, Keyboard.GetState());
            blockMan.Update(gameTime);
            Globals.theBall.Update(gameTime);


            // TODO: Add your update logic here
            Globals.Update(gameTime);
            _gameManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _gameManager.Draw();

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            paddle.Draw(_spriteBatch);
            blockMan.Draw(_spriteBatch);
            Globals.theBall.Draw(_spriteBatch);

            //_spriteBatch.DrawString(bitwiseFont, "Bubsy 3d", new Vector2(200, 0), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
