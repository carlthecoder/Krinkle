using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace KrinklesHell.SpriteStuff
{
    public class SpriteManager : ISpriteManager
    {
        private readonly List<Sprite> _spriteList = new List<Sprite>();

        private SpriteBatch _spriteBatch;
        private Sprite _playerSprite;

        public void Initialize(Game game)
        {
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void Update(GameTime gameTime, Game game)
        {
            _playerSprite.Update(gameTime, game.Window.ClientBounds);

            foreach (var sprite in _spriteList)
            {
                sprite.Update(gameTime, game.Window.ClientBounds);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var sprite in _spriteList)
            {
                sprite.Draw(gameTime, _spriteBatch);
            }

            _playerSprite.Draw(gameTime, _spriteBatch);
        }

        public void AddSprite(Sprite sprite)
        {
            if (!_spriteList.Contains(sprite))
            {
                _spriteList.Add(sprite);
            }
        }

        public void RemoveSprite(Sprite sprite)
        {
            if (_spriteList.Contains(sprite))
            {
                _spriteList.Remove(sprite);
            }
        }

        public void SetPlayerSprite(Sprite sprite)
        {
            _playerSprite = sprite;
        }
    }
}