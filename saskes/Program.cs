using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saskes
{
    static class Program
    {
        public static GameLogic gameLogic;
        public static InputManager inputManager = new InputManager();
        public static MainForm mainForm;

        [STAThread]
        static void Main()
        {
            gameLogic = new GameLogic();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm(gameLogic.map);
            Application.Run(mainForm);
        }
    }
}
