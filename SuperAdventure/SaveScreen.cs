using System;
using System.Threading;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class SaveScreen : Form
    {
        Thread th;
        public SaveScreen()
        {
            InitializeComponent();
        }

        

        private void save1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void opennewform(object obj)
        {
            Application.Run(new SuperAdventure());
        }

        private void save2_Click(object sender, EventArgs e)
        {

        }

        private void save3_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {

        }

        private void saveName1_Click(object sender, EventArgs e)
        {

        }

        private void saveName2_Click(object sender, EventArgs e)
        {

        }

        private void saveName3_Click(object sender, EventArgs e)
        {

        }
    }
}
