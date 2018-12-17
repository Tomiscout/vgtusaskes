using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saskes
{
    class RedTile : ITile
    {
        private int x, y;
        private Image image;
        public RedTile(int x, int y)
        {
            this.x = x;
            this.y = y;
            image = GameMap.getRedImage();
        }
        public TileType GetTileType()
        {
            return TileType.Red;
        }

        public Image GetImage()
        {
            return image;
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
