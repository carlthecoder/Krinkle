using KrinklesHell.SpriteStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KrinklesHell.Entities
{
    public sealed class CandyCane : GameComponent
    {
        private readonly ISpriteManager _spriteManager = Ioc.Resolve<ISpriteManager>();

        private Player _player;
        private Sprite _candyCaneSprite;

        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public CandyCane(Game game, Player player, Vector2 position, Vector2 speed, float scale, float rotation)
            :
            base(game)
        {
            _player = player;

            Position = position;
            Speed = speed;
            Scale = scale;
            Rotation = rotation;

            _candyCaneSprite = new Sprite(
                Game.Content.Load<Texture2D>("candycane"),
                Position,
                new Point(128, 128),
                10,
                new Point(0, 4),
                new Point(0, 0),
                Speed,
                Scale,
                Rotation);

            _spriteManager.AddSprite(_candyCaneSprite);

            game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;

            CheckPlayerCollisionAndBounds();

            CopyValuesToSprite();

            base.Update(gameTime);
        }

        private void CheckPlayerCollisionAndBounds()
        {
            if (Position.Y > Game.Window.ClientBounds.Height
                || _candyCaneSprite.CollisionRect.Intersects(_player.CollisionRect))
            {
                Game.Components.Remove(this);
                _spriteManager.RemoveSprite(_candyCaneSprite);
            }
        }

        private void CopyValuesToSprite()
        {
            _candyCaneSprite.Rotation += Rotation;
            _candyCaneSprite.Speed = Speed;
            _candyCaneSprite.Scale = Scale;
            _candyCaneSprite.Position = Position;
        }
    }
}