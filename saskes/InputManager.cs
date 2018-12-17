using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saskes
{
    class InputManager
    {
        private Button lastPress;
        public void processTilePress(Button btn)
        {
            GameMap map = Program.gameLogic.map;
            bool selected = map.selectTile(btn);
            if (selected)
            {
                if (lastPress == null)
                {
                    lastPress = btn;
                }
                else
                {
                    Program.gameLogic.processMove((ITile) lastPress.Tag, (ITile)btn.Tag);
                    map.selectTile(lastPress);
                    map.selectTile(btn);
                    lastPress = null;
                }
            }
            


        }
    }
}
