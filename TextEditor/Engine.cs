using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TextEditor.Scene;

namespace TextEditor;

public class Engine : Game
{
    private static readonly IReadOnlyDictionary<Keys, char> s_charByKey = new Dictionary<Keys, char>
    {
        [Keys.A] = 'a',
        [Keys.B] = 'b',
        [Keys.C] = 'c',
        [Keys.D] = 'd',
        [Keys.E] = 'e',
        [Keys.F] = 'f',
        [Keys.G] = 'g',
        [Keys.H] = 'h',
        [Keys.I] = 'i',
        [Keys.J] = 'j',
        [Keys.K] = 'k',
        [Keys.L] = 'l',
        [Keys.M] = 'm',
        [Keys.N] = 'n',
        [Keys.O] = 'o',
        [Keys.P] = 'p',
        [Keys.Q] = 'q',
        [Keys.R] = 'r',
        [Keys.S] = 's',
        [Keys.T] = 't',
        [Keys.U] = 'u',
        [Keys.V] = 'v',
        [Keys.W] = 'w',
        [Keys.X] = 'x',
        [Keys.Y] = 'y',
        [Keys.Z] = 'z',
        [Keys.D1] = '1',
        [Keys.D2] = '2',
        [Keys.D3] = '3',
        [Keys.D4] = '4',
        [Keys.D5] = '5',
        [Keys.D6] = '6',
        [Keys.D7] = '7',
        [Keys.D8] = '8',
        [Keys.D9] = '9',
        [Keys.D0] = '0',
        [Keys.OemTilde] = '`',
        [Keys.OemComma] = ',',
        [Keys.OemPeriod] = '.',
    };
    private static readonly IReadOnlyDictionary<char, char> s_alternateCharByPrimaryChar = new Dictionary<char, char>
    {
        ['a'] = 'A',
        ['b'] = 'B',
        ['c'] = 'C',
        ['d'] = 'D',
        ['e'] = 'E',
        ['f'] = 'F',
        ['g'] = 'G',
        ['h'] = 'H',
        ['i'] = 'I',
        ['j'] = 'J',
        ['k'] = 'K',
        ['l'] = 'L',
        ['m'] = 'M',
        ['n'] = 'N',
        ['o'] = 'O',
        ['p'] = 'P',
        ['q'] = 'Q',
        ['r'] = 'R',
        ['s'] = 'S',
        ['t'] = 'T',
        ['u'] = 'U',
        ['v'] = 'V',
        ['w'] = 'W',
        ['x'] = 'X',
        ['y'] = 'Y',
        ['z'] = 'Z',
        ['1'] = '!',
        ['2'] = '@',
        ['3'] = '#',
        ['4'] = '$',
        ['5'] = '%',
        ['6'] = '^',
        ['7'] = '&',
        ['8'] = '*',
        ['9'] = '(',
        ['0'] = ')',
        ['`'] = '~',
        [','] = '<',
        ['.'] = '>',
    };

    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;
    private Texture2D _blankTexture;
    private Editor _editor;
    private Color _backgroundColor = new(20, 20, 20);

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
            Point origin = new(8, 3);
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
        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        if (keyboardState.IsKeyDown(Keys.Escape)) 
        { 
            Exit();
        }

        if (keyboardState.IsKeyDown(Keys.Enter)) 
        {
            _editor.Content.Append(Environment.NewLine);
        }

        foreach (Keys key in pressedKeys)
        {
            if (s_charByKey.ContainsKey(key))
            {
                if (keyboardState.IsKeyDown(Keys.LeftShift) && s_alternateCharByPrimaryChar.ContainsKey(s_charByKey[key]))
                {
                    if (keyboardState.CapsLock)
                    {
                        _editor.Content.Append(char.ToLower(s_alternateCharByPrimaryChar[s_charByKey[key]]));
                    }
                    else
                    {
                        _editor.Content.Append(s_alternateCharByPrimaryChar[s_charByKey[key]]);
                    }
                }
                else
                {
                    if (keyboardState.CapsLock)
                    {
                        _editor.Content.Append(char.ToUpper(s_charByKey[key]));
                    }
                    else
                    {
                        _editor.Content.Append(s_charByKey[key]);
                    }
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        Debug.WriteLine(_editor.Content);
        GraphicsDevice.Clear(_backgroundColor);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        DrawDetails();
        DrawEditorContent();
        DrawDropdowns();
        DrawFPS(new Vector2(Window.ClientBounds.Width - 24, Window.ClientBounds.Height - 20));

        _spriteBatch.End();

        base.Draw(gameTime);

        void DrawFill(Rectangle rect, Color color) => 
            _spriteBatch.Draw(_blankTexture, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height), color);

        void DrawOutline(Rectangle rect, Color color)
        {
            DrawFill(new Rectangle(rect.X, rect.Y, rect.Width, 1), color);
            DrawFill(new Rectangle(rect.X, rect.Y + rect.Height - 1, rect.Width, 1), color);
            DrawFill(new Rectangle(rect.X, rect.Y + 1, 1, rect.Height - 2), color);
            DrawFill(new Rectangle(rect.X + rect.Width - 1, rect.Y + 1, 1, rect.Height - 2), color);
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
            DrawFill(new Rectangle(0, 24, Window.ClientBounds.Width, 1), Color.White);
            DrawFill(new Rectangle(0, Window.ClientBounds.Height - 24, Window.ClientBounds.Width, 1), Color.White);
        }

        void DrawEditorContent()
        {
            _spriteBatch.DrawString(_font, _editor.Content, new Vector2(16, 36), Color.White);
        }

        void DrawFPS(Vector2 origin)
        {
            _spriteBatch.DrawString(_font, $"{(int)(3600 * gameTime.ElapsedGameTime.TotalSeconds)}", origin, Color.Beige);
        }
    }
}
