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
        
        Color[] color = new Color[400];
        int[] a = new int[400];
        int[] r = new int[400];
        int[] g = new int[400];
        int[] b = new int[400];
        Vector2[] v1 = new Vector2[400];
        Vector2[] v2 = new Vector2[400];
        Vector2[] vertices;

        private Camera camera;

        Stopwatch watchKeyLimiter = new Stopwatch();
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
            this.watchKeyLimiter.Start();
            //

            Random rand = new Random();

            int vertexCount = 7;
            this.vertices = new Vector2[vertexCount];

            for (int i = 0; i < vertices.Length; i++)
            {
                float x = rand.Next() % this.screen.Width - this.screen.Width / 2;
                float y = rand.Next() % this.screen.Height - this.screen.Height / 2;
                this.vertices[i] = new Vector2(x, y);
            }


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
                Console.WriteLine("Middle button clicked. Stopwatch elapsed: " + this.watchKeyLimiter.ElapsedMilliseconds);

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

            if (keyboard.IsKeyDown(Keys.F))
            {
                if (this.watchKeyLimiter.ElapsedMilliseconds > 300)
                {
                    Util.ToggleFullScreen(this._graphics);
                    this.watchKeyLimiter.Restart();
                }
                
            }

            for (int i = 0; i < 400; i++)
            {

                Random rnd = new Random(Guid.NewGuid().GetHashCode());

                a[i] = rnd.Next(1, 256);
                r[i] = rnd.Next(1, 256);
                g[i] = rnd.Next(1, 256);
                b[i] = rnd.Next(1, 256);
                v1[i] = new Vector2(i * 2, 25);
                v2[i] = new Vector2(i * 4, 25);
                float rr = r[i];
                float gg = g[i];
                float bb = b[i];
                color[i] = new Color(rr, gg, bb);
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

            for (int i = 0; i < 400; i++)
            {
                shapes.DrawLineSlow(v1[i], v2[i], 12f, new Color(r[i], g[i], b[i]));
                //Console.WriteLine("R: " + r[i] + "G: " + g[i] + "B: " + b[i]);
            }
            //this.shapes.DrawRectangleFill(150f, 150f, 150f, 300f, Color.Red);

            this.shapes.DrawCircle(50, 50, 62, 32, 2, Color.RosyBrown);
            this.shapes.DrawLine(new Vector2(0, 0), new Vector2(50, 50), 15, Color.RosyBrown);
            this.shapes.DrawPolygon(vertices, 4, Color.WhiteSmoke);

            this.shapes.End();



            this.screen.Unset();
            this.screen.Present(this.sprites); //19:20 #04


            base.Draw(gameTime);
        }
    }
}
