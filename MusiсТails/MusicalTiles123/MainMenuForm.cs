using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.IO;
using Model;

namespace MusicalTiles123
{
    public partial class MainMenuForm : Form
    {
        private ScoreManager scoreManager = new ScoreManager();
        private ComboBox difficultyBox;
        private ListBox topScoresBox;

        public MainMenuForm()
        {
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            this.Text = "Musical Tiles - Меню";
            this.Size = new Size(400, 400);

            Button startButton = new Button
            {
                Text = "Начать игру",
                Location = new Point(130, 50),
                Size = new Size(120, 40)
            };
            startButton.Click += StartButton_Click;
            this.Controls.Add(startButton);

            Label difficultyLabel = new Label
            {
                Text = "Сложность (кол-во полос):",
                Location = new Point(100, 110),
                AutoSize = true
            };
            this.Controls.Add(difficultyLabel);

            difficultyBox = new ComboBox
            {
                Location = new Point(130, 140),
                Size = new Size(120, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            for (int i = 2; i <= 8; i++)
                difficultyBox.Items.Add(i);
            difficultyBox.SelectedIndex = 0;
            this.Controls.Add(difficultyBox);

            Label scoreLabel = new Label
            {
                Text = "Топ-10 результатов:",
                Location = new Point(130, 180),
                AutoSize = true
            };
            this.Controls.Add(scoreLabel);

            topScoresBox = new ListBox
            {
                Location = new Point(100, 210),
                Size = new Size(180, 120)
            };
            this.Controls.Add(topScoresBox);

            LoadScores();
        }

        private void LoadScores()
        {
            if (!File.Exists("scores.json")) return;

            var scores = scoreManager.Load();
            topScoresBox.Items.Clear();

            foreach (var s in scores.Take(10))
            {
                topScoresBox.Items.Add($"{s.Player} — {s.Score}");
            }
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            int difficulty = (int)difficultyBox.SelectedItem;
            var gameForm = new GameForm("Результат", difficulty, this);
            gameForm.Show();
            this.Hide();
        }



        public void RefreshScores()
        {
            LoadScores();
            this.Show();
        }
    }
}
