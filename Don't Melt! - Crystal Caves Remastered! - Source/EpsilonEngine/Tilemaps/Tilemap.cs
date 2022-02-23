using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public struct TileData
    {
        public Tile Tile;
        public Point Position;
        public TileData(Tile tile, Point position)
        {
            Tile = tile;
            Position = position;
        }
    }
    public class Tilemap : PhysicsObject
    {
        private Microsoft.Xna.Framework.Graphics.Texture2D _tilemapRender = null;
        private Microsoft.Xna.Framework.Vector2 _XNAPositionCache = Microsoft.Xna.Framework.Vector2.Zero;
        private int TilemapBoundsMinX = 0;
        private int TilemapBoundsMinY = 0;
        private int TilemapBoundsMaxX = 0;
        private int TilemapBoundsMaxY = 0;
        private List<Rectangle> tilemapColliderShape = new List<Rectangle>();
        private List<TileData> tiles = new List<TileData>();
        public Tilemap(PhysicsScene physicsScene, PhysicsLayer physicsLayer, bool Static) : base(physicsScene, physicsLayer, Static)
        {

        }
        public void SetTile(Tile tile, Point position)
        {
            tiles.Add(new TileData(tile, position));
        }
        protected override void Render()
        {
            if (_tilemapRender is null)
            {
                return;
            }
            int positionX = TilemapBoundsMinX - Scene.CameraPositionX + PositionX;
            int positionY = Scene.RenderHeight - TilemapBoundsMinY + Scene.CameraPositionY - PositionY - _tilemapRender.Height;
            if (positionX < -_tilemapRender.Width || positionY + _tilemapRender.Height < 0 || positionX > Scene.RenderWidth || positionY > Scene.RenderHeight)
            {

            }
            else
            {
                _XNAPositionCache.X = positionX;
                _XNAPositionCache.Y = positionY;
                Scene.XNASpriteBatch.Draw(_tilemapRender, _XNAPositionCache, Microsoft.Xna.Framework.Color.White);
            }
        }
        public void Apply()
        {
            TilemapBoundsMinX = int.MaxValue;
            TilemapBoundsMinY = int.MaxValue;
            TilemapBoundsMaxX = int.MinValue;
            TilemapBoundsMaxY = int.MinValue;

            foreach (TileData tileData in tiles)
            {
                foreach (Rectangle localShape in tileData.Tile.colliderShape)
                {
                    int minX = localShape.MinX + tileData.Position.X;
                    int minY = localShape.MinY + tileData.Position.X;
                    int maxX = localShape.MaxX + tileData.Position.X;
                    int maxY = localShape.MaxY + tileData.Position.X;
                    tilemapColliderShape.Add(new Rectangle(minX, minY, maxX, maxY));

                    if (minX < TilemapBoundsMinX)
                    {
                        TilemapBoundsMinX = minX;
                    }
                    if (minY < TilemapBoundsMinY)
                    {
                        TilemapBoundsMinY = minY;
                    }
                    if (maxX > TilemapBoundsMaxX)
                    {
                        TilemapBoundsMaxX = maxX;
                    }
                    if (maxY > TilemapBoundsMaxY)
                    {
                        TilemapBoundsMaxY = maxY;
                    }
                }
            }

            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(Game.GameInterface.XNAGraphicsDevice);
            Microsoft.Xna.Framework.Graphics.RenderTarget2D renderTarget2D = new Microsoft.Xna.Framework.Graphics.RenderTarget2D(Game.GameInterface.XNAGraphicsDevice, TilemapBoundsMaxX - TilemapBoundsMinX + 1, TilemapBoundsMaxY - TilemapBoundsMinY + 1);

            Game.GameInterface.XNAGraphicsDevice.SetRenderTarget(renderTarget2D);
            Game.GameInterface.XNAGraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
            spriteBatch.Begin(Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred, Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend, Microsoft.Xna.Framework.Graphics.SamplerState.PointClamp, null, null, null, null);

            foreach (TileData tileData in tiles)
            {
                if (tileData.Tile.Texture is not null)
                {
                    _XNAPositionCache.X = tileData.Position.X - TilemapBoundsMinX;
                    _XNAPositionCache.Y = renderTarget2D.Height - tileData.Position.Y + TilemapBoundsMinY - tileData.Tile.Texture.Height;
                    spriteBatch.Draw(tileData.Tile.Texture.XNABase, _XNAPositionCache, tileData.Tile.Color.ToXNA());
                }
            }

            spriteBatch.End();

            _tilemapRender = renderTarget2D;

            /*bool end = false;
            while (!end)
            {
                end = true;
                bool hit = false;
                int colliderShapeCount = tilemapColliderShape.Count;
                for (int i = 0; i < colliderShapeCount; i++)
                {
                    Rectangle a = tilemapColliderShape[i];
                    for (int i2 = 0; i2 < tilemapColliderShape.Count; i2++)
                    {
                        Rectangle b = tilemapColliderShape[i2];
                        if (a.MaxY == b.MaxY && a.MinY == b.MinY && a.MaxX + 1 == b.MinX)
                        {
                            tilemapColliderShape.RemoveAt(i);
                            tilemapColliderShape.RemoveAt(i2);
                            tilemapColliderShape.Add(new Rectangle(a.MinX, a.MinY, b.MaxX, a.MaxY));
                            hit = true;
                            end = false;
                            break;
                        }
                        if (a.MaxY == b.MaxY && a.MinY == b.MinY && a.MinX - 1 == b.MaxX)
                        {
                            tilemapColliderShape.RemoveAt(i);
                            tilemapColliderShape.RemoveAt(i2);
                            tilemapColliderShape.Add(new Rectangle(b.MinX, a.MinY, a.MaxX, a.MaxY));
                            hit = true;
                            end = false;
                            break;
                        }
                        if (a.MaxX == b.MaxX && a.MinX == b.MinX && a.MaxY + 1 == b.MinY)
                        {
                            tilemapColliderShape.RemoveAt(i);
                            tilemapColliderShape.RemoveAt(i2);
                            tilemapColliderShape.Add(new Rectangle(a.MinY, a.MinX, b.MaxY, a.MaxX));
                            hit = true;
                            end = false;
                            break;
                        }
                        if (a.MaxX == b.MaxX && a.MinX == b.MinX && a.MinY - 1 == b.MaxY)
                        {
                            tilemapColliderShape.RemoveAt(i);
                            tilemapColliderShape.RemoveAt(i2);
                            tilemapColliderShape.Add(new Rectangle(b.MinY, a.MinX, a.MaxY, a.MaxX));
                            hit = true;
                            end = false;
                            break;
                        }
                    }
                    if (hit)
                    {
                        break;
                    }
                }
            }*/

            //SetColliderShape(tilemapColliderShape.ToArray());
        }
    }
}