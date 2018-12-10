using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saskes
{
    class GameLogic
    {
        public GameMap map;
        private TileType currentTurn;
        private TileType enemy;

        public GameLogic()
        {
            this.map = map = new GameMap(8,8);

        }

        public void InitGame()
        {
            currentTurn = TileType.Red;
            map.clearTable();
            Program.mainForm.setTurnLabel(currentTurn, false);
            


            generateRow(TileType.Red, 0, 3, true);
            generateRow(TileType.Black, map.tiles.GetLength(0)-3, 3, false);
        }

        private void generateRow(TileType type, int yStart, int rows, bool oddSpawn)
        {
            bool oddTile = oddSpawn;
            for (int u = yStart; u < yStart+rows; u++)
            {
                for (int i = 0; i < map.tiles.GetLength(1); i++)
                {
                    if (!oddTile)
                    {
                        map.setTile(new Tile(type, i,u));
                    }

                    oddTile = !oddTile;

                }
                if (oddSpawn) oddTile = false;
                else oddTile = true;
                oddSpawn = !oddSpawn;
            }
        }

        internal void processMove(Tile firstTile, Tile nextTile)
        {
            firstTile = map.getTile(firstTile);
            nextTile = map.getTile(nextTile);
            bool extraTurn = false, failedTurn = true;
            Console.WriteLine($"From:{firstTile.x},{firstTile.y} to:{nextTile.x},{nextTile.y}");
            //Checks if it's correct turn
            if (firstTile.type == currentTurn)
            {
                int direction;
                if (currentTurn == TileType.Red)
                {
                    direction = -1;
                }
                else if (currentTurn == TileType.Black)
                {
                    direction = 1;
                }
                else
                {
                    direction = 0;

                }

                //If direction down
                if (direction != 0)
                {
                    if (firstTile.y == nextTile.y + direction)
                    {
                        if (Math.Abs(firstTile.x-nextTile.x) == 1)
                        {
                            if (nextTile.type == TileType.None)
                            {
                                //Move
                                map.removeChecker(firstTile);
                                map.setTile(new Tile(currentTurn, nextTile.x, nextTile.y));
                                failedTurn = false;
                            }else if (nextTile.type == getOpositeTeam(firstTile.type))
                            {
                                //Get adjacent tiles
                                Tile leftTile = map.getTile(nextTile.x - 1, nextTile.y - direction);
                                Tile rightTile = map.getTile(nextTile.x + 1, nextTile.y - direction);
                                if (leftTile != null || rightTile != null)
                                {
                                    if (leftTile.type == TileType.None)
                                    {
                                        map.removeChecker(firstTile);
                                        map.removeChecker(nextTile);
                                        map.setTile(new Tile(currentTurn, leftTile.x, leftTile.y));
                                        extraTurn = true;
                                        failedTurn = false;
                                    }else if (rightTile.type == TileType.None)
                                    {
                                        map.removeChecker(firstTile);
                                        map.removeChecker(nextTile);
                                        map.setTile(new Tile(currentTurn, rightTile.x, rightTile.y));
                                        extraTurn = true;
                                        failedTurn = false;
                                    }
                                }
                            }
                           
                        }
                    }

                }
            }
            

            //Check if game over
            var counts = map.getTileCounts();
            if (counts[TileType.Black] == 0)
            {
                Program.mainForm.setTurnLabel(TileType.Black, true);
            }
            else if (counts[TileType.Red] == 0)
            {
                Program.mainForm.setTurnLabel(TileType.Red, true);
            }

            //next turn
            if (!failedTurn && !extraTurn)
            {
                currentTurn = getOpositeTeam(currentTurn);
            }
            Program.mainForm.setTurnLabel(currentTurn, false);
        }

        private TileType getOpositeTeam(TileType type)
        {
            if (type == TileType.Black)
            {
                return TileType.Red;
            }else if (type == TileType.Red)
            {
                return TileType.Black;
            }

            return TileType.None;
        }

       
    }
   
}
