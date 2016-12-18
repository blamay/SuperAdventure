using Engine;
using System;
using System.Threading;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class SaveScreen : Form
    {
        private static int savingReference;
        //Thread th;
        public SaveScreen()
        {
            InitializeComponent();
        }

        

        private void save1_Click(object sender, EventArgs e)
        {

            //Alternate way to change forms
            /*
            this.Close();
            th = new Thread(openSuperAdventure);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            */
            //Create player
            savingReference = 1;
            Player _player = PlayerDataMapper.CreateFromDatabase(1);
            this.Hide();
            SuperAdventure save1 = new SuperAdventure(_player, savingReference);
            save1.ShowDialog();
        }

        /*
        private void openSuperAdventure(object obj)
        {
            Application.Run(new SuperAdventure());
        }
        */

        private void save2_Click(object sender, EventArgs e)
        {
            savingReference = 2;
            Player _player = PlayerDataMapper.CreateFromDatabase(2);
            this.Hide();
            SuperAdventure save2 = new SuperAdventure(_player, savingReference);
            save2.ShowDialog();
        }

        private void save3_Click(object sender, EventArgs e)
        {
            savingReference = 3;
            Player _player = PlayerDataMapper.CreateFromDatabase(3);
            this.Hide();
            SuperAdventure save3 = new SuperAdventure(_player, savingReference);
            save3.ShowDialog();
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

        private void SaveScreen_Load(object sender, EventArgs e)
        {

        }

        public void SaveScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
