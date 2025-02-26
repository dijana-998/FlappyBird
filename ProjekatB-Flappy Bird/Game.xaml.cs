using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProjekatB_Flappy_Bird
{
    public partial class Initial : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        double score;
        int gravity = 8;
        bool gameOver;
        Rect flappyBirdHitBox;
        public Initial()
        {
            InitializeComponent();
            gameTimer.Tick += MainEventTimer;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            StartGame();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {
            txtScore.Content = "Score:" + score;
            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 5, flappyBird.Height);
            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);

            if (Canvas.GetTop(flappyBird) < -10 || Canvas.GetTop(flappyBird) > 458)
            {
                EndGame();
            }
            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);
                    if (Canvas.GetLeft(x) < -100)
                    {
                        Canvas.SetLeft(x, 800);
                        score += .5;
                    }

                    Rect pipHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (flappyBirdHitBox.IntersectsWith(pipHitBox))
                    {
                        EndGame();
                    }
                }
                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 2);
                    if (Canvas.GetLeft(x) < -250)
                    {
                        Canvas.SetLeft(x, 550);
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = -8;
            }
            if (e.Key == Key.Q && gameOver == true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 8;
        }

        private void StartGame()
        {
            MyCanvas.Focus();
            int temp = 300;
            score = 0;
            gameOver = false;
            Canvas.SetTop(flappyBird, 190);

            saveButton.Visibility = Visibility.Hidden;
            nameTextBox.Visibility = Visibility.Hidden;
            confirmButton.Visibility = Visibility.Hidden;
            placeholderText.Visibility = Visibility.Hidden;
            txtEndScore.Visibility = Visibility.Hidden;


            foreach (var x in MyCanvas.Children.OfType<Image>())
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);
                }
                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);
                }
                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1100);
                }
                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, 300 + temp);
                    temp = 800;
                }
            }
            gameTimer.Start();

        }

        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content = " Game over ! Press Q to try again";
            if (score > 0) 
            {
                saveButton.Visibility = Visibility.Visible;
                txtEndScore.Content = "Score:"+score;
                txtEndScore.Visibility = Visibility.Visible;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            nameTextBox.Visibility = Visibility.Visible;
            confirmButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;
            placeholderText.Visibility = Visibility.Visible;
            nameTextBox.TextChanged+= NameTextBox_TextChanged;
        }
        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                placeholderText.Visibility = Visibility.Visible;
            }
            else
            {
                placeholderText.Visibility = Visibility.Hidden;
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = nameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                string scoreRecord = $"{playerName} - {score}";
                // Putanja do tekstualne datoteke gde ćete čuvati rezultate
                string filePath = "Score\\scores.txt.";

                // Upisujemo rezultat u tekstualnu datoteku
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(scoreRecord);
                }

                // Sakrivamo elemente nakon čuvanja
                nameTextBox.Visibility = Visibility.Hidden;
                confirmButton.Visibility = Visibility.Hidden;
                MessageBox.Show("Score saved successfully!");
            }
            else
            {
                MessageBox.Show("Please enter a valid name.");
            }
            StartGame();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
