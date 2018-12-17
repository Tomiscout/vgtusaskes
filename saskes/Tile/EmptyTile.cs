using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saskes
{
    class EmptyTile : ITile
    {
        private int x, y;
        public EmptyTile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public TileType GetTileType()
        {
            return TileType.None;
        }

        public Image GetImage()
        {
            return null;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}
