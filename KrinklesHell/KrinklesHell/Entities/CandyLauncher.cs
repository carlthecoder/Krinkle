using Microsoft.Xna.Framework;
using System;

namespace KrinklesHell.Entities
{
    public class CandyLauncher
    {
        private const int CANDY_MIN_INTERVAL = 500;
        private const int CANDY_MAX_INTERVAL = 2500;
        private const int CANDY_AMOUNT = 50;

        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        private int _candyCounter;
        private int _candyTimer;
        private int _candyInterval = 1000;

        public void Update(GameTime gameTime, Game game, Player player)
        {
            _candyTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (_candyTimer >= _candyInterval)
            {
                _candyTimer = 0;
                _candyInterval = _random.Next(CANDY_MIN_INTERVAL, CANDY_MAX_INTERVAL);

                if (_candyCounter <= CANDY_AMOUNT)
                {
                    _candyCounter++;

                    GenerateCandyCane(game, player);
                }
            }
        }

        private void GenerateCandyCane(Game game, Player player)
        {
            var xPosition = _random.Next(10, 1792);
            var ySpeed = _random.Next(2, 7);
            var scale = _random.Next(3, 6) / 10.0f;
            var rotation = _random.Next(0, 3) / 10.0f;

            new CandyCane(game, player, new Vector2(xPosition, -200), new Vector2(0, ySpeed), scale, rotation);
        }
    }
}