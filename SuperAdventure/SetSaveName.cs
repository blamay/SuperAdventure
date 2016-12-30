using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Engine;

namespace SuperAdventure
{
    public partial class SetSaveName : Form
    {
        public bool IsSQLRunning;
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True;MultipleActiveResultSets=True";
        string SaveNameCommandText = null;
        string TXT_PLAYER_NAME = null;
        public SetSaveName(int saveNumber, bool _isSQLRunning)
        {
            InitializeComponent();

            IsSQLRunning = _isSQLRunning;

            if (saveNumber == 1)
            {
                SaveNameCommandText = "INSERT INTO Name ";
                TXT_PLAYER_NAME = "PlayerName.txt";

            }
            else if (saveNumber == 2)
            {
                SaveNameCommandText = "INSERT INTO Name2 ";
                TXT_PLAYER_NAME = "PlayerName2.txt";
            }

            else
            {
                SaveNameCommandText = "INSERT INTO Name3 ";
                TXT_PLAYER_NAME = "PlayerName3.txt";
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textboxName.Text == "")
            {
                MessageBox.Show("You must enter a valid name.");
                return;
            }


            if (IsSQLRunning == true)
            {
                try
                {

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        // Open the connection, so we can perform SQL commands
                        connection.Open();

                        using (SqlCommand insertSaveName = connection.CreateCommand())
                        {

                            insertSaveName.CommandType = CommandType.Text;
                            insertSaveName.CommandText = SaveNameCommandText +
                                "(PlayerName) " +
                                "VALUES " +
                                "(@PlayerName)";

                            // Pass the values from the textbox, to the SQL query, using parameters
                            insertSaveName.Parameters.Add("@PlayerName", SqlDbType.VarChar);
                            insertSaveName.Parameters["@PlayerName"].Value = textboxName.Text;


                            // Perform the SQL command.
                            // Use ExecuteNonQuery, because this query does not return any results.
                            insertSaveName.ExecuteNonQuery();
                            Hide();
                        }
                    }

                }

                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                    // MessageBox.Show(e);
                    Debug.WriteLine(exc);

                    var inner = exc.InnerException;
                    while (inner != null)
                    {
                        //display / log / view
                        inner = inner.InnerException;
                    }
                    return;
                }
            }

            //Write name to Text no matter what. Check to make sure this is happening
           File.WriteAllText(TXT_PLAYER_NAME, textboxName.Text);
           Console.WriteLine("SaveName to Text");
           Hide();
            
        }

        private void SetSaveName_FormClosing(object sender, FormClosingEventArgs e)
        {
   //         Application.Exit();
        }
    }
}
