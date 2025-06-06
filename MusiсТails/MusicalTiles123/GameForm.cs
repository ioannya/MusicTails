using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using Model;

namespace MusicalTiles123
{
    public partial class GameForm : Form
    {
        private string playerName;
        private int difficulty;
        private MainMenuForm menuForm;

        private int score = 0;
        private Label scoreLabel;

        private System.Windows.Forms.Timer fallTimer;
        private Button[] lanes;
        private Button[] activeTiles;
        private IMusicalButton[] activeMusicalButtons; // бизнес-логика

        private ButtonGenerator buttonGenerator;
        private int tileSpeed = 5;
        private Random rand = new Random();

        public GameForm(string playerName, int difficulty, MainMenuForm menuForm)
        {

            this.playerName = playerName;
            this.difficulty = difficulty;
            this.menuForm = menuForm;

            this.Text = "Musical Tiles - Игра";
            this.ClientSize = new Size(100 * difficulty, 300);

            InitializeGame();

            fallTimer = new System.Windows.Forms.Timer();
            fallTimer.Interval = 30;
            fallTimer.Tick += FallTimer_Tick;
            fallTimer.Start();
        }

        private void InitializeGame()
        {
            scoreLabel = new Label()
            {
                Text = "Очки: 0",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 14)
            };
            this.Controls.Add(scoreLabel);

            lanes = new Button[difficulty];
            activeTiles = new Button[difficulty];
            activeMusicalButtons = new IMusicalButton[difficulty];
            buttonGenerator = new ButtonGenerator();

            int buttonWidth = 80;
            int buttonHeight = 30;
            int spacing = 10;
            int startX = 10;
            int y = this.ClientSize.Height - buttonHeight - 30;

            for (int i = 0; i < difficulty; i++)
            {
                Button btn = new Button()
                {
                    Text = "",
                    Size = new Size(buttonWidth, buttonHeight),
                    Location = new Point(startX + i * (buttonWidth + spacing), y),
                    BackColor = Color.Gray
                };
                this.Controls.Add(btn);
                lanes[i] = btn;
            }

            Button finishButton = new Button()
            {
                Text = "Закончить игру",
                Location = new Point(10, 50),
                Size = new Size(150, 30)
            };
            finishButton.Click += FinishButton_Click;
            this.Controls.Add(finishButton);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int index = -1;
            switch (keyData)
            {
                case Keys.A: index = 0; break;
                case Keys.S: index = 1; break;
                case Keys.D: index = 2; break;
                case Keys.F: index = 3; break;
                case Keys.G: index = 4; break;
                case Keys.H: index = 5; break;
                case Keys.J: index = 6; break;
                case Keys.K: index = 7; break;
            }


            if (index >= 0 && index < difficulty)
                TryHit(index);

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            FinishGame();
        }
    }
}
