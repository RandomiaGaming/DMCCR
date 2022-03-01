using System.Reflection;
using EpsilonEngine;
using System.IO;
using System.Text;
namespace DMCCR
{
    public sealed class StagePlayer : PhysicsScene
    {
        public const int ViewPortWidth = 256 * 3;
        public const int ViewPortHeight = 144 * 3;

        public Point CheckPointPos = new Point(-1036, -126);

        public StagePlayer(DMCCR dontmelt) : base(dontmelt, ViewPortWidth, ViewPortHeight, 0)
        {
            Stream levelDataStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Stages.Expert.txt");
            byte[] levelDataBytes = new byte[levelDataStream.Length];
            levelDataStream.Read(levelDataBytes, 0, (int)levelDataStream.Length);
            string levelData = Encoding.ASCII.GetString(levelDataBytes);

            PhysicsLayer playerPhysicsLayer = new PhysicsLayer(this);

            PhysicsLayer checkPointPhysicsLayer = new PhysicsLayer(this);

            PhysicsLayer groundPhysicsLayer = new PhysicsLayer(this);

            PhysicsLayer lavaPhysicsLayer = new PhysicsLayer(this);

            PhysicsLayer noJumpPhysicsLayer = new PhysicsLayer(this);

            PhysicsLayer airPhysicsLayer = new PhysicsLayer(this);

            Texture groundTexture = new Texture(dontmelt, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.Ground.png"));

            Texture lavaTexture = new Texture(dontmelt, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.Lava.png"));

            Texture noJumpTexture = new Texture(dontmelt, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.NoJump.png"));

            Texture airTexture = new Texture(dontmelt, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.Air.png"));

            Tile groundTile = new Tile()
            {
                Texture = groundTexture,
                colliderShape = new Rectangle[1] { new Rectangle(0, 0, 15, 15) },
                Color = Color.White,
            };

            Tile lavaTile = new Tile()
            {
                Texture = lavaTexture,
                colliderShape = new Rectangle[1] { new Rectangle(1, 1, 14, 14) },
                Color = Color.White,
            };

            Tile noJumpTile = new Tile()
            {
                Texture = noJumpTexture,
                colliderShape = new Rectangle[1] { new Rectangle(0, 0, 15, 15) },
                Color = Color.White,
            };

            Tile airTile = new Tile()
            {
                Texture = airTexture,
                colliderShape = new Rectangle[1] { new Rectangle(0, 0, 16, 16) },
                Color = Color.White,
            };

            Player player = new Player(this, playerPhysicsLayer, new PhysicsLayer[] { playerPhysicsLayer, groundPhysicsLayer, lavaPhysicsLayer, noJumpPhysicsLayer });
            player.Position = CheckPointPos;

            Tilemap groundTilemap = new Tilemap(this, groundPhysicsLayer, true);

            Tilemap lavaTilemap = new Tilemap(this, groundPhysicsLayer, true);

            Tilemap noJumpTilemap = new Tilemap(this, groundPhysicsLayer, true);

            Tilemap airTilemap = new Tilemap(this, airPhysicsLayer, true);

            string[] objects = levelData.Split('\n');

            foreach (string _object in objects)
            {
                if (_object != "")
                {
                    string t = _object.Substring(0, _object.Length - 1);
                    int xsplit = -1;
                    int ysplit = -1;
                    for (int i = 0; i < t.Length; i++)
                    {
                        if (t[i] == ';')
                        {
                            if (xsplit == -1)
                            {
                                xsplit = i;
                            }
                            else
                            {
                                ysplit = i;
                            }
                        }
                    }
                    string xs = t.Substring(0, xsplit);
                    string ys = t.Substring(xsplit + 1, ysplit - 1 - xsplit);
                    string ds = t.Substring(ysplit + 1, t.Length - 1 - ysplit);

                    int x = int.Parse(xs);
                    int y = int.Parse(ys);

                    if (ds == "Ground")
                    {
                        groundTilemap.SetTile(groundTile, new Point(16 * x, 16 * y));
                        new Ground(this, groundPhysicsLayer)
                        {
                            PositionX = 16 * x,
                            PositionY = 16 * y,
                        };
                    }
                    else if (ds == "Lava")
                    {
                        lavaTilemap.SetTile(lavaTile, new Point(16 * x, 16 * y));
                        new Lava(this, lavaPhysicsLayer)
                        {
                            PositionX = 16 * x,
                            PositionY = 16 * y,
                        };
                    }
                    else if (ds == "NoJump")
                    {
                        noJumpTilemap.SetTile(noJumpTile, new Point(16 * x, 16 * y));
                        new NoJump(this, noJumpPhysicsLayer)
                        {
                            PositionX = 16 * x,
                            PositionY = 16 * y,
                        };
                    }
                    else if (ds == "Air")
                    {
                        airTilemap.SetTile(airTile, new Point(16 * x, 16 * y));
                        new NoJump(this, airPhysicsLayer)
                        {
                            PositionX = 16 * x,
                            PositionY = 16 * y,
                        };
                    }
                    else if (ds == "CheckPoint")
                    {
                        new Checkpoint(this, checkPointPhysicsLayer, new PhysicsLayer[] { playerPhysicsLayer })
                        {
                            PositionX = 16 * x,
                            PositionY = 16 * y,
                        };
                    }
                }
            }

            groundTilemap.Apply();
            lavaTilemap.Apply();
            noJumpTilemap.Apply();
            airTilemap.Apply();
        }
        public override string ToString()
        {
            return $"Epsilon.StagePlayer()";
        }
    }
}
