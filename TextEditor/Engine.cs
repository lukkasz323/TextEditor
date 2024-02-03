using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TextEditor;
public class Engine : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    public Engine()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        IsMouseVisible = true;
        Window.IsBorderless = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _font = Content.Load<SpriteFont>("font");

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

        _spriteBatch.DrawString(_font, "aha", Vector2.One, Color.Black);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
