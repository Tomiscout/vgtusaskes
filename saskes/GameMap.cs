using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Saskes
{
    public class GameMap
    {
        public Tile[,] tiles { get; }
        private Bitmap redImg, blackImg;
        private Color blackColor = Color.DarkGray, selectColor = Color.Bisque;

        public GameMap(int x, int y)
        {
            tiles = new Tile[y,x];

            for (int i = 0; i < y; i++)
            {
                for (int u = 0; u < x; u++)
                {
                    tiles[i,u] = new Tile(TileType.None, u, i);
                }
            }

            redImg = new Bitmap(Saskes.Properties.Resources.red);
            blackImg = new Bitmap(Saskes.Properties.Resources.black);
        }

        public bool selectTile(int x, int y)
        {
            return selectTile(getUiButton(y, x));
        }
        public bool selectTile(Button btn)
        {
            if (btn.BackColor == blackColor)
            {
                btn.BackColor = selectColor;
                return true;
            }
            else if(btn.BackColor == selectColor)
            {
                btn.BackColor = blackColor;
            }

            return false;
        }

        public void setTile(Tile tile)
        {
            tiles[tile.y, tile.x] = tile;
            Button btn = getUiButton(tile.x, tile.y);

            if (tile.type == TileType.None)
            {
                btn.Image = null;
            }
            else if (tile.type == TileType.Black)
            {
                btn.Image = blackImg;
            }else if (tile.type == TileType.Red)
            {
                btn.Image = redImg;
            }
        }

        public void removeChecker(Tile tile)
        {
            setTile(new Tile(TileType.None, tile.x, tile.y));
        }

        private Button getUiButton(int x, int y)
        {
            return Program.mainForm.Buttons[y, x];
        }

        public void clearTable()
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int u = 0; u < tiles.GetLength(1); u++)
                {
                    removeChecker(tiles[u,i]);
                }
            }
        }

        public Tile getTile(int x, int y)
        {
            if(x >= 0 && x < tiles.GetLength(1) && y >= 0 && y < tiles.GetLength(0))
                return tiles[y, x];
            return null;
        }
        public Tile getTile(Tile tile)
        {
           Tile t= tiles[tile.y, tile.x];
            return t;
        }

        public Dictionary<TileType, int> getTileCounts()
        {
            Dictionary < TileType, int> counts = new Dictionary<TileType, int>();
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int u = 0; u < tiles.GetLength(1); u++)
                {
                    if (counts.ContainsKey(tiles[i,u].type))
                    {
                        counts[tiles[i, u].type]++;
                    }
                    else
                    {
                        counts.Add(tiles[i, u].type, 1);
                    }
                }
            }
           

            return counts;
        }
    }
}