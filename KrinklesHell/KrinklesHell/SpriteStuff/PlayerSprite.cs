using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KrinklesHell.SpriteStuff
{
    public class PlayerSprite : Sprite
    {
        private bool _onground;
        private float _velocityY;
        private float _velocityX;
        private float _jumpForce = 25.0f;
        private float _initialFallingVelocity = 3;
        private float _gravity = 0.75f;
        private float _playerOnFloorYCoordinate;
        private bool _isFacingRight = true;

        public PlayerSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float scale, float rotation)
            :
            base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, scale, rotation)
        {
        }

        public PlayerSprite(Texture2D textureImage, Vector2 position, Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, float scale, float rotation, int millisecondsPerFrame)
            :
            base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, scale, rotation, millisecondsPerFrame)
        {
        }

        public override void Update(GameTime gameTime, Rectangle ClientBounds)
        {
            HandleInput();

            _velocityY += _gravity;
            _position.Y += _velocityY;
            _position.X += _velocityX;

            if (_position.Y > _playerOnFloorYCoordinate)
            {
                _position.Y = _playerOnFloorYCoordinate;
                _velocityY = 0;
                _onground = true;
            }

            CheckSpriteInsideWindow(ClientBounds);
        }

        public void SetFloorYPosition(float yPos)
        {
            _playerOnFloorYCoordinate = yPos;
        }

        private void HandleInput()
        {
            _velocityX = 0;

            if (_isFacingRight)
            {
                if (_currentFrame.Y != 2)
                {
                    _currentFrame.Y = 2;
                }

                if (_currentFrame.X >= _sheetSize.X)
                {
                    _currentFrame.X = 0;
                }

                if (_onground)
                {
                    _currentFrame.X++;
                }
            }
            else
            {
                if (_currentFrame.Y != 3)
                {
                    _currentFrame.Y = 3;
                }

                if (_onground)
                {
                    _currentFrame.X--;
                }

                if (_currentFrame.X <= 0)
                {
                    _currentFrame.X = _sheetSize.X;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _isFacingRight = false;
                _velocityX -= 7;

                if (_currentFrame.Y != 1)
                {
                    _currentFrame.Y = 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _isFacingRight = true;
                _velocityX += 7;

                if (_currentFrame.Y != 0)
                {
                    _currentFrame.Y = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                StartJump();
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                EndJump();
            }
        }

        private void StartJump()
        {
            if (_onground)
            {
                _velocityY = -_jumpForce;
                _onground = false;
            }
        }

        private void EndJump()
        {
            if (!_onground && _velocityY < _initialFallingVelocity)
            {
                _velocityY = _initialFallingVelocity;
            }            
        }

        private void CheckSpriteInsideWindow(Rectangle ClientBounds)
        {
            if (_position.X < 0)
                _position.X = 0;

            if (_position.Y < 0)
                _position.Y = 0;

            if (_position.X >= ClientBounds.Width - _frameSize.X)
                _position.X = ClientBounds.Width - _frameSize.X;

            if (_position.Y >= ClientBounds.Height - _frameSize.Y)
                _position.Y = ClientBounds.Height - _frameSize.Y;
        }
    }
}