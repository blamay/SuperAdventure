using System;
using System.Threading;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class Title : Form
    {

        //Thread th;
        public Title()
        {
            InitializeComponent();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveScreen savescreen = new SaveScreen();
            savescreen.ShowDialog();

        }

        private void opennewform(object obj)
        {
            Application.Run(new SaveScreen());
        }
    }
}
