using System;
using System.Windows;

namespace ProjekatB_Flappy_Bird
{
    
    public partial class Instructions : Window
    {
        public Instructions()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
