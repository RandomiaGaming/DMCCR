using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public class PhysicsObject : GameObject
    {
        #region Variables
        public List<PhysicsObject> _collisionsUp = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsRight = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsDown = new List<PhysicsObject>();
        public List<PhysicsObject> _collisionsLeft = new List<PhysicsObject>();
        public List<PhysicsObject> _overlaps = new List<PhysicsObject>();

        private Rectangle[] _localColliderShapes = new Rectangle[0];
        private Rectangle[] _worldColliderShapes = new Rectangle[0];
        #endregion
        #region Properties
        public PhysicsScene PhysicsScene { get; private set; } = null;
        public PhysicsLayer PhysicsLayer { get; private set; } = null;
        public PhysicsLayer[] CollisionPhysicsLayers { get; set; } = null;
        public bool IsStatic { get; private set; } = true;
        //Velocity is the objects move speed over time in pixels per frame.
        public float VelocityX { get; set; } = 0f;
        public float VelocityY { get; set; } = 0f;
        public Vector Velocity
        {
            get
            {
                return new Vector(VelocityX, VelocityY);
            }
            set
            {
                VelocityX = value.X;
                VelocityY = value.Y;
            }
        }
        //Subpixel stores how close the object is to moving another pixel.
        public float SubPixelX { get; private set; } = 0f;
        public float SubPixelY { get; private set; } = 0f;
        public Vector SubPixel
        {
            get
            {
                return new Vector(SubPixelX, SubPixelY);
            }
        }
        //Bounciness is the percentage of the objects velocity that is reflected in a collision. Negative values indicate a traditional bounce.
        public float BouncynessUp { get; set; } = 0f;
        public float BouncynessLeft { get; set; } = 0f;
        public float BouncynessRight { get; set; } = 0f;
        public float BouncynessDown { get; set; } = 0f;
        //LocalColliderRect stores the colliders local shape. This will not change with the objects position.
        public int LocalColliderBoundsMinX { get; private set; } = 0;
        public int LocalColliderBoundsMinY { get; private set; } = 0;
        public int LocalColliderBoundsMaxX { get; private set; } = 0;
        public int LocalColliderBoundsMaxY { get; private set; } = 0;
        public Rectangle LocalColliderBounds { get; private set; } = new Rectangle(0, 0, 0, 0);
        //WorldColliderRect stores the colliders world shape. This will change with the objects position.
        public int ColliderBoundsMinX { get; private set; } = 0;
        public int ColliderBoundsMinY { get; private set; } = 0;
        public int ColliderBoundsMaxX { get; private set; } = 0;
        public int ColliderBoundsMaxY { get; private set; } = 0;
        public Rectangle ColliderBounds { get; private set; } = new Rectangle(0, 0, 0, 0);
        //SolidSides stores on which side the object blocks other objects from moving.
        public bool SolidUp { get; set; } = true;
        public bool SolidDown { get; set; } = true;
        public bool SolidLeft { get; set; } = true;
        public bool SolidRight { get; set; } = true;
        public DirectionInfo SolidSides
        {
            get
            {
                return new DirectionInfo(SolidRight, SolidUp, SolidLeft, SolidDown);
            }
            set
            {
                SolidRight = value.Right;
                SolidUp = value.Up;
                SolidLeft = value.Left;
                SolidDown = value.Down;
            }
        }
        //PhaseThroughSides stores in which directions the object can phase through other solid colliders.
        public bool PhaseThroughUp { get; set; } = false;
        public bool PhaseThroughDown { get; set; } = false;
        public bool PhaseThroughLeft { get; set; } = false;
        public bool PhaseThroughRight { get; set; } = false;
        public DirectionInfo PhaseThroughDirections
        {
            get
            {
                return new DirectionInfo(PhaseThroughRight, PhaseThroughUp, PhaseThroughLeft, PhaseThroughDown);
            }
            set
            {
                PhaseThroughRight = value.Right;
                PhaseThroughUp = value.Up;
                PhaseThroughLeft = value.Left;
                PhaseThroughDown = value.Down;
            }
        }
        //PushableSides stores in which directions the object can be pushed.
        public bool PushableUp { get; set; } = false;
        public bool PushableDown { get; set; } = false;
        public bool PushableLeft { get; set; } = false;
        public bool PushableRight { get; set; } = false;
        public DirectionInfo PushableDirections
        {
            get
            {
                return new DirectionInfo(PushableRight, PushableUp, PushableLeft, PushableDown);
            }
            set
            {
                PushableRight = value.Right;
                PushableUp = value.Up;
                PushableLeft = value.Left;
                PushableDown = value.Down;
            }
        }
        //PushOthersSides stores in which directions the object will push others.
        public bool PushOthersUp { get; set; } = false;
        public bool PushOthersDown { get; set; } = false;
        public bool PushOthersLeft { get; set; } = false;
        public bool PushOthersRight { get; set; } = false;
        public DirectionInfo PushOthersDirections
        {
            get
            {
                return new DirectionInfo(PushOthersRight, PushOthersUp, PushOthersLeft, PushOthersDown);
            }
            set
            {
                PushOthersRight = value.Right;
                PushOthersUp = value.Up;
                PushOthersLeft = value.Left;
                PushOthersDown = value.Down;
            }
        }
        //LogCollisionSides stores in which direction we record collisions to the collision list.
        public bool LogCollisionsUp { get; set; } = false;
        public bool LogCollisionsDown { get; set; } = false;
        public bool LogCollisionsLeft { get; set; } = false;
        public bool LogCollisionsRight { get; set; } = false;
        public DirectionInfo LogCollisionSides
        {
            get
            {
                return new DirectionInfo(LogCollisionsRight, LogCollisionsUp, LogCollisionsLeft, LogCollisionsDown);
            }
            set
            {
                LogCollisionsRight = value.Right;
                LogCollisionsUp = value.Up;
                LogCollisionsLeft = value.Left;
                LogCollisionsDown = value.Down;
            }
        }
        //LogOverlaps stores weather or not this object logs overlaps to the overlap list.
        public bool LogOverlaps = false;
        //DragableSides stores on which sides the object can be dragged by other objects.
        public bool DragableUp { get; set; } = false;
        public bool DragableDown { get; set; } = false;
        public bool DragableLeft { get; set; } = false;
        public bool DragableRight { get; set; } = false;
        public DirectionInfo DragableSides
        {
            get
            {
                return new DirectionInfo(DragableRight, DragableUp, DragableLeft, DragableDown);
            }
            set
            {
                DragableRight = value.Right;
                DragableUp = value.Up;
                DragableLeft = value.Left;
                DragableDown = value.Down;
            }
        }
        //DragOthersSides stores on which sides the object will latch on to and drag other objects.
        public bool DragOthersUp { get; set; } = false;
        public bool DragOthersDown { get; set; } = false;
        public bool DragOthersLeft { get; set; } = false;
        public bool DragOthersRight { get; set; } = false;
        public DirectionInfo DragOthersSides
        {
            get
            {
                return new DirectionInfo(DragOthersRight, DragOthersUp, DragOthersLeft, DragOthersDown);
            }
            set
            {
                DragOthersRight = value.Right;
                DragOthersUp = value.Up;
                DragOthersLeft = value.Left;
                DragOthersDown = value.Down;
            }
        }
        #endregion
        #region Constructors
        public PhysicsObject(PhysicsScene physicsScene, PhysicsLayer physicsLayer, bool isStatic) : base(physicsScene)
        {
            if (physicsScene is null)
            {
                throw new Exception("physicsScene cannot be null.");
            }
            PhysicsScene = physicsScene;

            if (physicsLayer is null)
            {
                throw new Exception("physicsLayer cannot be null.");
            }
            if (physicsLayer.PhysicsScene != physicsScene)
            {
                throw new Exception("physicsScene cannot be null.");
            }
            PhysicsLayer = physicsLayer;

            IsStatic = isStatic;

            if (!IsStatic)
            {
                Game.UpdatePump.RegisterPumpEventUnsafe(PhysicsUpdate, 0);
            }

            PhysicsScene.AddPhysicsObject(this);

            PhysicsLayer.AddPhysicsObject(this);

            MovePump.RegisterPumpEventUnsafe(RecalculateOnMove);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsObject({ColliderBounds})";
        }
        #endregion
        #region Methods
        private bool _moving = false;
        public void SetColliderShape(Rectangle[] colliderShape)
        {
            if (colliderShape is null)
            {
                throw new Exception("colliderShape cannot be null.");
            }

            int shapesLength = colliderShape.Length;

            _localColliderShapes = new Rectangle[shapesLength];
            _worldColliderShapes = new Rectangle[shapesLength];

            LocalColliderBoundsMinX = int.MaxValue;
            LocalColliderBoundsMinY = int.MaxValue;
            LocalColliderBoundsMaxX = int.MinValue;
            LocalColliderBoundsMaxY = int.MinValue;


            for (int i = 0; i < shapesLength; i++)
            {
                Rectangle safeLocalShape = colliderShape[i];

                _localColliderShapes[i] = new Rectangle(safeLocalShape.MinX, safeLocalShape.MinY, safeLocalShape.MaxX, safeLocalShape.MaxY);
                _worldColliderShapes[i] = new Rectangle(safeLocalShape.MinX + PositionX, safeLocalShape.MinY + PositionY, safeLocalShape.MaxX + PositionX, safeLocalShape.MaxY + PositionY);

                if (safeLocalShape.MinX < LocalColliderBoundsMinX)
                {
                    LocalColliderBoundsMinX = safeLocalShape.MinX;
                }
                if (safeLocalShape.MinY < LocalColliderBoundsMinY)
                {
                    LocalColliderBoundsMinY = safeLocalShape.MinY;
                }
                if (safeLocalShape.MaxX > LocalColliderBoundsMaxX)
                {
                    LocalColliderBoundsMaxX = safeLocalShape.MaxX;
                }
                if (safeLocalShape.MaxY > LocalColliderBoundsMaxY)
                {
                    LocalColliderBoundsMaxY = safeLocalShape.MaxY;
                }
            }
            LocalColliderBounds = new Rectangle(LocalColliderBoundsMinX, LocalColliderBoundsMinY, LocalColliderBoundsMaxX, LocalColliderBoundsMaxY);

            ColliderBoundsMinX = LocalColliderBoundsMinX + PositionX;
            ColliderBoundsMinY = LocalColliderBoundsMinY + PositionY;
            ColliderBoundsMaxX = LocalColliderBoundsMaxX + PositionX;
            ColliderBoundsMaxY = LocalColliderBoundsMaxY + PositionY;
            ColliderBounds = new Rectangle(ColliderBoundsMinX, ColliderBoundsMinY, ColliderBoundsMaxX, ColliderBoundsMaxY);
        }
        public Rectangle[] GetColliderShape()
        {
            return (Rectangle[])_localColliderShapes.Clone();
        }
        public Point PhysicsMove(Point moveDistance)
        {
            int outputX;
            if (moveDistance.X < 0)
            {
                outputX = PhysicsMoveLeftUnsafe(moveDistance.X * -1);
            }
            else
            {
                outputX = PhysicsMoveRightUnsafe(moveDistance.X);
            }

            int outputY;
            if (moveDistance.Y < 0)
            {
                outputY = PhysicsMoveLeftUnsafe(moveDistance.Y * -1);
            }
            else
            {
                outputY = PhysicsMoveRightUnsafe(moveDistance.Y);
            }

            moveDistance.X = outputX;
            moveDistance.Y = outputY;
            return moveDistance;
        }

        public int PhysicsMoveXAxis(int moveDistance)
        {
            if (moveDistance == 0)
            {
                return 0;
            }
            else if (moveDistance < 0)
            {
                return PhysicsMoveLeftUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveRightUnsafe(moveDistance);
            }
        }
        public int PhysicsMoveYAxis(int moveDistance)
        {
            if (moveDistance == 0)
            {
                return 0;
            }
            else if (moveDistance < 0)
            {
                return PhysicsMoveDownUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveUpUnsafe(moveDistance);
            }
        }

        public int PhysicsMoveUp(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveUpUnsafe(moveDistance);
        }
        public int PhysicsMoveDown(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveDownUnsafe(moveDistance);
        }
        public int PhysicsMoveRight(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveRightUnsafe(moveDistance);
        }
        public int PhysicsMoveLeft(int moveDistance)
        {
            if (moveDistance < 0)
            {
                throw new Exception("moveDistance must be greater than or equal to 0.");
            }
            return PhysicsMoveLeftUnsafe(moveDistance);
        }



        public int PhysicsMoveXAxisUnsafe(int moveDistance)
        {
            if (moveDistance < 0)
            {
                return PhysicsMoveLeftUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveRightUnsafe(moveDistance);
            }
        }
        public int PhysicsMoveYAxisUnsafe(int moveDistance)
        {
            if (moveDistance < 0)
            {
                return PhysicsMoveDownUnsafe(moveDistance * -1);
            }
            else
            {
                return PhysicsMoveUpUnsafe(moveDistance);
            }
        }

        public int PhysicsMoveUpUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            if (CollisionPhysicsLayers is null || PhaseThroughUp)
            {
                PositionY += moveDistance;
                return moveDistance;
            }

            _moving = true;

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidDown)
                    {
                        if (collisionPhysicsObject.ColliderBoundsMinX > ColliderBoundsMaxX || collisionPhysicsObject.ColliderBoundsMaxX < ColliderBoundsMinX)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMaxY < ColliderBoundsMinY)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMinY > ColliderBoundsMaxY)
                        {
                            foreach (Rectangle thisColliderShape in _worldColliderShapes)
                            {
                                foreach (Rectangle otherColliderShape in collisionPhysicsObject._worldColliderShapes)
                                {

                                }
                            }
                            //This physics object must be in front of us.
                            int maxMove = collisionPhysicsObject.ColliderBoundsMinY - ColliderBoundsMaxY - 1;

                            if (maxMove < moveDistance && PushOthersUp && collisionPhysicsObject.PushableUp)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collisionPhysicsObject.PhysicsMoveUpUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionY += moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveRightUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughRight)
            {
                PositionX += moveDistance;
                _moving = false;
                return moveDistance;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidLeft)
                    {
                        if (collisionPhysicsObject.ColliderBoundsMinY > ColliderBoundsMaxY || collisionPhysicsObject.ColliderBoundsMaxY < ColliderBoundsMinY)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMaxX < ColliderBoundsMinX)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMinX > ColliderBoundsMaxX)
                        {
                            //This physics object must be in front of us.
                            int maxMove = collisionPhysicsObject.ColliderBoundsMinX - ColliderBoundsMaxX - 1;

                            if (maxMove < moveDistance && PushOthersRight && collisionPhysicsObject.PushableRight)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collisionPhysicsObject.PhysicsMoveRightUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionX += moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveDownUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughDown)
            {
                PositionY -= moveDistance;
                _moving = false;
                return moveDistance;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidUp)
                    {
                        if (collisionPhysicsObject.ColliderBoundsMinX > ColliderBoundsMaxX || collisionPhysicsObject.ColliderBoundsMaxX < ColliderBoundsMinX)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMinY > ColliderBoundsMaxY)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMaxY < ColliderBoundsMinY)
                        {
                            //This physics object must be in front of us.
                            int maxMove = ColliderBoundsMinY - collisionPhysicsObject.ColliderBoundsMaxY - 1;

                            if (maxMove < moveDistance && PushOthersDown && collisionPhysicsObject.PushableDown)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collisionPhysicsObject.PhysicsMoveDownUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionY -= moveDistance;
            return moveDistance;
        }
        public int PhysicsMoveLeftUnsafe(int moveDistance)
        {
            if (_moving)
            {
                return 0;
            }

            _moving = true;

            if (CollisionPhysicsLayers is null || PhaseThroughLeft)
            {
                PositionX -= moveDistance;
                _moving = false;
                return moveDistance;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidRight)
                    {
                        if (collisionPhysicsObject.ColliderBoundsMinY > ColliderBoundsMaxY || collisionPhysicsObject.ColliderBoundsMaxY < ColliderBoundsMinY)
                        {
                            //Ignore this physics object because it is too far to the left or right for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMinX > ColliderBoundsMaxX)
                        {
                            //Ignore this physics object because it is too below us for a collision.
                        }
                        else if (collisionPhysicsObject.ColliderBoundsMaxX < ColliderBoundsMinX)
                        {
                            //This physics object must be in front of us.
                            int maxMove = ColliderBoundsMinX - collisionPhysicsObject.ColliderBoundsMaxX - 1;

                            if (maxMove < moveDistance && PushOthersLeft && collisionPhysicsObject.PushableLeft)
                            {
                                int pushRequest = moveDistance - maxMove;
                                int pushDistance = collisionPhysicsObject.PhysicsMoveLeftUnsafe(pushRequest);
                                maxMove = maxMove + pushDistance;
                            }

                            if (maxMove > moveDistance)
                            {
                                //Ignore this collision because the object we hit is too far in front.
                            }
                            else
                            {
                                //Set the target move to the maximun and zero out the velocity due to a collision.
                                moveDistance = maxMove;
                            }
                        }
                        else
                        {
                            //Set target move to 0, and zero out the velocity because there is an object overlapping us.
                            moveDistance = 0;
                        }
                    }
                }
            }

            _moving = false;

            PositionX -= moveDistance;
            return moveDistance;
        }
        #endregion
        #region Internals
        private void RecalculateOnMove()
        {
            int localColliderShapesLength = _localColliderShapes.Length;
            for (int i = 0; i < localColliderShapesLength; i++)
            {
                Rectangle localShape = _localColliderShapes[i];
                Rectangle worldShape = _worldColliderShapes[i];
                worldShape._minX = PositionX + localShape.MinX;
                worldShape._minY = PositionY + localShape.MinY;
                worldShape._maxX = PositionX + localShape.MaxX;
                worldShape._maxY = PositionY + localShape.MaxY;

                worldShape.Width = worldShape._maxX - worldShape._minX + 1;
                worldShape.Height = worldShape._maxY - worldShape._minY + 1;

                worldShape.Size._x = worldShape.Width;
                worldShape.Size._y = worldShape.Height;

            }
            ColliderBoundsMinX = PositionX + LocalColliderBoundsMinX;
            ColliderBoundsMinY = PositionY + LocalColliderBoundsMinY;
            ColliderBoundsMaxX = PositionX + LocalColliderBoundsMaxX;
            ColliderBoundsMaxY = PositionY + LocalColliderBoundsMaxY;
            ColliderBounds = new Rectangle(ColliderBoundsMinX, ColliderBoundsMinY, ColliderBoundsMaxX, ColliderBoundsMaxY);
        }
        private void PhysicsUpdate()
        {
            if (VelocityY != 0)
            {
                SubPixelY += VelocityY;
                int targetMoveY = (int)SubPixelY;

                if (targetMoveY != 0)
                {
                    SubPixelY -= targetMoveY;

                    if (targetMoveY < 0)
                    {
                        if (PhysicsMoveDownUnsafe(targetMoveY * -1) != targetMoveY * -1)
                        {
                            VelocityY *= BouncynessDown;
                        }
                    }
                    else
                    {
                        if (PhysicsMoveUpUnsafe(targetMoveY) != targetMoveY)
                        {
                            VelocityY *= BouncynessUp;
                        }
                    }
                }
            }

            if (VelocityX != 0)
            {
                SubPixelX += VelocityX;
                int targetMoveX = (int)SubPixelX;

                if (targetMoveX != 0)
                {
                    SubPixelX -= targetMoveX;

                    if (targetMoveX < 0)
                    {
                        if (PhysicsMoveLeftUnsafe(targetMoveX * -1) != targetMoveX * -1)
                        {
                            VelocityX *= BouncynessLeft;
                        }
                    }
                    else
                    {
                        if (PhysicsMoveRightUnsafe(targetMoveX) != targetMoveX)
                        {
                            VelocityX *= BouncynessRight;
                        }
                    }
                }
            }

            if (LogCollisionsUp)
            {
                CheckCollisionsUp();
            }
            if (LogCollisionsRight)
            {
                CheckCollisionsRight();
            }
            if (LogCollisionsDown)
            {
                CheckCollisionsDown();
            }
            if (LogCollisionsLeft)
            {
                CheckCollisionsLeft();
            }
            if (LogOverlaps)
            {
                CheckOverlaps();
            }
        }

        private void CheckOverlaps()
        {
            _overlaps = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || (PhaseThroughUp && PhaseThroughDown && PhaseThroughLeft && PhaseThroughRight))
            {
                return;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && (collisionPhysicsObject.SolidDown || collisionPhysicsObject.SolidUp || collisionPhysicsObject.SolidLeft || collisionPhysicsObject.SolidRight) && collisionPhysicsObject.ColliderBounds.Overlaps(ColliderBounds))
                    {
                        _overlaps.Add(collisionPhysicsObject);
                    }
                }
            }
        }
        private void CheckCollisionsUp()
        {
            _collisionsUp = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughUp)
            {
                return;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidDown && collisionPhysicsObject.ColliderBoundsMinX <= ColliderBoundsMaxX && collisionPhysicsObject.ColliderBoundsMaxX >= ColliderBoundsMinX && collisionPhysicsObject.ColliderBoundsMinY == ColliderBoundsMaxY + 1)
                    {
                        _collisionsUp.Add(collisionPhysicsObject);
                    }
                }
            }
        }
        private void CheckCollisionsRight()
        {
            _collisionsRight = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughRight)
            {
                return;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidDown && collisionPhysicsObject.ColliderBoundsMinY <= ColliderBoundsMaxY && collisionPhysicsObject.ColliderBoundsMaxY >= ColliderBoundsMinY && collisionPhysicsObject.ColliderBoundsMinX == ColliderBoundsMaxX + 1)
                    {
                        _collisionsRight.Add(collisionPhysicsObject);
                    }
                }
            }
        }
        private void CheckCollisionsDown()
        {
            _collisionsDown = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughDown)
            {
                return;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidDown && collisionPhysicsObject.ColliderBoundsMinX <= ColliderBoundsMaxX && collisionPhysicsObject.ColliderBoundsMaxX >= ColliderBoundsMinX && collisionPhysicsObject.ColliderBoundsMaxY + 1 == ColliderBoundsMinY)
                    {
                        _collisionsDown.Add(collisionPhysicsObject);
                    }
                }
            }
        }
        private void CheckCollisionsLeft()
        {
            _collisionsLeft = new List<PhysicsObject>();

            if (CollisionPhysicsLayers is null || PhaseThroughLeft)
            {
                return;
            }

            foreach (PhysicsLayer collisionPhysicsLayer in CollisionPhysicsLayers)
            {
                foreach (PhysicsObject collisionPhysicsObject in collisionPhysicsLayer._physicsObjectCache)
                {
                    if (collisionPhysicsObject != this && collisionPhysicsObject.SolidDown && collisionPhysicsObject.ColliderBoundsMinY <= ColliderBoundsMaxY && collisionPhysicsObject.ColliderBoundsMaxY >= ColliderBoundsMinY && collisionPhysicsObject.ColliderBoundsMaxX + 1 == ColliderBoundsMinX)
                    {
                        _collisionsLeft.Add(collisionPhysicsObject);
                    }
                }
            }
        }
        #endregion
    }
}