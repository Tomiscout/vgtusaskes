using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saskes
{
    public enum TileType
    {
        None,
        Black,
        Red
    }

    public class Tile
    {
        public TileType type { get; }
        public int x { get; }
        public int y { get; }
        
        public Tile(TileType type, int x, int y)
        {
            this.type = type;
            this.x = x;
            this.y = y;
        }

        
    }
}
