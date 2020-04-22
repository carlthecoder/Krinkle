using KrinklesHell.SpriteStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KrinklesHell.Entities
{
    public class FlameBullet : GameComponent
    {
        private Sprite _sprite;
        private ISpriteManager _spriteManager = Ioc.Resolve<ISpriteManager>();

        private float _speed = 20.0f;

        private Vector2 _position;
        private Vector2 _direction = Vector2.Zero;

        public FlameBullet(Game game, Vector2 position, Vector2 direction)
            :
            base(game)
        {
            _position = position;
            _direction = direction;

            _sprite = new Sprite(
                Game.Content.Load<Texture2D>("flame_bullet_16x16"),
                _position,
                new Point(16, 16),
                10,
                new Point(0, 0),
                new Point(0, 0),
                new Vector2(0,0),
                1.0f,
                0.0f);

            _spriteManager.AddSprite(_sprite);
            Game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            _position += _direction * _speed;

            CheckBounds();

            CopyValuesToSprite();

            base.Update(gameTime);
        }

        private void CheckBounds()
        {
            if (_position.X < 0 || _position.X >= Game.Window.ClientBounds.Width
                || _position.Y < 0 || _position.Y >= Game.Window.ClientBounds.Height)
            {
                _spriteManager.RemoveSprite(_sprite);
                Game.Components.Remove(this);
            }
        }

        private void CopyValuesToSprite()
        {
            _sprite.Position = _position;
        }
    }
}
