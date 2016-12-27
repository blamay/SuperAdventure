using Engine;
using System;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class SaveScreen : Form
    {
        public SaveScreen()
        {
            InitializeComponent();
            readNames();

        }

        private void readNames()
        {
           // try
           // {
           //     save1.Text = PlayerDataMapper.ReadSQLName(1);
           //     save2.Text = PlayerDataMapper.ReadSQLName(2);
           //     save3.Text = PlayerDataMapper.ReadSQLName(3);
           // }

            //catch
            //{
                Console.WriteLine("No SQL Detected");
                save1.Text = PlayerDataMapper.ReadTXTName(1);
                save2.Text = PlayerDataMapper.ReadTXTName(2);
                save3.Text = PlayerDataMapper.ReadTXTName(3);
            //}
        }

        private void save1_Click(object sender, EventArgs e)
        {
            LoadSaveGame(1);
        }
        private void save2_Click(object sender, EventArgs e)
        {
            LoadSaveGame(2);
        }
        private void save3_Click(object sender, EventArgs e)
        {
            LoadSaveGame(3);
        }

        private void LoadSaveGame (int saveNumber)
        {
            Player _player = null;//PlayerDataMapper.CreateFromDatabase(saveNumber);

            if (_player == null)
            {
                _player = Player.CreatePlayerFromXmlString(saveNumber);

                if (_player == null)
                {
                    SetSaveName setname = new SetSaveName(saveNumber);
                    Hide();
                    setname.ShowDialog();
                    SuperAdventure superadventure = new SuperAdventure(_player, saveNumber);
                    superadventure.ShowDialog();
                }
                else
                {
                    Hide();
                    SuperAdventure superadventure = new SuperAdventure(_player, saveNumber);
                    superadventure.ShowDialog();
                }

            }


        }

        private void buttonDelete1_Click(object sender, EventArgs e)
        {
            PlayerDataMapper.DeleteSQLSaveGame(1);
            PlayerDataMapper.DeleteXMLSaveGame(1);
            readNames();
        }
        private void buttonDelete2_Click(object sender, EventArgs e)
        {
            PlayerDataMapper.DeleteSQLSaveGame(2);
            PlayerDataMapper.DeleteXMLSaveGame(2);
            readNames();
        }
        private void buttonDelete3_Click(object sender, EventArgs e)
        {
            PlayerDataMapper.DeleteSQLSaveGame(3);
            PlayerDataMapper.DeleteXMLSaveGame(3);
            readNames();
        }

        private void SaveScreen_Load(object sender, EventArgs e)
        {

        }

        public void SaveScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }
    }
}
