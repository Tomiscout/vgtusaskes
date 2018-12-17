using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saskes
{
    class BlackTile : ITile
    {
        private int x, y;
        private Image image;
        public BlackTile(int x, int y)
        {
            this.x = x;
            this.y = y;
            image = GameMap.getRedImage();
        }
        public TileType GetTileType()
        {
            return TileType.Black;
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
