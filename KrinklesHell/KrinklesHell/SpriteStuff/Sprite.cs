using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KrinklesHell.SpriteStuff
{
    /// <summary>
    /// The basic sprite class.
    /// This class provides the basic set of fields common to all sprites.
    /// It can be used for still sprites as well as sequentially laid out animated sprite sheets
    /// It's purpose is to draw the sprite to the screen, when called by the SpriteManager.
    /// </summary>
    public class Sprite
    {
        private const int FRAME_MS = 16;

        protected readonly Texture2D _texture;
        protected readonly int _collisionOffset;
        protected readonly int _millieSecondsPerFrame;

        protected Vector2 _position;
        protected Vector2 _speed;
        protected float _rotation;
        protected float _scale;
        protected Point _frameSize;
        protected Point _currentFrame;
        protected Point _sheetSize;
        protected int _timeSinceLastFrame;

        public float Scale
        {
            get => _scale;
            set
            {
                if (_scale == value)
                {
                    return;
                }
                _scale = value;
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                if (_position == value)
                {
                    return;
                }
                _position = value;
            }
        }

        public Vector2 Speed
        {
            get => _speed;
            set
            {
                if (_speed == value)
                {
                    return;
                }

                _speed = value;
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                if (_rotation == value)
                {
                    return;
                }

                _rotation = value;
            }
        }

        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)_position.X + _collisionOffset,
                    (int)_position.Y + _collisionOffset,
                    (int)_frameSize.X - (_collisionOffset * 2),
                    (int)_frameSize.Y - (_collisionOffset * 2));
            }
        }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float scale, float rotation)
            :
            this(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, scale, rotation, FRAME_MS)
        { }

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float scale, float rotation, int millisecondsPerFrame)
        {
            _texture = textureImage;
            _position = position;
            _frameSize = frameSize;
            _collisionOffset = collisionOffset;
            _currentFrame = currentFrame;
            _sheetSize = sheetSize;
            _speed = speed;
            _scale = scale;
            _rotation = rotation;
            _millieSecondsPerFrame = millisecondsPerFrame;
        }
      
        public virtual void Update(GameTime gameTime, Rectangle ClientBounds)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _millieSecondsPerFrame)
            {
                _timeSinceLastFrame = 0;
                _currentFrame.X++;

                if (_currentFrame.X >= _sheetSize.X)
                {
                    _currentFrame.X = 0;
                    _currentFrame.Y++;
                }

                if (_currentFrame.Y >= _sheetSize.Y)
                {
                    _currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(
                _texture,
                _position,
                new Rectangle(_currentFrame.X * _frameSize.X, _currentFrame.Y * _frameSize.Y, _frameSize.X, _frameSize.Y),
                Color.White,
                _rotation,
                Vector2.Zero,
                _scale,
                SpriteEffects.None,
                0);

            spriteBatch.End();
        }
    }
}