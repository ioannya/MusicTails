using System;
using System.Drawing;
using System.Windows.Forms;
using Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MusicalTiles123
{
    public partial class GameForm : Form
    {
        private void FallTimer_Tick(object sender, EventArgs e)
        {
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            for (int i = 0; i < difficulty; i++)
            {
                Button tile = activeTiles[i];
                IMusicalButton musicalBtn = activeMusicalButtons[i];

                if (tile != null && musicalBtn != null)
                {
                    if (musicalBtn is MusicalButtonBase baseBtn)
                    {
                        baseBtn.MoveDown(tileSpeed);
                    }
                    tile.Top += tileSpeed;

                    if (musicalBtn.IsMissed(lanes[i].Bottom))
                    {
                        FinishGame();
                        return;
                    }

                    if (tile.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(tile);
                        tile.Dispose();
                        activeTiles[i] = null;
                        activeMusicalButtons[i] = null;
                    }
                }
                else if (tile == null)
                {
                    if (rand.NextDouble() < 0.02 + score * 0.0005)
                        SpawnTile(i);
                }
            }

            AdjustDifficulty();
        }

        private void SpawnTile(int laneIndex)
        {
            int x = lanes[laneIndex].Left;
            Button newTile = new Button
            {
                Size = new Size(80, 30),
                Location = new Point(x, 0),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Text = ""
            };
            this.Controls.Add(newTile);
            activeTiles[laneIndex] = newTile;

            IMusicalButton musicalBtn = buttonGenerator.GenerateButton(laneIndex % 2 == 0);
            activeMusicalButtons[laneIndex] = musicalBtn;

            if (musicalBtn is MusicalButtonBase baseBtn)
            {
                baseBtn.MoveDown(0);
            }
        }

        private void TryHit(int laneIndex)
        {
            Button tile = activeTiles[laneIndex];
            IMusicalButton musicalBtn = activeMusicalButtons[laneIndex];
            if (tile == null || musicalBtn == null) return;

            Rectangle hitZone = lanes[laneIndex].Bounds;
            hitZone.Y -= 15;
            hitZone.Height += 30;

            if (tile.Bounds.IntersectsWith(hitZone))
            {
                musicalBtn.OnPress();
                score += musicalBtn.Score * difficulty;
                scoreLabel.Text = $"Очки: {score}";

                this.Controls.Remove(tile);
                tile.Dispose();
                activeTiles[laneIndex] = null;
                activeMusicalButtons[laneIndex] = null;
            }
        }

        private void AdjustDifficulty()
        {
            tileSpeed = 5 + score / 50;
        }

        private void FinishGame()
        {
            fallTimer.Stop();

            ScoreManager manager = new ScoreManager();
            try
            {
                var existing = manager.Load();
                Model.ScoreEntry[] newScores = new Model.ScoreEntry[existing.Length + 1];
                for (int i = 0; i < existing.Length; i++)
                    newScores[i] = existing[i];
                newScores[existing.Length] = new Model.ScoreEntry { Player = playerName, Score = score };

                manager.Save(newScores);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении результата: " + ex.Message);
            }

            MessageBox.Show($"Игра окончена! Ваш счет: {score}", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            menuForm.RefreshScores();
            menuForm.Show();
        }



        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}

