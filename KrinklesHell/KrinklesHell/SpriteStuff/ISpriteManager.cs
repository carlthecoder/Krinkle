using Microsoft.Xna.Framework;

namespace KrinklesHell.SpriteStuff
{
    public interface ISpriteManager
    {
        void Initialize(Game game);

        void AddSprite(Sprite sprite);

        void RemoveSprite(Sprite sprite);

        void SetPlayerSprite(Sprite sprite);

        void Update(GameTime gameTime, Game game);

        void Draw(GameTime gameTime);
    }
}
