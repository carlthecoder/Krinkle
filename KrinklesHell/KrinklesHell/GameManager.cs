using KrinklesHell.Entities;
using KrinklesHell.Scenes;
using KrinklesHell.SpriteStuff;
using Microsoft.Xna.Framework;

namespace KrinklesHell
{
    public class GameManager : DrawableGameComponent
    {
        private readonly ISpriteManager _spriteManager = Ioc.Resolve<ISpriteManager>();

        private Player _player;
        private Scene1 _scene1;
        private CandyLauncher _candyLauncher;

        public GameManager(Game game)
            :
            base(game)
        {
        }

        public override void Initialize()
        {
            _spriteManager.Initialize(Game);

            // Todo: game should start in the PreGame state drawing a simple UI menu

            _player = new Player(Game);
            _scene1 = new Scene1(Game);
            _candyLauncher = new CandyLauncher();

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // Todo: Check game state and handle updates accordingly 

            _spriteManager.Update(gameTime, Game);
            _candyLauncher.Update(gameTime, Game, _player);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}