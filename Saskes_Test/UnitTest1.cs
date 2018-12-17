using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Saskes;

namespace Saskes_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetOppositeTeam_Test()
        {
            GameLogic gameLogic = new GameLogic(new GameMap(8,8));
            Assert.AreEqual(TileType.Red, gameLogic.getOpositeTeam(TileType.Black));
            Assert.AreEqual(TileType.Black, gameLogic.getOpositeTeam(TileType.Red));
            Assert.AreEqual(TileType.None, gameLogic.getOpositeTeam(TileType.None));
        }

        [TestMethod]
        public void GetTileCounts()
        {
            GameMap map = new GameMap(8,8);
            GameLogic logic = new GameLogic(map);
            logic.InitGame();
            var tileCounts = map.getTileCounts();

            int count;
            //Empty tile
            if (tileCounts.TryGetValue(TileType.None, out count))
            {
                Assert.AreEqual(40, count);
            }
            else
            {
                Assert.Fail("No empty tile count found");
            }

            //Red Tiles
            if (tileCounts.TryGetValue(TileType.Red, out count))
            {
                Assert.AreEqual(12, count);
            }
            else
            {
                Assert.Fail("No Red tile count found");
            }

            //Black Tiles
            if (tileCounts.TryGetValue(TileType.Black, out count))
            {
                Assert.AreEqual(12, count);
            }
            else
            {
                Assert.Fail("No Black tile count found");
            }
        }
    }
}
