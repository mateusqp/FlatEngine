using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Flat;
using Flat.Graphics;
using Flat.Input;
using System.Diagnostics;
using System;

namespace Game_0
{
    public class Game1 : Game
    {
        //
        private GraphicsDeviceManager _graphics;
        private Sprites sprites;
        private Screen screen;
        private Texture2D texture;
        private Shapes shapes;

        private Camera camera;
        //
        
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
            //
            this._graphics.PreferredBackBufferWidth = 1280;
            this._graphics.PreferredBackBufferHeight = 600;
            this._graphics.ApplyChanges();

            this.sprites = new Sprites(this);
            this.screen = new Screen(this, 1280, 720);
            this.shapes = new Shapes(this);
            this.camera = new Camera(this.screen);
            //




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

            FlatMouse mouse = FlatMouse.Instance;
            mouse.Update();

            if (mouse.IsLeftButtonClicked())
            {
                Console.WriteLine("Left button clicked.");
                
            }
            if (mouse.IsMiddleButtonClicked())
            {
                Console.WriteLine("Middle button clicked.");

            }
            if (mouse.IsRightButtonClicked())
            {
                Console.WriteLine("Right button clicked.");

            }

            if (keyboard.IsKeyDown(Keys.Q))
            {
                Console.WriteLine("Mouse window position: " + mouse.WindowPosition);
                Console.WriteLine("Mouse screen position: " + mouse.GetScreenPosition(this.screen));
            }
            if (keyboard.IsKeyDown(Keys.Z))
            {
                this.camera.IncZoom();
            }
            if (keyboard.IsKeyDown(Keys.X))
            {
                this.camera.DecZoom();
            }
            if (keyboard.IsKeyDown(Keys.C))
            {
                this.camera.GetExtents(out Vector2 min, out Vector2 max); // Centro da câmera é coordenada (0,0)
                Console.WriteLine("Camera min: " + min);
                Console.WriteLine("Camera max: " + max);
            }

            if (keyboard.IsKeyClicked(Keys.Escape)) 
                Exit();
            
            if (keyboard.IsKeyDown(Keys.D))
            {
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.screen.Set();
            this.GraphicsDevice.Clear(Color.CornflowerBlue);



            Viewport vp = this.GraphicsDevice.Viewport;


            this.sprites.Begin(this.camera, false); 
            
            this.sprites.Draw(texture, null, new Rectangle((int)35, 62, 40, 40), Color.White);

            this.sprites.End();



            this.shapes.Begin(this.camera);

            

            this.shapes.End();



            this.screen.Unset();
            this.screen.Present(this.sprites); //19:20 #04


            base.Draw(gameTime);
        }
    }
}
