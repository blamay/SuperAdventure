using Engine;
using System;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class SaveScreen : Form
    {
        public bool IsSQLRunning;
        public SaveScreen(bool _isSQLRunning)
        {
            InitializeComponent();
            IsSQLRunning = _isSQLRunning;
            readNames();
        }

        private void readNames()
        {
            if(IsSQLRunning == true)
            {
                save1.Text = PlayerDataMapper.ReadSQLName(1);
                save2.Text = PlayerDataMapper.ReadSQLName(2);
                save3.Text = PlayerDataMapper.ReadSQLName(3);
            }

            else
            {
                save1.Text = PlayerDataMapper.ReadTXTName(1);
                save2.Text = PlayerDataMapper.ReadTXTName(2);
                save3.Text = PlayerDataMapper.ReadTXTName(3);
            }
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
            Player _player;

            if (IsSQLRunning == true)
            {
                _player = PlayerDataMapper.CreateFromDatabase(saveNumber);
            }

            else _player = Player.CreatePlayerFromXmlString(saveNumber);

                if (_player == null)
                {
                    SetSaveName setname = new SetSaveName(saveNumber, IsSQLRunning);
                    Hide();
                    setname.ShowDialog();
                    SuperAdventure superadventure = new SuperAdventure(_player, saveNumber, IsSQLRunning);
                    superadventure.ShowDialog();
                }
                else
                {
                    Hide();
                    SuperAdventure superadventure = new SuperAdventure(_player, saveNumber, IsSQLRunning);
                    superadventure.ShowDialog();
                }
        }

        private void buttonDelete1_Click(object sender, EventArgs e)
        {
            DeleteGame(1);
        }
        private void buttonDelete2_Click(object sender, EventArgs e)
        {
            DeleteGame(2);
        }
        private void buttonDelete3_Click(object sender, EventArgs e)
        {
            DeleteGame(3);
        }

        private void DeleteGame(int saveNum)
        {
            if(IsSQLRunning == true)
            {
                PlayerDataMapper.DeleteSQLSaveGame(saveNum);
            }

            PlayerDataMapper.DeleteXMLSaveGame(saveNum);
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
