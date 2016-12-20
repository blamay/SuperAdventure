using Engine;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SuperAdventure
{
    public partial class SaveScreen : Form
    {
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True;MultipleActiveResultSets=True";
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

            if (_player == null)
            {
                
            } 
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

            if (_player == null)
            {
                SetSaveName setname = new SetSaveName(savingReference);
                setname.ShowDialog();
                Hide();
            }

            else
            {
                this.Hide();
                SuperAdventure save2 = new SuperAdventure(_player, savingReference);
                save2.ShowDialog();
            }
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

        //Not Done
        /*
        private void ReadSaveNames()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // Open the connection, so we can perform SQL commands
                connection.Open();

                // Create a SQL command object, that uses the connection to our database
                // The SqlCommand object is where we create our SQL statement
                #region Load savedGame
                using (SqlCommand savedGameCommand = connection.CreateCommand())
                {
                    savedGameCommand.CommandType = CommandType.Text;
                    // This SQL statement reads the first rows in teh SavedGame table.
                    // For this program, we should only ever have one row,
                    // but this will ensure we only get one record in our SQL query results.
                    savedGameCommand.CommandText = "SELECT PlayerName FROM SavedGame";

                    // Use ExecuteReader when you expect the query to return a row, or rows
                    SqlDataReader reader = savedGameCommand.ExecuteReader();

                    // Check if the query did not return a row/record of data
                    if (!reader.HasRows)
                    {
                        // There is no data in the SavedGame table, 
                        // so return null (no saved player data)
                        return;
                    }

                    // Get the row/record from the data reader
                    reader.Read();

                    // Get the column values for the row/record
                    int maximumHitPoints = (int)reader["MaximumHitPoints"];
                    int gold = (int)reader["Gold"];
                    int experiencePoints = (int)reader["ExperiencePoints"];
                    int currentLocationID = (int)reader["CurrentLocationID"];

                }
                #endregion
            }
        }
        */
        public void SaveScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
