using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Engine
{
    public static class PlayerDataMapper
    {
        private static readonly string _connectionString = "Data Source=(local);Initial Catalog=SuperAdventure;Integrated Security=True;MultipleActiveResultSets=True";
        private static int saveNumber = 0;
        public static Player CreateFromDatabase(int save)
        {
            #region try

            //Set Commands for savenumber
            string savedGameCommandText, questCommandText, inventoryCommandText;

            if (save == 1)
            {
                savedGameCommandText = "SELECT TOP 1 * FROM SavedGame";
                questCommandText = "SELECT * FROM Quest";
                inventoryCommandText = "SELECT * FROM Inventory";
            }
            else if (save == 2)
            {
                savedGameCommandText = "SELECT TOP 1 * FROM SavedGame2";
                questCommandText = "SELECT * FROM Quest2";
                inventoryCommandText = "SELECT * FROM Inventory2";
            }
            else
            {
                savedGameCommandText = "SELECT TOP 1 * FROM SavedGame3";
                questCommandText = "SELECT * FROM Quest3";
                inventoryCommandText = "SELECT * FROM Inventory3";
            }

            try
            {
                // This is our connection to the database
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection, so we can perform SQL commands
                    connection.Open();

                    Player player;

                    // Create a SQL command object, that uses the connection to our database
                    // The SqlCommand object is where we create our SQL statement
                    #region Load savedGame
                    using (SqlCommand savedGameCommand = connection.CreateCommand())
                    {
                        savedGameCommand.CommandType = CommandType.Text;
                        // This SQL statement reads the first rows in teh SavedGame table.
                        // For this program, we should only ever have one row,
                        // but this will ensure we only get one record in our SQL query results.
                        savedGameCommand.CommandText = savedGameCommandText;

                        // Use ExecuteReader when you expect the query to return a row, or rows
                        SqlDataReader reader = savedGameCommand.ExecuteReader();

                        // Check if the query did not return a row/record of data
                        if (!reader.HasRows)
                        {
                            // There is no data in the SavedGame table, 
                            // so return null (no saved player data)
                            return null;
                        }

                        // Get the row/record from the data reader
                        reader.Read();

                        // Get the column values for the row/record
                        int currentHitPoints = (int)reader["CurrentHitPoints"];
                        int maximumHitPoints = (int)reader["MaximumHitPoints"];
                        int gold = (int)reader["Gold"];
                        int experiencePoints = (int)reader["ExperiencePoints"];
                        int currentLocationID = (int)reader["CurrentLocationID"];

                        // Create the Player object, with the saved game values
                        player = Player.CreatePlayerFromDatabase(currentHitPoints, maximumHitPoints, gold,
                            experiencePoints, currentLocationID);
                    }
                    #endregion

                    // Read the rows/records from the Quest table, and add them to the player
                    #region Load quests
                    using (SqlCommand questCommand = connection.CreateCommand())
                    {
                        questCommand.CommandType = CommandType.Text;
                        questCommand.CommandText = questCommandText;

                        SqlDataReader reader = questCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int questID = (int)reader["QuestID"];
                                bool isCompleted = (bool)reader["IsCompleted"];

                                // Build the PlayerQuest item, for this row
                                PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(questID));
                                playerQuest.IsCompleted = isCompleted;

                                // Add the PlayerQuest to the player's property
                                player.Quests.Add(playerQuest);
                            }
                        }
                    }
                    #endregion

                    // Read the rows/records from the Inventory table, and add them to the player
                    #region Load Inventory
                    using (SqlCommand inventoryCommand = connection.CreateCommand())
                    {
                        inventoryCommand.CommandType = CommandType.Text;
                        inventoryCommand.CommandText = inventoryCommandText;

                        SqlDataReader reader = inventoryCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int inventoryItemID = (int)reader["InventoryItemID"];
                                int quantity = (int)reader["Quantity"];

                                // Add the item to the player's inventory
                                player.AddItemToInventory(World.ItemByID(inventoryItemID), quantity);
                            }
                        }
                    }
                    #endregion

                    // Now that the player has been built from the database, return it.
                    return player;
                }
            }
            #endregion

            #region catch
            catch (Exception e)
            {
                // Ignore errors. If there is an error, this function will return a "null" player.
                // at least one of these..
                Console.WriteLine(e);
               // MessageBox.Show(e);
                Debug.WriteLine(e);

                var inner = e.InnerException;
                while (inner != null)
                {
                    //display / log / view
                    inner = inner.InnerException;
                }
            }
            #endregion

            return null;
        }

        public static void SaveToDatabase(Player player, int saveNumber)
        {
            #region try
            try
            {
                string existingRowCountCommandText, insertSavedGameCommandText, updateSavedGameCommandText, deleteQuestsCommandText, insertQuestsCommandText, deleteInventoryCommandText, insertInventoryCommandText;
                if (saveNumber == 1)
                {
                    existingRowCountCommandText = "SELECT count(*) FROM SavedGame";
                    insertSavedGameCommandText = "INSERT INTO SavedGame ";
                    updateSavedGameCommandText = "UPDATE SavedGame ";
                    deleteQuestsCommandText = "DELETE FROM Quest";
                    insertQuestsCommandText = "INSERT INTO Quest (QuestID, IsCompleted) VALUES (@QuestID, @IsCompleted)";
                    deleteInventoryCommandText = "DELETE FROM Inventory";
                    insertInventoryCommandText = "INSERT INTO Inventory (InventoryItemID, Quantity) VALUES (@InventoryItemID, @Quantity)";
                }
                else if (saveNumber == 2)
                {
                    existingRowCountCommandText = "SELECT count(*) FROM SavedGame2";
                    insertSavedGameCommandText = "INSERT INTO SavedGame2 ";
                    updateSavedGameCommandText = "UPDATE SavedGame2 ";
                    deleteQuestsCommandText = "DELETE FROM Quest2";
                    insertQuestsCommandText = "INSERT INTO Quest2 (QuestID, IsCompleted) VALUES (@QuestID, @IsCompleted)";
                    deleteInventoryCommandText = "DELETE FROM Inventory2";
                    insertInventoryCommandText = "INSERT INTO Inventory2 (InventoryItemID, Quantity) VALUES (@InventoryItemID, @Quantity)";
                }
                else
                {
                    existingRowCountCommandText = "SELECT count(*) FROM SavedGame3";
                    insertSavedGameCommandText = "INSERT INTO SavedGame3 ";
                    updateSavedGameCommandText = "UPDATE SavedGame3 ";
                    deleteQuestsCommandText = "DELETE FROM Quest3";
                    insertQuestsCommandText = "INSERT INTO Quest3 (QuestID, IsCompleted) VALUES (@QuestID, @IsCompleted)";
                    deleteInventoryCommandText = "DELETE FROM Inventory3";
                    insertInventoryCommandText = "INSERT INTO Inventory3 (InventoryItemID, Quantity) VALUES (@InventoryItemID, @Quantity)";
                }
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open the connection, so we can perform SQL commands
                    connection.Open();


                    // Insert/Update data in SavedGame table
                    #region SavedGame
                    using (SqlCommand existingRowCountCommand = connection.CreateCommand())
                    {
                        existingRowCountCommand.CommandType = CommandType.Text;
                        existingRowCountCommand.CommandText = existingRowCountCommandText;

                        // Use ExecuteScalar when your query will return one value
                        int existingRowCount = (int)existingRowCountCommand.ExecuteScalar();

                        if (existingRowCount == 0)
                        {
                            // There is no existing row, so do an INSERT
                            using (SqlCommand insertSavedGame = connection.CreateCommand())
                            {
                                insertSavedGame.CommandType = CommandType.Text;
                                insertSavedGame.CommandText =
                                    insertSavedGameCommandText +
                                    "(CurrentHitPoints, MaximumHitPoints, Gold, ExperiencePoints, CurrentLocationID) " +
                                    "VALUES " +
                                    "(@CurrentHitPoints, @MaximumHitPoints, @Gold, @ExperiencePoints, @CurrentLocationID)";

                                // Pass the values from the player object, to the SQL query, using parameters
                                insertSavedGame.Parameters.Add("@CurrentHitPoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@CurrentHitPoints"].Value = player.CurrentHitPoints;
                                insertSavedGame.Parameters.Add("@MaximumHitPoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@MaximumHitPoints"].Value = player.MaximumHitPoints;
                                insertSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                insertSavedGame.Parameters["@Gold"].Value = player.Gold;
                                insertSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@ExperiencePoints"].Value = player.ExperiencePoints;
                                insertSavedGame.Parameters.Add("@CurrentLocationID", SqlDbType.Int);
                                insertSavedGame.Parameters["@CurrentLocationID"].Value = player.CurrentLocation.ID;

                                // Perform the SQL command.
                                // Use ExecuteNonQuery, because this query does not return any results.
                                insertSavedGame.ExecuteNonQuery();
                            }
                        }

                        else
                        {
                            // There is an existing row, so do an UPDATE
                            using (SqlCommand updateSavedGame = connection.CreateCommand())
                            {
                                updateSavedGame.CommandType = CommandType.Text;
                                updateSavedGame.CommandText =
                                    updateSavedGameCommandText +
                                    "SET CurrentHitPoints = @CurrentHitPoints, " +
                                    "MaximumHitPoints = @MaximumHitPoints, " +
                                    "Gold = @Gold, " +
                                    "ExperiencePoints = @ExperiencePoints, " +
                                    "CurrentLocationID = @CurrentLocationID";

                                // Pass the values from the player object, to the SQL query, using parameters
                                // Using parameters helps make your program more secure.
                                // It will prevent SQL injection attacks.
                                updateSavedGame.Parameters.Add("@CurrentHitPoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentHitPoints"].Value = player.CurrentHitPoints;
                                updateSavedGame.Parameters.Add("@MaximumHitPoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@MaximumHitPoints"].Value = player.MaximumHitPoints;
                                updateSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                updateSavedGame.Parameters["@Gold"].Value = player.Gold;
                                updateSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@ExperiencePoints"].Value = player.ExperiencePoints;
                                updateSavedGame.Parameters.Add("@CurrentLocationID", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentLocationID"].Value = player.CurrentLocation.ID;

                                // Perform the SQL command.
                                // Use ExecuteNonQuery, because this query does not return any results.
                                updateSavedGame.ExecuteNonQuery();
                            }
                        }
                    }
                    #endregion

                    // The Quest and Inventory tables might have more, or less, rows in the database
                    // than what the player has in their properties.
                    // So, when we save the player's game, we will delete all the old rows
                    // and add in all new rows.
                    // This is easier than trying to add/delete/update each individual rows

                    // Delete existing Quest rows
                    using (SqlCommand deleteQuestsCommand = connection.CreateCommand())
                    {
                        deleteQuestsCommand.CommandType = CommandType.Text;
                        deleteQuestsCommand.CommandText = deleteQuestsCommandText;

                        deleteQuestsCommand.ExecuteNonQuery();
                    }

                    // Insert Quest rows, from the player object
                    foreach (PlayerQuest playerQuest in player.Quests)
                    {
                        using (SqlCommand insertQuestCommand = connection.CreateCommand())
                        {
                            insertQuestCommand.CommandType = CommandType.Text;
                            insertQuestCommand.CommandText = insertQuestsCommandText;

                            insertQuestCommand.Parameters.Add("@QuestID", SqlDbType.Int);
                            insertQuestCommand.Parameters["@QuestID"].Value = playerQuest.Details.ID;
                            insertQuestCommand.Parameters.Add("@IsCompleted", SqlDbType.Bit);
                            insertQuestCommand.Parameters["@IsCompleted"].Value = playerQuest.IsCompleted;

                            insertQuestCommand.ExecuteNonQuery();
                        }
                    }

                    // Delete existing Inventory rows
                    using (SqlCommand deleteInventoryCommand = connection.CreateCommand())
                    {
                        deleteInventoryCommand.CommandType = CommandType.Text;
                        deleteInventoryCommand.CommandText = deleteInventoryCommandText;

                        deleteInventoryCommand.ExecuteNonQuery();
                    }

                    // Insert Inventory rows, from the player object
                    foreach (InventoryItem inventoryItem in player.Inventory)
                    {
                        using (SqlCommand insertInventoryCommand = connection.CreateCommand())
                        {
                            insertInventoryCommand.CommandType = CommandType.Text;
                            insertInventoryCommand.CommandText = insertInventoryCommandText;

                            insertInventoryCommand.Parameters.Add("@InventoryItemID", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@InventoryItemID"].Value = inventoryItem.Details.ID;
                            insertInventoryCommand.Parameters.Add("@Quantity", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@Quantity"].Value = inventoryItem.Quantity;

                            insertInventoryCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            #endregion

            #region catch
            catch (Exception e)
            {
                // We are going to ignore erros, for now.
                Console.WriteLine(e);
                // MessageBox.Show(e);
                Debug.WriteLine(e);

                var inner = e.InnerException;
                while (inner != null)
                {
                    //display / log / view
                    inner = inner.InnerException;
                }
            }
            #endregion
        }

    }
}
