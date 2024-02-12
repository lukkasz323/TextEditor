using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TextEditor.Core;

namespace TextEditor;

public class Engine : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Texture2D _blankTexture;
    private Color _backgroundColor = new Color(20, 20, 20);
    private readonly Editor _editor = new();

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
        Window.IsBorderless = false;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _font = Content.Load<SpriteFont>("font");
        _blankTexture = new Texture2D(GraphicsDevice, 1, 1);
        _blankTexture.SetData(new Color[] { Color.White });

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(20, 20, 20));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        //DrawOutline(4, 4, 32, 16, Color.White);
        //_spriteBatch.DrawString(_font, "File", new Vector2(6, 4), Color.Blue);
        //_spriteBatch.Draw(_font.Texture, new Rectangle(32, 32, _font.Texture.Width, _font.Texture.Height), Color.White);
        DrawDropdowns(0, 0);
        DrawFPS();

        _spriteBatch.End();

        base.Draw(gameTime);

        void DrawFill(int x, int y, int w, int h, Color color) => 
            _spriteBatch.Draw(_blankTexture, new Rectangle(x, y, w, h), color);

        void DrawOutline(int x, int y, int w, int h, Color color)
        {
            DrawFill(x, y, w, 1, color);
            DrawFill(x, y + h - 1, w, 1, color);
            DrawFill(x, y + 1, 1, h - 2, color);
            DrawFill(x + w - 1, y + 1, 1, h - 2, color);
        }

        void DrawDropdowns(int x, int y)
        {
            int widthOffset = 4;
            foreach (string label in _editor.Dropdowns)
            {
                Vector2 labelSize = _font.MeasureString(label);
                _spriteBatch.DrawString(_font, label, new Vector2(x + widthOffset, y), Color.White);

                widthOffset += (int)labelSize.X + 8;
            }
        }

        void DrawFPS()
        {
            _spriteBatch.DrawString(_font, $"{(int)(3600 * gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(700, 400), Color.Beige);
        }
    }
}
