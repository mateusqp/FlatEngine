using Flat.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace Flat.Input
{
    public sealed class FlatMouse
    {
        private static readonly Lazy<FlatMouse> Lazy = new Lazy<FlatMouse>(() => new FlatMouse());

        public static FlatMouse Instance
        {
            get { return Lazy.Value; }
        }

        private MouseState prevMouseState;
        private MouseState currMouseState;

        public Point WindowPosition
        {
            get { return this.currMouseState.Position; }
        }

        public FlatMouse()
        {
            this.prevMouseState = Mouse.GetState();
            this.currMouseState = prevMouseState;
        }

        public void Update()
        {
            this.prevMouseState = this.currMouseState;
            this.currMouseState = Mouse.GetState();
        }

        public bool IsLeftButtonDown()
        {
            return this.currMouseState.LeftButton == ButtonState.Pressed;
        }
        public bool IsRightButtonDown()
        {
            return this.currMouseState.RightButton == ButtonState.Pressed;
        }
        public bool IsMiddleButtonDown()
        {
            return this.currMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftButtonClicked()
        {
            return this.currMouseState.LeftButton == ButtonState.Pressed && this.prevMouseState.LeftButton == ButtonState.Pressed;
        }
        public bool IsRightButtonClicked()
        {
            return this.currMouseState.RightButton == ButtonState.Pressed && this.prevMouseState.RightButton == ButtonState.Pressed;
        }
        public bool IsMiddleButtonClicked()
        {
            return this.currMouseState.MiddleButton == ButtonState.Pressed && this.prevMouseState.MiddleButton == ButtonState.Pressed;
        }

        public Vector2 GetScreenPosition(Screen screen)
        {
            Rectangle screenDestinationRectangle = screen.CalculateDestinationRectangle();

            Point windowPosition = this.WindowPosition;

            float sx = windowPosition.X - screenDestinationRectangle.X;
            float sy = windowPosition.Y - screenDestinationRectangle.Y;

            sx /= (float)screenDestinationRectangle.Width;
            sy /= (float)screenDestinationRectangle.Height;

            sx *= (float)screen.Width;
            sy *= (float)screen.Height;

            sy = (float)screen.Height - sy;

            return new Vector2(sx, sy); 
        }
    }
}
