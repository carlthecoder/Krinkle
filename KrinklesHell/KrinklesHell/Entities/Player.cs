using KrinklesHell.SpriteStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Utilities;

namespace KrinklesHell.Entities
{
    public sealed class Player : GameComponent
    {
        private readonly PlayerSprite _playerSprite;
        private bool _shooting;
        public Rectangle CollisionRect => _playerSprite.CollisionRect;

        public Player(Game game)
            :
            base(game)
        {
            var xPos = 100f;
            var yPos = (Dimensions.WindowSize.Y * 0.9f) - 170;

            _playerSprite = new PlayerSprite(
                Game.Content.Load<Texture2D>("spritesheet_santa_24"),
                new Vector2(xPos, yPos),
                new Point(170, 234),
                60,
                new Point(0, 2),
                new Point(23, 3),
                new Vector2(0, 0),
                1.0f,
                0f);

            _playerSprite.SetFloorYPosition(yPos);

            if (_playerSprite != null)
            {
                Ioc.Resolve<ISpriteManager>().SetPlayerSprite(_playerSprite);
            }

            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                _shooting = true;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                if (_shooting)
                {
                    var clickPosition = mouseState.Position;

                    var xDirection = clickPosition.X - _playerSprite.Position.X;
                    var yDirection = clickPosition.Y - _playerSprite.Position.Y;

                    var directionVector = new Vector2(xDirection, yDirection);
                    directionVector.Normalize();

                    new FlameBullet(Game, _playerSprite.Position, directionVector);

                    _shooting = false;
                }
            }

            base.Update(gameTime);
        }
    }
}