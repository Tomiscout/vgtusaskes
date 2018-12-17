using System;
using System.Collections.Generic;
using System.Drawing;
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

    public interface ITile
    {
        int GetX();
        int GetY();
        TileType GetTileType();
        Image GetImage();

    }
}
