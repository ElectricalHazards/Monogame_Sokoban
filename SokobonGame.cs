using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monogame_Sokobon.LevelThings;
using Monogame_Sokobon.LevelThings.Procedural;
using Monogame_Sokobon.TerminalSokobon;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using NJsonSchema;
using Microsoft.VisualBasic;

namespace Monogame_Sokobon {

    public enum LevelType {
        Generated,
        Custom,
        External
    }
    public class SokobonGame : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D emptySquare;
        private SpriteFont font;
        private int boardSize = 10;
        private int difficulty = 1;
        public static int moves = 0;
        public static int level = 1;

        public static bool isLevelEditor = false;
        public static LevelType levelType = LevelType.Generated;


        public static bool flag = true;

        private LevelData currentLevel;

        public SokobonGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            Window.AllowUserResizing = true;
            //Soko.playSokobon(10,10,2);
            base.Initialize();
            //Console.WriteLine(JsonSerializer.Serialize(new LevelData()));
            String fileName = "Content/Levels/Cringe.json";
            String jsonString = File.ReadAllText(fileName);
            //LevelData weatherForecast = JsonSerializer.Deserialize<LevelData>(jsonString);
            //Console.WriteLine(weatherForecast);
            currentLevel = new GenerateLevel().createLevel(difficulty);
            Soko.loadSokoban(currentLevel);
            levelType = LevelType.Generated;
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            emptySquare = new Texture2D(GraphicsDevice, 1, 1);
            font = Content.Load<SpriteFont>("font");
            emptySquare.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        bool isPressed = false;
        protected override void Update(GameTime gameTime) {
            if (!isLevelEditor) {
                Resolution.Update(_graphics);
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                else if (Keyboard.GetState().IsKeyDown(Keys.Z) && !isPressed) {
                    Board.Undo();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Y) && !isPressed) {
                    currentLevel = new GenerateLevel().createLevel(difficulty);
                    Soko.loadSokoban(currentLevel);
                    levelType = LevelType.Generated;
                    moves = 0;
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.R) && !isPressed) {
                    Board.Reset();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W) && !isPressed) {
                    Board.Player.Move(0);
                    Board.update();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A) && !isPressed) {
                    Board.Player.Move(1);
                    Board.update();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S) && !isPressed) {
                    Board.Player.Move(2);
                    Board.update();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D) && !isPressed) {
                    Board.Player.Move(3);
                    Board.update();
                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.F1) && !isPressed) {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog {
                        InitialDirectory = @"C:\",
                        Title = "Browse Level File",

                        CheckFileExists = true,
                        CheckPathExists = true,

                        DefaultExt = "json",
                        Filter = "json files (*.json)|*.json",
                        FilterIndex = 2,
                        RestoreDirectory = true,

                        ReadOnlyChecked = true,
                        ShowReadOnly = true
                    };

                    if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                        String jsonString = File.ReadAllText(openFileDialog1.FileName);
                        JsonSchema schema = JsonSchema.FromType<LevelData>();
                        if (schema.Validate(jsonString).Count == 0) {
                            LevelData level = JsonSerializer.Deserialize<LevelData>(jsonString);
                            Soko.loadSokoban(level);
                            moves = 0;
                            levelType = LevelType.External;
                        }
                        else {
                            MessageBox.Show("Error Loading Level: " + openFileDialog1.FileName + ". ");
                        }
                    }

                    isPressed = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.F2) && !isPressed) {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.InitialDirectory = @"C:\";
                    saveFileDialog1.Title = "Save Level Files";
                    //saveFileDialog1.CheckFileExists = true;
                    saveFileDialog1.CheckPathExists = true;
                    saveFileDialog1.DefaultExt = "json";
                    saveFileDialog1.Filter = "json files (*.json)|*.json";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.RestoreDirectory = true;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName)) {
                            writer.Write(JsonSerializer.Serialize(currentLevel));
                        }
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.F3) && !isPressed) {
                    isLevelEditor = true;
                    LevelEditor levelEditor = new LevelEditor();
                    levelEditor.Show();
                }
                else if (Keyboard.GetState().GetPressedKeyCount() == 0 && isPressed) {
                    isPressed = false;

                }


                // TODO: Add your update logic here
                if (!Board.IsRunning) {
                    //Exit();
                    moves = 0;
                    if (levelType == LevelType.Generated) {
                        difficulty++;
                        level++;
                    }
                    currentLevel = new GenerateLevel().createLevel(difficulty);
                    Soko.loadSokoban(currentLevel);
                    levelType = LevelType.Generated;
                    //Soko.playSokobon(boardSize,boardSize*3,difficulty+2);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(flag?Color.Blue:Color.White);
            //_spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Resolution.Scale);
            _spriteBatch.Begin();
            int squareSize = (int)Math.Min(GraphicsDevice.Viewport.Width / (Board.board.GetLength(1) + 1), GraphicsDevice.Viewport.Height / (Board.board.GetLength(0) + 1));
            Vector2 middle = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            if (!isLevelEditor) {
                for (int row = 0; row < Board.board.GetLength(1); row++) {
                    for (int col = 0; col < Board.board.GetLength(0); col++) {
                        int middlXOffset = Board.board.GetLength(1) * squareSize / 2;
                        int middlYOffset = Board.board.GetLength(0) * squareSize / 2;
                        int startX = (int)middle.X - middlXOffset + row * squareSize;
                        int startY = (int)middle.Y - middlYOffset + col * squareSize;
                        _spriteBatch.Draw(emptySquare, new Rectangle(startX, startY, squareSize, squareSize), Color.Black);
                        switch (Board.board[col, row]) {
                            case 0:
                                _spriteBatch.Draw(emptySquare, new Rectangle(startX, startY, squareSize - 2, squareSize - 2), Color.White);
                                break;
                            case 1:
                            case 6:
                                _spriteBatch.Draw(emptySquare, new Rectangle(startX, startY, squareSize - 2, squareSize - 2), Color.Black);
                                break;
                            case 2:
                                _spriteBatch.Draw(emptySquare, new Rectangle(startX, startY, squareSize - 2, squareSize - 2), Color.Red);
                                break;
                            case 3:
                                _spriteBatch.Draw(emptySquare, new Rectangle(startX, startY, squareSize - 2, squareSize - 2), Color.Green);
                                break;
                            case 4:
                                _spriteBatch.Draw(emptySquare, new Rectangle(+startX, startY, squareSize - 2, squareSize - 2), Color.Brown);
                                break;
                            case 5:
                                _spriteBatch.Draw(emptySquare, new Rectangle(+startX, startY, squareSize - 2, squareSize - 2), Color.Gold);
                                break;
                        }
                    }
                }
            }
            // TODO: Add your drawing code here
            String Moves = (isLevelEditor?"LEVEL EDITOR":"Moves: " + moves);
            String Level = (isLevelEditor?"LEVEL EDITOR":(levelType == LevelType.Generated?"Level: " + level:(levelType == LevelType.Custom?"Custom Level":"External Level")));
            _spriteBatch.DrawString(font, Moves, new Vector2(middle.X-((Moves.Length - 2)*6), ((middle.Y - Board.board.GetLength(0) * squareSize / 2 + 0 * squareSize)/2)-6), Color.Black,0f,Vector2.Zero,1f,SpriteEffects.None,0f);
            _spriteBatch.DrawString(font, Level, new Vector2(middle.X - ((Level.Length - 2) * 6), (GraphicsDevice.Viewport.Height-(squareSize/2))), Color.Black);//((middle.Y - Board.board.GetLength(0) * squareSize / 2 + Board.board.GetLength(0) * squareSize) + 6)), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

/// <summary>
/// Resolution
/// </summary>
public static class Resolution {
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
    public static void Update(GraphicsDeviceManager device) {
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
    public static Vector2 DetermineDrawScaling() {
        var x = _preferredBackBufferWidth / VirtualScreen.X;
        var y = _preferredBackBufferHeight / VirtualScreen.Y;
        return new Vector2(x, y);
    }
}

