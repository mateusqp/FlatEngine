using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using System.Diagnostics;

namespace Game_0
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private Sprites sprites;
        private Screen screen;
        private Texture2D texture;

        private Shapes shapes;

        private float x = 0;
        private Stopwatch stopwatch;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.SynchronizeWithVerticalRetrace = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            
        }

        protected override void Initialize()
        {
            this._graphics.PreferredBackBufferWidth = 1280;
            this._graphics.PreferredBackBufferHeight = 600;
            this._graphics.ApplyChanges();

            this.sprites = new Sprites(this);
            this.screen = new Screen(this, 640, 480);
            this.shapes = new Shapes(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.texture = this.Content.Load<Texture2D>("Box");
        }

        protected override void Update(GameTime gameTime)
        {
            FlatKeyboard keyboard = FlatKeyboard.Instance;
            keyboard.Update();

            if (keyboard.IsKeyClicked(Keys.Escape))
                Exit();
            
            if (keyboard.IsKeyDown(Keys.D))
            {
                x += 3;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.screen.Set();
            this.GraphicsDevice.Clear(Color.CornflowerBlue);



            Viewport vp = this.GraphicsDevice.Viewport;


            this.sprites.Begin(false); 
            
            this.sprites.Draw(texture, null, new Rectangle((int)x, 32, 40, 40), Color.White);

            this.sprites.End();



            this.shapes.Begin();

            this.shapes.DrawRectangle(x, 130, 30, 45, Color.Red);
            this.shapes.DrawRectangle(450, 150, 260, 95, Color.DarkCyan);
            this.shapes.DrawLine(new Vector2(24, 32), new Vector2(48, 150), 5f, Color.Black);

            this.shapes.End();



            this.screen.Unset();
            this.screen.Present(this.sprites); //19:20 #04


            base.Draw(gameTime);
        }
    }
}
