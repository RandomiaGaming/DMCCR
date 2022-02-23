using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonEngine
{
    public sealed class Tile
    {
        public Color Color = Color.White;
        public Texture Texture = null;
        public Rectangle[] colliderShape = new Rectangle[0];
    }
}
