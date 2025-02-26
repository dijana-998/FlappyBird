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
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void instruction_Click(object sender, RoutedEventArgs e)
        {
            Instructions instructions= new Instructions();
            instructions.Show();
        }

        private void score_Click(object sender, RoutedEventArgs e)
        {
            Score score= new Score();
            score.Show();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Initial initial= new Initial();
            initial.Show();
        }
    }
}
