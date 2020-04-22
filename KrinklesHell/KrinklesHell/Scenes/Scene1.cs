using KrinklesHell.SpriteStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities;

namespace KrinklesHell.Scenes
{
    public class Scene1
    {
        private readonly ISpriteManager _spriteManager = Ioc.Resolve<ISpriteManager>();

        private Vector2 _floorPosition;
        private Vector2 _mountainPosition;

        private Sprite _floor;
        private Sprite _mountains;
        private Sprite _sky;

        private Game _game;

        public Scene1(Game game)
        {
            _game = game;

            _floorPosition = new Vector2(0, Dimensions.WindowSize.Y * 0.9f);
            _mountainPosition = new Vector2(0, Dimensions.WindowSize.Y * 0.7f);
            CreateScene(game);
        }

        public void RemoveScene()
        {
            _spriteManager.AddSprite(_sky);
            _spriteManager.AddSprite(_mountains);
            _spriteManager.AddSprite(_floor);
        }

        private void CreateScene(Game game)
        {
            _floor = new Sprite(
                game.Content.Load<Texture2D>("floor_yellow_brown_1920"),
                _floorPosition,
                new Point(1920, 138),
                10,
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                1,
                0);
            
            _mountains = new Sprite(
                game.Content.Load<Texture2D>("snowymountain_1920x360"),
                _mountainPosition,
                new Point(1920, 360),
                10,
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                1,
                0);

            _sky = new Sprite(
                game.Content.Load<Texture2D>("bluesky_1920x1080"),
                new Vector2(0, 0),
                new Point(1920, 1080),
                10,
                new Point(0, 0),
                new Point(0, 0),
                Vector2.Zero,
                1,
                0);

            _spriteManager.AddSprite(_sky);
            _spriteManager.AddSprite(_mountains);
            _spriteManager.AddSprite(_floor);
        }
    }
}