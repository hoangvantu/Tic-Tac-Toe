using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Itexico.TicTacToe.Core;
using Microsoft.Xna.Framework.Input.Touch;

namespace Itexico.TicTacToe.AndroidOS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logoTexture;
        TicTacToeGame TicTacToeGame = new TicTacToeGame();
        Texture2D Square;
        Texture2D Pixel;

        MouseState MouseLast { get; set; }
        MouseState MouseCurrent { get; set; }
        SpriteFont Font { get; set; }

        #endregion

        #region Initialization

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

        }

        protected override void Initialize()
        {
            base.Initialize();
            var screenHeight = graphics.PreferredBackBufferHeight;
            TicTacToeGame.Init(graphics.PreferredBackBufferWidth, screenHeight, screenHeight / 4);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            logoTexture = Content.Load<Texture2D>("logo");
            Square = CreateSquareTexture(graphics.PreferredBackBufferHeight / 4);
            Pixel = CreatePixel();
        }

        #endregion

        #region Update and Draw


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GetInputs();
        }

        protected void GetInputs()
        {
            MouseLast = MouseCurrent;
            MouseCurrent = Mouse.GetState();


            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                this.Exit();

            if (MouseCurrent.LeftButton == ButtonState.Released
                && MouseLast.LeftButton == ButtonState.Pressed)
            {
                if (Clicked((int)Mouse.GetState().X, (int)Mouse.GetState().Y))
                    return;
            }

            var touchCollection = TouchPanel.GetState().Where(x => x.State == TouchLocationState.Pressed);
            foreach (var touch in touchCollection)
            {
                if (Clicked((int)touch.Position.X, (int)touch.Position.Y))
                    return;
            }
        }

        protected bool Clicked(int x, int y)
        {
            foreach (var column in TicTacToeGame.Board)
            {
                foreach (var cell in column)
                {
                    if (cell.Collide(x, y, cell.Size, cell.Size))
                    {
                        TicTacToeGame.CellClicked(cell);
                        return true;
                    }
                }
            }
            return false;
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var column in TicTacToeGame.Board)
            {
                foreach (var cell in column)
                {
                    spriteBatch.Draw(Square, new Vector2(cell.PositionOnScreenX, cell.PositionOnScreenY), Color.White);
                    DrawBorder(new Rectangle(cell.PositionOnScreenX, cell.PositionOnScreenY, cell.Size, cell.Size), 2, Color.Black);
                    if (cell.Move != Player.NOTHING)
                    {
                        var cellCenter = cell.Center;
                        var color = (cell.Move == Player.CROSS) ? Color.Green : Color.Red;

                        spriteBatch.Draw(logoTexture, new Vector2(cellCenter.X - (logoTexture.Width / 2),
                                    cellCenter.Y - (logoTexture.Height / 2)), color);
                    }
                }
            }

            if (TicTacToeGame.SomePlayerHasWin)
            {
                if (TicTacToeGame.Winner == Player.BOTH)
                {
                    spriteBatch.Draw(logoTexture, new Vector2(graphics.PreferredBackBufferWidth / 8,
                            graphics.PreferredBackBufferHeight / 2), Color.Black);
                }
                else
                {
                    var color = (TicTacToeGame.Winner == Player.CROSS) ? Color.Green : Color.Red;
                    spriteBatch.Draw(logoTexture, new Vector2(graphics.PreferredBackBufferWidth / 8,
                            graphics.PreferredBackBufferHeight / 2), color);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion


        private Texture2D CreateSquareTexture(int size)
        {
            Texture2D texture = new Texture2D(GraphicsDevice, size, size);
            Color[] data = new Color[size * size];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.White;
            }

            texture.SetData(data);

            return texture;
        }

        private Texture2D CreatePixel()
        {
            Texture2D texture = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = Color.White;
            texture.SetData(data);
            return texture;
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(Pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(Pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(Pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                rectangleToDraw.Y,
                thicknessOfBorder,
                rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(Pixel, new Rectangle(rectangleToDraw.X,
                rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                rectangleToDraw.Width,
                thicknessOfBorder), borderColor);
        }
    }
}
