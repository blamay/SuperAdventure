using System;
using System.Threading;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class Title : Form
    {

        Thread th;
        public Title()
        {
            InitializeComponent();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(opennewform);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void opennewform(object obj)
        {
            Application.Run(new SaveScreen());
        }
    }
}
