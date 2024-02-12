using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TextEditor.Scene;

namespace TextEditor;

public class Engine : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Texture2D _blankTexture;
    private Editor _editor;
    private Color _backgroundColor = new Color(20, 20, 20);

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
        _editor = new(_font);

        _blankTexture = new Texture2D(GraphicsDevice, 1, 1);
        _blankTexture.SetData(new Color[] { Color.White });

        InitEditor();

        base.Initialize();

        void InitEditor()
        {
            Point origin = new(8, 8);
            int textSpacing = 8;
            int widthSpacing = 8;
            int widthOffset = 0;
            foreach (Button button in _editor.Dropdowns)
            {
                button.Body = new Rectangle(widthOffset + origin.X, button.Body.Y + origin.Y, (int)button.TextSize.X + widthSpacing, (int)button.TextSize.Y);

                widthOffset += (int)button.TextSize.X + textSpacing + widthSpacing;
            }
        }
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Escape)) { Exit(); }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(20, 20, 20));

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        DrawDropdowns();
        DrawFPS(new Vector2(Window.ClientBounds.Width - 24, Window.ClientBounds.Height - 20));

        _spriteBatch.End();

        base.Draw(gameTime);

        void DrawFill(Rectangle rect, Color color) => 
            _spriteBatch.Draw(_blankTexture, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), color);

        void DrawOutline(Rectangle rect, Color color)
        {
            DrawFill(rect.X, rect.Y, rect.Width, 1, color);
            DrawFill(rect.X, rect.Y + rect.Height - 1, rect.Width, 1, color);
            DrawFill(rect.X, rect.Y + 1, 1, rect.Height - 2, color);
            DrawFill(rect.X + rect.Width - 1, rect.Y + 1, 1, rect.Height - 2, color);
        }

        void DrawDropdowns()
        {
            foreach (Button button in _editor.Dropdowns)
            {
                _spriteBatch.DrawString(_font, button.Text, new Vector2(button.Body.X + 4, button.Body.Y), Color.White);
                if (button.IsHovered)
                {
                    DrawOutline(button.Body, Color.White);
                }
            }
        }

        void DrawDetails()
        {
            DrawFill()
        }

        void DrawFPS(Vector2 origin)
        {
            _spriteBatch.DrawString(_font, $"{(int)(3600 * gameTime.ElapsedGameTime.TotalSeconds)}", origin, Color.Beige);
        }
    }
}
