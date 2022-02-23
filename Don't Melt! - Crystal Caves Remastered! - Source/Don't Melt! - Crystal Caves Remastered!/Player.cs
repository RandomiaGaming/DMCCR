using EpsilonEngine;
using System;
using System.Reflection;
namespace DMCCR
{
    public enum FacingDirection { Right, Left };
    public sealed class Player : PhysicsObject
    {
        private TextureRenderer _textureRenderer = null;

        private Texture _playerTextureRight = null;
        private Texture _playerTextureLeft = null;

        public FacingDirection FacingDirection = FacingDirection.Right;

        public const float JumpForce = 15f * 16f / 60f;
        public const float WallJumpForce = 15f * 16f / 60f;
        public const float MoveForce = 10f * 16f / 60f;
        public const float GravityForce = 9.8f * 16f * 1.5f / 60f / 60f;
        public Player(StagePlayer stagePlayer, PhysicsLayer physicsLayer, PhysicsLayer[] collsionPhysicsLayers) : base(stagePlayer, physicsLayer, false)
        {
            _playerTextureRight = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.PlayerRight.png"));
            _playerTextureLeft = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.PlayerLeft.png"));

            _textureRenderer = new TextureRenderer(this);

            _textureRenderer.Texture = _playerTextureRight;

            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 11, 11) });

            LogCollisionsUp = true;
            LogCollisionsRight = true;
            LogCollisionsDown = true;
            LogCollisionsLeft = true;

            MovePump.RegisterPumpEventUnsafe(CameraUpdate);

            CollisionPhysicsLayers = collsionPhysicsLayers;
        }
        private bool _leftMouseButtonPressedLastFrame = false;
        private bool _rightMouseButtonPressedLastFrame = false;
        private Point _reusablePoint = new Point(0, 0);
        private void CameraUpdate()
        {
            _reusablePoint.X = PositionX + 6 - (Scene.RenderWidth / 2);
            _reusablePoint.Y = PositionY + 6 - (Scene.RenderHeight / 2);
            Scene.CameraPosition = _reusablePoint;
        }
        protected override void Update()
        {
            Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            bool leftMouseButtonPressed = mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
            bool rightMouseButtonPressed = mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;

            bool leftMouseButtonDown = !_leftMouseButtonPressedLastFrame && leftMouseButtonPressed;
            bool rightMouseButtonDown = !_rightMouseButtonPressedLastFrame && rightMouseButtonPressed;

            bool Grounded = false;
            bool Walled = false;

            foreach (PhysicsObject physicsObject in _collisionsUp)
            {
                if (physicsObject.GetType() == typeof(Lava))
                {
                    Kill();
                    return;
                }
            }

            foreach (PhysicsObject physicsObject in _collisionsRight)
            {
                if (physicsObject.GetType() == typeof(Lava))
                {
                    Kill();
                   return;
                }
                if (physicsObject.GetType() == typeof(Ground))
                {
                    Walled = true;
                }
            }

            foreach (PhysicsObject physicsObject in _collisionsDown)
            {
                if (physicsObject.GetType() == typeof(Lava))
                {
                    Kill();
                    return;
                }
                if (physicsObject.GetType() == typeof(Ground))
                {
                    Grounded = true;
                }
            }

            foreach (PhysicsObject physicsObject in _collisionsLeft)
            {
                if (physicsObject.GetType() == typeof(Lava))
                {
                    Kill();
                    return;
                }
                if (physicsObject.GetType() == typeof(Ground))
                {
                    Walled = true;
                }
            }


            if (rightMouseButtonDown)
            {
                if (FacingDirection == FacingDirection.Right)
                {
                    FacingDirection = FacingDirection.Left;
                }
                else
                {
                    FacingDirection = FacingDirection.Right;
                }
            }

            if (leftMouseButtonDown)
            {
                if (Grounded)
                {
                    VelocityY = JumpForce;
                }
                else if (Walled)
                {
                    if (FacingDirection == FacingDirection.Right)
                    {
                        FacingDirection = FacingDirection.Left;
                    }
                    else
                    {
                        FacingDirection = FacingDirection.Right;
                    }

                    VelocityY = WallJumpForce;
                }
            }

            if (FacingDirection == FacingDirection.Right)
            {
                _textureRenderer.Texture = _playerTextureRight;
                VelocityX = MoveForce;
            }
            else
            {
                _textureRenderer.Texture = _playerTextureLeft;
                VelocityX = MoveForce * -1;
            }

            VelocityY -= GravityForce;

            _leftMouseButtonPressedLastFrame = leftMouseButtonPressed;
            _rightMouseButtonPressedLastFrame = rightMouseButtonPressed;
        }
        public override string ToString()
        {
            return $"DMCCR.Player()";
        }
        public void Kill()
        {
            PositionX = -1036;
            PositionY = -78 - 48;
            FacingDirection = FacingDirection.Right;
            VelocityX = 0;
            VelocityY = 0;
        }
    }
}







/*using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace EpsilonEngine
{
    public sealed class Player : GameObject
    {
        public float gravityForce = -0.00008f;
        private VirtualInput jumpVirtualInput = null;
        private VirtualInput rightVirtualInput = null;
        private VirtualInput leftVirtualInput = null;
        private VirtualInput upVirtualInput = null;
        private VirtualInput downVirtualInput = null;

        private Rigidbody _rigidbody = null;
        private float moveSpeed = 0.00005f;
        public Player(Scene stage, PhysicsManager physicsManager) : base(stage)
        {
            TextureRenderer textureRenderer = new TextureRenderer(this, new Texture(Engine, @"D:\C# Windows Apps\Epsilon\Epsilon - Source\Old Code\Default\Assets\Textures\Item Textures\LavaBubble.png"));
            _rigidbody = new Rigidbody(this, 0);
            Collider collider = new Collider(this, physicsManager, 1);
        }
        public override string ToString()
        {
            return $"Epsilon.Player({Position})";
        }
        protected override void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            jumpVirtualInput = Engine.InputManager.GetVirtualInputFromName("Jump");
            rightVirtualInput = Engine.InputManager.GetVirtualInputFromName("Right");
            leftVirtualInput = Engine.InputManager.GetVirtualInputFromName("Left");
            upVirtualInput = Engine.InputManager.GetVirtualInputFromName("Up");
            downVirtualInput = Engine.InputManager.GetVirtualInputFromName("Down");
        }
        protected override void Update()
        {
            float horizontalAxis = 0;

            if (rightVirtualInput.Pressed && !leftVirtualInput.Pressed)
            {
                horizontalAxis = 1;
            }
            else if (leftVirtualInput.Pressed && !rightVirtualInput.Pressed)
            {
                horizontalAxis = -1;
            }

            float vericalAxis = 0;

            if (upVirtualInput.Pressed && !downVirtualInput.Pressed)
            {
                vericalAxis = 1;
            }
            else if (downVirtualInput.Pressed && !upVirtualInput.Pressed)
            {
                vericalAxis = -1;
            }

            if (jumpVirtualInput.Pressed)
            {
                _rigidbody.velocity.Y = 0.1f;
            }

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.X + (horizontalAxis * moveSpeed), _rigidbody.velocity.Y + gravityForce);

            Scene.CameraPosition = Position - new Point(Scene.ViewPortSize.X / 2, Scene.ViewPortSize.Y / 2) + new Point(8, 8);
        }
    }
}
*/