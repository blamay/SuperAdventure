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

namespace SuperAdventure
{
    public partial class SetSaveName : Form
    {
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True;MultipleActiveResultSets=True";
        string SaveNameCommandText = null;
        public SetSaveName(int saveNumber)
        {
            InitializeComponent();

            if (saveNumber == 1)
            {
                 SaveNameCommandText = "INSERT INTO Name ";
            }
            else if (saveNumber ==2)
            {
                 SaveNameCommandText = "INSERT INTO Name2 ";
            }

            else SaveNameCommandText = "INSERT INTO Name3 ";
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (textboxName.Text == null)
            {
                MessageBox.Show("You must enter a valid name.");
                return;
            }

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

        private void SetSaveName_FormClosing(object sender, FormClosingEventArgs e)
        {
   //         Application.Exit();
        }
    }
}
