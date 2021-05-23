using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame_Sokobon.TerminalSokobon;
using Monogame_Sokobon.LevelThings;
using Monogame_Sokobon.LevelThings.Procedural;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Monogame_Sokobon
{
    public class SokobonGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D emptySquare;
        private int boardSize = 10;
        private int difficulty = 0;

        public SokobonGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.AllowUserResizing = true;
            //Soko.playSokobon(10,10,2);
            base.Initialize();
            //Console.WriteLine(JsonSerializer.Serialize(new LevelData()));
            String fileName = "Content/Levels/Cringe.json";
            String jsonString = File.ReadAllText(fileName);
            LevelData weatherForecast = JsonSerializer.Deserialize<LevelData>(jsonString);
            //Console.WriteLine(weatherForecast);
            Soko.loadSokoban(new GenerateLevel().createLevel());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            emptySquare = new Texture2D(GraphicsDevice,1,1);
            emptySquare.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        bool isPressed = false;
        protected override void Update(GameTime gameTime)
        {
            Resolution.Update(_graphics);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else if (Keyboard.GetState().IsKeyDown(Keys.Z)&&!isPressed){
                Soko.loadSokoban(new GenerateLevel().createLevel());
                //Board.Undo();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.W)&&!isPressed){
                Board.Player.Move(0);
                Board.update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A)&&!isPressed){
                Board.Player.Move(1);
                Board.update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S)&&!isPressed){
                Board.Player.Move(2);
                Board.update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)&&!isPressed){
                Board.Player.Move(3);
                Board.update();
                isPressed = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D1)&&!isPressed){
                //Board size 10*10
                //boardSize = 10;
                //Soko.playSokobon(10,10,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D2)&&!isPressed){
                //Board size 20*20
                //boardSize = 20;
                //Soko.playSokobon(20,20,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D3)&&!isPressed){
                //Board size 30*30
                //boardSize = 30;
                //Soko.playSokobon(30,30,2);

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D4)&&!isPressed){
                //Board size 40*40
                //boardSize = 40;
                //Soko.playSokobon(40,40,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D5)&&!isPressed){
                //Board size 50*50
                //boardSize = 50;
                //Soko.playSokobon(50,50,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D6)&&!isPressed){
                //Board size 60*60
                //boardSize = 60;
                //Soko.playSokobon(60,60,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D7)&&!isPressed){
                //Board size 70*70
                //boardSize = 70;
                //Soko.playSokobon(70,70,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D8)&&!isPressed){
                //Board size 80*80
                //boardSize = 80;
                //Soko.playSokobon(80,80,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D9)&&!isPressed){
                //Board size 90*90
                //boardSize = 90;
                //Soko.playSokobon(90,90,2);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D0)&&!isPressed){
                //Board size 100*100
                //boardSize = 100;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
                //Soko.playSokobon(100,100,2);
            }
            else if(Keyboard.GetState().GetPressedKeyCount()==0&&isPressed){
                isPressed = false;
                
            }
            

            // TODO: Add your update logic here
            if(!Board.IsRunning){
                difficulty++;
                //Exit();
                Soko.playSokobon(boardSize,boardSize*3,difficulty+2);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            //_spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Resolution.Scale);
            _spriteBatch.Begin();
            int squareSize = (int)Math.Min(GraphicsDevice.Viewport.Width/(Board.board.GetLength(1)+1),GraphicsDevice.Viewport.Height/(Board.board.GetLength(0)+1));
            Vector2 middle = new Vector2(GraphicsDevice.Viewport.Width/2,GraphicsDevice.Viewport.Height/2);
            for(int row = 0; row < Board.board.GetLength(1); row ++){
                for(int col = 0; col < Board.board.GetLength(0); col++){
                    int middlXOffset = Board.board.GetLength(1)*squareSize/2;
                    int middlYOffset = Board.board.GetLength(0)*squareSize/2;
                    int startX = (int)middle.X - middlXOffset + row * squareSize;
                    int startY = (int)middle.Y - middlYOffset + col * squareSize;
                    _spriteBatch.Draw(emptySquare, new Rectangle(startX,startY,squareSize,squareSize), Color.Black);
                    switch(Board.board[col,row]){
                       case 0:
                           _spriteBatch.Draw(emptySquare, new Rectangle(startX,startY,squareSize-2,squareSize-2), Color.White);
                            break;
                       case 1:
                       case 6:
                            _spriteBatch.Draw(emptySquare, new Rectangle(startX,startY,squareSize-2,squareSize-2), Color.Black);
                            break;
                       case 2:
                            _spriteBatch.Draw(emptySquare, new Rectangle(startX,startY,squareSize-2,squareSize-2), Color.Red);
                            break;
                       case 3:
                            _spriteBatch.Draw(emptySquare, new Rectangle(startX,startY,squareSize-2,squareSize-2), Color.Green);
                            break;
                       case 4:
                            _spriteBatch.Draw(emptySquare, new Rectangle(+startX,startY,squareSize-2,squareSize-2), Color.Brown);
                            break;
                       case 5:
                            _spriteBatch.Draw(emptySquare, new Rectangle(+startX,startY,squareSize-2,squareSize-2), Color.Gold);
                            break;
                    }
                }
            }
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

/// <summary>
/// Resolution
/// </summary>
public static class Resolution
{
    private static Vector3 ScalingFactor;
    private static int _preferredBackBufferWidth;
    private static int _preferredBackBufferHeight;

    /// <summary>
    /// The virtual screen size. Default is 1280x800. See the non-existent documentation on how this works.
    /// </summary>
    public static Vector2 VirtualScreen = new Vector2(1280, 800);

    /// <summary>
    /// The screen scale
    /// </summary>
    public static Vector2 ScreenAspectRatio = new Vector2(1, 1);

    /// <summary>
    /// The scale used for beginning the SpriteBatch.
    /// </summary>
    public static Matrix Scale;

    /// <summary>
    /// The scale result of merging VirtualScreen with WindowScreen.
    /// </summary>
    public static Vector2 ScreenScale;

    /// <summary>
    /// Updates the specified graphics device to use the configured resolution.
    /// </summary>
    /// <param name="device">The device.</param>
    /// <exception cref="System.ArgumentNullException">device</exception>
    public static void Update(GraphicsDeviceManager device)
    {
        if (device == null) throw new ArgumentNullException("device");

        //Calculate ScalingFactor
        _preferredBackBufferWidth = device.PreferredBackBufferWidth;
        float widthScale = _preferredBackBufferWidth / VirtualScreen.X;

        _preferredBackBufferHeight = device.PreferredBackBufferHeight;
        float heightScale = _preferredBackBufferHeight / VirtualScreen.Y;

        ScreenScale = new Vector2(widthScale, heightScale);

        ScreenAspectRatio = new Vector2(widthScale / heightScale);
        ScalingFactor = new Vector3(widthScale, heightScale, 1);
        Scale = Matrix.CreateScale(ScalingFactor);
        device.ApplyChanges();
    }


    /// <summary>
    /// <para>Determines the draw scaling.</para>
    /// <para>Used to make the mouse scale correctly according to the virtual resolution,
    /// no matter the actual resolution.</para>
    /// <para>Example: 1920x1080 applied to 1280x800: new Vector2(1.5f, 1,35f)</para>
    /// </summary>
    /// <returns></returns>
    public static Vector2 DetermineDrawScaling()
    {
        var x = _preferredBackBufferWidth / VirtualScreen.X;
        var y = _preferredBackBufferHeight / VirtualScreen.Y;
        return new Vector2(x, y);
    }
}

