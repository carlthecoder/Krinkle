using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KrinklesHell.Entities
{
    public class Tile : DrawableGameComponent
    {
        private const int TEXTURE_DIM = 80;

        private static readonly Vector2 TEXTURE_CENTER_OFFSET = new Vector2(TEXTURE_DIM / 2, TEXTURE_DIM / 2);

        private SpriteBatch _spriteBatch;
        private Texture2D _texture;

        private Color[] _pixels = new Color[TEXTURE_DIM * TEXTURE_DIM];
        private Vector2 _position;
        private Color _color;

        public Tile(Game game, Vector2 position, Color color)
            :
            base(game)
        {
            _position = position;
            _color = color;
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _texture = new Texture2D(Game.GraphicsDevice, TEXTURE_DIM, TEXTURE_DIM);

            DrawTile();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_texture, _position, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawTile()
        {
            var tileDim = TEXTURE_DIM;

            for (int col = 0; col < tileDim; col++)
            {
                for (int row = 0; row < tileDim; row++)
                {
                    DrawPixel(col, row, _color);
                }
            }

            _texture.SetData(_pixels);
        }

        public void DrawPixel(int x, int y, Color color)
        {
            var pixelIndex = (y * TEXTURE_DIM) + x;

            _pixels[pixelIndex] = color;
        }
    }
}