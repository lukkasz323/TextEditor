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
            int widthSpacing = 8;
            int widthOffset = 0;
            foreach (Button button in _editor.Dropdowns)
            {
                button.Body = new Rectangle(widthOffset, button.Body.Y, (int)button.TextSize.X + widthSpacing, (int)button.TextSize.Y);

                widthOffset += (int)button.TextSize.X + widthSpacing;
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

        //DrawOutline(4, 4, 32, 16, Color.White);
        //_spriteBatch.DrawString(_font, "File", new Vector2(6, 4), Color.Blue);
        //_spriteBatch.Draw(_font.Texture, new Rectangle(32, 32, _font.Texture.Width, _font.Texture.Height), Color.White);
        DrawDropdowns();
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

        void DrawDropdowns()
        {
            //int widthOffset = 4;
            //foreach (string label in _editor.Dropdowns)
            //{
            //    Vector2 labelSize = _font.MeasureString(label);
            //    _spriteBatch.DrawString(_font, label, new Vector2(x + widthOffset, y), Color.White);

            //    widthOffset += (int)labelSize.X + 8;
            //}
            foreach (Button button in _editor.Dropdowns)
            {
                _spriteBatch.DrawString(_font, button.Text, new Vector2((int)button.Body.X + 4, (int)button.Body.Y), Color.White);
                if (button.IsHovered)
                {
                    DrawOutline(button.Body.X, button.Body.Y, button.Body.Width, button.Body.Height, Color.White);
                }
            }
        }

        void DrawFPS()
        {
            _spriteBatch.DrawString(_font, $"{(int)(3600 * gameTime.ElapsedGameTime.TotalSeconds)}", new Vector2(700, 400), Color.Beige);
        }
    }
}
