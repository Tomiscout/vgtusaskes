using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace Saskes
{
    public class GameMap
    {
        public ITile[,] tiles { get; }
        private static Bitmap redImg, blackImg;
        private Color blackColor = Color.DarkGray, selectColor = Color.Bisque;

        public static Image getRedImage()
        {
            return redImg;
        }

        public static Image getBlackImage()
        {
            return blackImg;
        }
        public GameMap(int x, int y)
        {
            tiles = new ITile[y,x];

            for (int i = 0; i < y; i++)
            {
                for (int u = 0; u < x; u++)
                {
                    tiles[i,u] = new EmptyTile(u, i);
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

        public void setTile(ITile tile)
        {
            tiles[tile.GetY(), tile.GetX()] = tile;
            Button btn = getUiButton(tile.GetX(), tile.GetY());

            if (Program.mainForm != null)
            {
                if (tile.GetTileType() == TileType.None)
                {
                    btn.Image = null;
                }
                else if (tile.GetTileType() == TileType.Black)
                {
                    btn.Image = blackImg;
                }
                else if (tile.GetTileType() == TileType.Red)
                {
                    btn.Image = redImg;
                }
            }
        }
        public ITile setTile(TileType type, int x, int y)
        {
            ITile tile = null;
            if (type == TileType.Black)
            {
                tile = new BlackTile(x,y);
            }
            else if (type == TileType.Red)
            {
                tile = new RedTile(x, y);
            }
            else if (type == TileType.None)
            {
                tile = new EmptyTile(x, y);
            }

            tiles[tile.GetY(), tile.GetX()] = tile;

            if (Program.mainForm != null)
            {
                Button btn = getUiButton(tile.GetX(), tile.GetY());
                if (tile.GetTileType() == TileType.None)
                {
                    btn.Image = null;
                }
                else if (tile.GetTileType() == TileType.Black)
                {
                    btn.Image = blackImg;
                }
                else if (tile.GetTileType() == TileType.Red)
                {
                    btn.Image = redImg;
                }
            }

            return tile;
        }

        public void removeChecker(ITile tile)
        {
            setTile(new EmptyTile(tile.GetX(), tile.GetY()));
        }

        private Button getUiButton(int x, int y)
        {
            if(Program.mainForm != null)
                return Program.mainForm.Buttons[y, x];
            return null;
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

        public ITile getTile(int x, int y)
        {
            if(x >= 0 && x < tiles.GetLength(1) && y >= 0 && y < tiles.GetLength(0))
                return tiles[y, x];
            return null;
        }
        public ITile getTile(ITile tile)
        {
           ITile t= tiles[tile.GetY(), tile.GetX()];
            return t;
        }

        public Dictionary<TileType, int> getTileCounts()
        {
            Dictionary < TileType, int> counts = new Dictionary<TileType, int>();
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int u = 0; u < tiles.GetLength(1); u++)
                {
                    if (counts.ContainsKey(tiles[i,u].GetTileType()))
                    {
                        counts[tiles[i, u].GetTileType()]++;
                    }
                    else
                    {
                        counts.Add(tiles[i, u].GetTileType(), 1);
                    }
                }
            }
           

            return counts;
        }

        // attTile[0] = left tile, attTile[1] = right tile
        public ITile[] getCrossTiles(ITile tile, int direction)
        {
            ITile[] attTiles = new ITile[2];

            attTiles[0] = tile.GetX() > 0 ? getTile(tile.GetX() - 1, tile.GetY() - direction) : null;
            attTiles[1] = tile.GetX() < tiles.GetLength(1) ? getTile(tile.GetX() + 1, tile.GetY() - direction) : null;

            return attTiles;
        }

        public List<ITile> getTilesByType(TileType type)
        {
            List<ITile> filteredTiles = new List<ITile>();
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int u = 0; u < tiles.GetLength(1); u++)
                {
                    if (tiles[i, u].GetTileType() == type)
                    {
                        filteredTiles.Add(tiles[i,u]);
                    }
                }
            }
            return filteredTiles;
        }
    }
}