using System;
using System.Threading;
using System.Windows.Forms;
using Engine;

namespace SuperAdventure
{
    public partial class Title : Form
    {
        public bool IsSQLRunning;
        //Thread th;
        public Title()
        {
            InitializeComponent();
            IsSQLRunning = PlayerDataMapper.CheckForSQLConnection();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            SaveScreen savescreen = new SaveScreen(IsSQLRunning);
            savescreen.ShowDialog();
        }
        
         

    }
}
