using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ProjekatB_Flappy_Bird
{
    public partial class Score : Window
    {
        public Score()
        {
            InitializeComponent();
            LoadScores();
        }

        public class ScoreRecord
        {
            public string Name { get; set; }
            public string Score { get; set; }
        }

        private void LoadScores()
        {
            // Putanja do tekstualne datoteke sa rezultatima
            string filePath = "Score\\scores.txt";

            var scores = new List<ScoreRecord>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    // Pretpostavljamo format "ime - rezultat"
                    var parts = line.Split('-');
                    if (parts.Length == 2)
                    {
                        // Dodavanje u listu uz parsiranje score-a u int
                        int parsedScore;
                        if (int.TryParse(parts[1].Trim(), out parsedScore))
                        {
                            scores.Add(new ScoreRecord
                            {
                                Name = parts[0].Trim(),
                                Score = parsedScore.ToString()
                            });
                        }
                    }
                }

                // Sortiranje rezultata od najvećeg ka najmanjem
                scores.Sort((x, y) => int.Parse(y.Score).CompareTo(int.Parse(x.Score)));
            }

            // Povezujemo podatke sa DataGrid-om
            scoreDataGrid.ItemsSource = scores;
        }

        private void exit_Click(object sender,EventArgs e)
        {
            this.Close();
        }
    }
}

