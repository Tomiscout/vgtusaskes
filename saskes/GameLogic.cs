using System;
using System.Collections.Generic;

namespace Saskes
{
    public class GameLogic
    {
        private TileType currentTurn;
        private TileType enemy;
        public GameMap map;

        public GameLogic(GameMap map)
        {
            this.map = map;
        }

        public void InitGame()
        {
            currentTurn = TileType.Red;
            map.clearTable();

            if (Program.mainForm != null) 
                Program.mainForm.setTurnLabel(getTurnLabel(currentTurn, false));


            generateRow(TileType.Red, 0, 3, true);
            generateRow(TileType.Black, map.tiles.GetLength(0) - 3, 3, false);
        }

        private void generateRow(TileType type, int yStart, int rows, bool oddSpawn)
        {
            var oddTile = oddSpawn;
            for (var u = yStart; u < yStart + rows; u++)
            {
                for (var i = 0; i < map.tiles.GetLength(1); i++)
                {
                    if (!oddTile)
                        map.setTile(type, i, u);

                    oddTile = !oddTile;
                }
                if (oddSpawn) oddTile = false;
                else oddTile = true;
                oddSpawn = !oddSpawn;
            }
        }

        internal void processMove(ITile firstTile, ITile nextTile)
        {
            firstTile = map.getTile(firstTile);
            nextTile = map.getTile(nextTile);
            bool extraTurn = false, failedTurn = true, mustAttack = isAttackAvailable(currentTurn);
            Console.WriteLine($"From:{firstTile.GetX()},{firstTile.GetY()} to:{nextTile.GetX()},{nextTile.GetY()}");
            //Checks if it's correct turn
            if (firstTile.GetTileType() == currentTurn)
            {
                int direction = getTileDirection(currentTurn);

                //If direction down and y delta is 1 or 2
                if (direction != 0 && firstTile.GetY() == nextTile.GetY() + direction || firstTile.GetY() == nextTile.GetY() + direction * 2)
                {
                    var deltaX = Math.Abs(firstTile.GetX() - nextTile.GetX());
                    if (deltaX == 1 && !mustAttack)
                    {
                        if (nextTile.GetTileType() == TileType.None)
                        {
                            //Move
                            map.removeChecker(firstTile);
                            map.setTile(currentTurn, nextTile.GetX(), nextTile.GetY());
                            failedTurn = false;
                        }
                    }
                    else if (deltaX == 2)
                    {
                        //Get available attack moves
                        var attackTiles = getAttackTiles(firstTile, direction);
                        foreach (var attTile in attackTiles)
                            if (attTile.Key != null && attTile.Value != null && attTile.Value == nextTile)
                            {
                                map.removeChecker(firstTile);
                                map.removeChecker(attTile.Key);
                                var tile = map.setTile(currentTurn, attTile.Value.GetX(), attTile.Value.GetY());
                                failedTurn = false;

                                //Should give extra turn?
                                if (getAttackTiles(tile, direction).Count > 0)
                                    extraTurn = true;
                            }
                    }
                }
            }


            //Check if game over
            var counts = map.getTileCounts();
            if (counts.ContainsKey(TileType.Black) && counts[TileType.Black] == 0)
                Program.mainForm.setTurnLabel(getTurnLabel(TileType.Black, true));
            else if (counts.ContainsKey(TileType.Red) && counts[TileType.Red] == 0)
                Program.mainForm.setTurnLabel(getTurnLabel(TileType.Red, true));


            //next turn
            if (!failedTurn && !extraTurn)
            {
                currentTurn = getOpositeTeam(currentTurn);
                Program.mainForm.setTurnLabel(getTurnLabel(currentTurn, false));
                if (isAttackAvailable(currentTurn))
                    Program.mainForm.setTurnLabel(getTurnLabel(currentTurn, false) + " must attack");
            }
        }

        public TileType getOpositeTeam(TileType type)
        {
            if (type == TileType.Black)
                return TileType.Red;
            if (type == TileType.Red)
                return TileType.Black;

            return TileType.None;
        }

        //tile[0] is the enemy tile, tile[1] is the destination tile
        private Dictionary<ITile, ITile> getAttackTiles(ITile tile, int direction)
        {
            var returnTiles = new Dictionary<ITile, ITile>();
            var oppositeTeam = getOpositeTeam(tile.GetTileType());

            //Get cross enemy tiles
            var attackTiles = map.getCrossTiles(tile, direction);

            for (var i = 0; i < 2; i++)
                //If the cross tile is enemy
                if (attackTiles[i] != null && attackTiles[i].GetTileType() == oppositeTeam)
                {
                    var emptyTiles = map.getCrossTiles(attackTiles[i], direction);
                    for (var u = 0; u < 2; u++)
                        if (emptyTiles[u] != null && emptyTiles[u].GetTileType() == TileType.None && i == u)
                            returnTiles.Add(attackTiles[i], emptyTiles[u]);
                }
            return returnTiles;
        }

        private bool isAttackAvailable(TileType tileType)
        {
            //Iterate all tiles of current team
            var filteredTiles = map.getTilesByType(tileType);
            var direction = getTileDirection(tileType);

            foreach (var tile in filteredTiles)
            {
                if (getAttackTiles(tile, direction).Count > 0)
                    return true;
            }
            return false;
        }

        public static int getTileDirection(TileType type)
        {
            return type == TileType.Red ? -1 : (type == TileType.Black ? 1 : 0);
        }

        public string getTurnLabel(TileType type, bool win)
        {
            if (win)
                return type + " has won!";

            return type.ToString();
        }
    }
}