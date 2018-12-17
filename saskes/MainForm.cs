using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saskes
{
    public partial class MainForm : Form
    {
        public Button[,] Buttons;
        public MainForm(GameMap map)
        {
            InitializeComponent();

            Buttons = new Button[map.tiles.GetLength(0), map.tiles.GetLength(1)];
            int ButtonWidth = 40;
            int ButtonHeight = 40;
            int Distance = 20;
            int start_x = 2;
            int start_y = 20;
            int blackColor = 0;

            for (int y = 0; y < map.tiles.GetLength(0); y++)
            {
                for (int x = 0; x < map.tiles.GetLength(1); x++)
                {
                    Button tmpButton = new Button();
                    tmpButton.Top = start_y + (y * ButtonHeight + Distance);
                    tmpButton.Left = start_x + (x * ButtonWidth + Distance);
                    tmpButton.Width = ButtonWidth;
                    tmpButton.Height = ButtonHeight;
                    tmpButton.Tag = map.tiles[y,x];

                    
                    tmpButton.BackColor = blackColor % 2 == 0 ? Color.White : Color.DarkGray;
                    tmpButton.FlatStyle = FlatStyle.Flat;
                    tmpButton.FlatAppearance.BorderColor = Color.Black;
                    tmpButton.FlatAppearance.BorderSize = 1;

                    tmpButton.Click += (source, e) =>
                    {
                        Button btn = (Button)source;
                        Program.inputManager.processTilePress(btn);
                    };
                    // Possible add Buttonclick event etc..
                    Buttons[y, x] = tmpButton;

                    blackColor++;
                }

                foreach (var button in Buttons)
                {
                    this.Controls.Add(button);
                }

                if (y % 2 == 0) blackColor = 1;
                else blackColor = 0;
            }
        }

        private void startGameBtn_Click(object sender, EventArgs e)
        {
            Program.gameLogic.InitGame();
        }

        public void setTurnLabel(string text)
        {
             turnLabel.Text = text;
        }
    }
}
