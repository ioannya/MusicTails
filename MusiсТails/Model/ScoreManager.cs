using Model;
using System;
using System.IO;
using System.Text;

namespace Model
{
    public class ScoreManager : IScoreSavable
    {
        private const string FileName = "scores.json";

        public ScoreEntry[] Load()
        {
            try
            {
                if (!File.Exists(FileName))
                    return new ScoreEntry[0];

                string[] lines = File.ReadAllLines(FileName);
                ScoreEntry[] result = new ScoreEntry[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split('|');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        result[i] = new ScoreEntry
                        {
                            Player = parts[0],
                            Score = score
                        };
                    }
                    else
                    {
                        // Генерируем исключение — обработка ошибки в строке
                        throw new Exception($"Ошибка в файле рекордов, строка: {lines[i]}");
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // Пробрасываем ошибку выше
                throw new Exception($"Ошибка при загрузке рекордов: {ex.Message}");
            }
        }

        public void Save(ScoreEntry[] entries)
        {
            try
            {
                Array.Sort(entries, (a, b) => b.Score.CompareTo(a.Score));
                int count = entries.Length < 10 ? entries.Length : 10;

                using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.UTF8))
                {
                    for (int i = 0; i < count; i++)
                    {
                        sw.WriteLine(entries[i].Player + "|" + entries[i].Score);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при сохранении рекордов: {ex.Message}");
            }
        }
    }
}
