using System.Collections.Generic;
using System.Linq;

using System.Xml;
using System;

using System.ComponentModel;

namespace Engine
{
    public class Player : LivingCreature
    {

        //Variables
        private int _gold;
        private int _experiencePoints;
        private Location _currentLocation;
        private Monster _currentMonster;
        public Weapon CurrentWeapon { get; set; }

        //Wrappers
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged("Gold");
            }
        }
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged("ExperiencePoints");
                OnPropertyChanged("Level");
            }
        }
        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged("CurrentLocation");
            }
        }

        //Lists
        public BindingList<InventoryItem> Inventory { get; set; }
        public BindingList<PlayerQuest> Quests { get; set; }

        public List<Weapon> Weapons
        {
            get
            {
                return Inventory.Where(
                x => x.Details is Weapon).Select(
                x => x.Details as Weapon).ToList();
            }
        }
        public List<HealingPotion> Potions
        {
            get
            {
                return Inventory.Where(
                x => x.Details is HealingPotion).Select(
                x => x.Details as HealingPotion).ToList();
            }
        }


        //Constructor
        private Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints) : base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExperiencePoints = experiencePoints;
            Inventory = new BindingList<InventoryItem>();
            Quests = new BindingList<PlayerQuest>();
        }


        //Xml Saving
        public string ToXmlString()
        {

            XmlDocument playerData = new XmlDocument();

            //Create the top-level XML node
            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);

            //Create the "Stats child node to hold the other player statistics nodes
            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            //Create the child nodes for the Stats Node
            XmlNode currentHitPoints = playerData.CreateElement("CurrentHitPoints");
            currentHitPoints.AppendChild(playerData.CreateTextNode(this.CurrentHitPoints.ToString()));
            stats.AppendChild(currentHitPoints);

            XmlNode maximumHitPoints = playerData.CreateElement("MaximumHitPoints");
            maximumHitPoints.AppendChild(playerData.CreateTextNode(this.MaximumHitPoints.ToString()));
            stats.AppendChild(maximumHitPoints);

            XmlNode gold = playerData.CreateElement("Gold");
            gold.AppendChild(playerData.CreateTextNode(this.Gold.ToString()));
            stats.AppendChild(gold);

            XmlNode experiencePoints = playerData.CreateElement("ExperiencePoints");
            experiencePoints.AppendChild(playerData.CreateTextNode(this.ExperiencePoints.ToString()));
            stats.AppendChild(experiencePoints);

            XmlNode currentLocation = playerData.CreateElement("CurrentLocation");
            currentLocation.AppendChild(playerData.CreateTextNode(this.CurrentLocation.ID.ToString()));
            stats.AppendChild(currentLocation);

            if (CurrentWeapon != null)
            {
                XmlNode currentWeapon = playerData.CreateElement("CurrentWeapon");
                currentWeapon.AppendChild(playerData.CreateTextNode(this.CurrentWeapon.ID.ToString()));
            }

            //Create the "InventoryItems" child node to hold each InventoryItem node
            XmlNode inventoryItems = playerData.CreateElement("InventoryItems");
            player.AppendChild(inventoryItems);

            //Create an "InventoryItem" node for each item in the player's inventory
            foreach (InventoryItem item in this.Inventory)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = item.Details.ID.ToString();
                inventoryItem.Attributes.Append(idAttribute);

                XmlAttribute quantityAttribute = playerData.CreateAttribute("Quantity");
                quantityAttribute.Value = item.Quantity.ToString();
                inventoryItem.Attributes.Append(quantityAttribute);

                inventoryItems.AppendChild(inventoryItem);
            }

            //create the "playerQuests" child node to hold each PlayerQuest node
            XmlNode playerQuests = playerData.CreateElement("PlayerQuests");
            player.AppendChild(playerQuests);

            //Create a playerquest node for each quest the player has acquireed
            foreach (PlayerQuest quest in this.Quests)
            {
                XmlNode playerQuest = playerData.CreateElement("PlayerQuest");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = quest.Details.ID.ToString();
                playerQuest.Attributes.Append(idAttribute);

                XmlAttribute isCompletedAttribute = playerData.CreateAttribute("IsCompleted");
                isCompletedAttribute.Value = quest.IsCompleted.ToString();
                playerQuest.Attributes.Append(isCompletedAttribute);

                playerQuests.AppendChild(playerQuest);
            }

            return playerData.InnerXml; //Xml as a string

        }
        public static Player CreatePlayerFromXmlString(string xmlPlayerData)
        {
            try
            {
                XmlDocument playerData = new XmlDocument();

                playerData.LoadXml(xmlPlayerData);

                int currentHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentHitPoints").InnerText);
                int maximumHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/MaximumHitPoints").InnerText);
                int gold = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Gold").InnerText);
                int experiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ExperiencePoints").InnerText);

                Player player = new Player(currentHitPoints, maximumHitPoints, gold, experiencePoints);

                int currentLocationID = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText);
                player.CurrentLocation = World.LocationByID(currentLocationID);

                if (playerData.SelectSingleNode("/Player/Stats/CurrentWeapon") != null)
                {
                    int currentWeaponID = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText);
                    player.CurrentWeapon = (Weapon)World.ItemByID(currentWeaponID);
                }

                foreach (XmlNode node in playerData.SelectNodes("/Player/InventoryItems/InventoryItem"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    int quantity = Convert.ToInt32(node.Attributes["Quantity"].Value);

                    for (int i = 0; i < quantity; i++)
                    {
                        player.AddItemToInventory(World.ItemByID(id));
                    }
                }

                foreach (XmlNode node in playerData.SelectNodes("/Player/PlayerQuests/PlayerQuest"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    bool isCompleted = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);

                    PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(id));
                    playerQuest.IsCompleted = isCompleted;

                    player.Quests.Add(playerQuest);
                }

                return player;

            }
            catch
            {
                //If there was an error with the XML data, return a default player object
                return Player.CreateDefaultPlayer();
            }
        }

        //SQL Saving
        public static Player CreatePlayerFromDatabase(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int currentLocationID)
        {
            Player player = new Player(currentHitPoints, maximumHitPoints, gold, experiencePoints);

            player.MoveTo(World.LocationByID(currentLocationID));

            return player;
        }

        //Functions
        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(10, 10, 20, 0);
            player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));
            player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);

            return player;
        }
        private void RaiseInventoryChangedEvent(Item item)
        {
            if(item is Weapon)
            {
                OnPropertyChanged("Weapons");
            }

            if(item is HealingPotion)
            {
                OnPropertyChanged("Potions");
            }
        }
        public void RemoveItemFromInventory(Item itemToRemove, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);

            if (item == null)
            {
                //The item is not in the player's inventory so ignore it.

                //Maise raise an error in the future
            }

            else
            {
                //They have the item in the inventory so decrease quantity
                item.Quantity -= quantity;

                //Din't allow negatives...maybe raise error
                if(item.Quantity < 0)
                {
                    item.Quantity = 0;
                }

                //If the quantity is zero, remove the item from the lsit
                if(item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }

                //Notify the UI that the inventory has changed
                RaiseInventoryChangedEvent(itemToRemove);
            }
        }
        public void AddExperiencePoints (int experiencePointsToAdd)
        {
            ExperiencePoints += experiencePointsToAdd;
            MaximumHitPoints = (Level * 10);
        }

        //Quest logic
        public bool HasThisQuest(Quest quest)
        {
            return Quests.Any(pq => pq.Details.ID == quest.ID);
        }
        public bool CompletedThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }
        public bool HasAllQuestCompletionItems(Quest quest)
        {
            //See if the player has all the items jneeded to complete the quest here
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                if(!Inventory.Any(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quantity))
                {
                    return false;
                }
            }

            //If we got here, then the player must have all the required items, and enough of them to compelte the quest
            return true;
        }
        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                //SingleOrDefault can only return 1 specific item. Use another when searching for multiples
                InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == qci.Details.ID);

                //Need to check for null when using SingleOrDefault
                if (item != null)
                {
                    RemoveItemFromInventory(item.Details, qci.Quantity);
                }
            }
        }
        public void AddItemToInventory(Item itemToAdd, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if(item == null)
            {
                //They didn't have the item, so add it to their inventory, with a quantity of 1
                Inventory.Add(new InventoryItem(itemToAdd, quantity));
            }

            else
            {
                item.Quantity += quantity;
            }

            RaiseInventoryChangedEvent(itemToAdd);
        }
        public void MarkQuestCompleted(Quest quest)
        {
            //Find the quest in the player's quest list
            PlayerQuest playerQuest = Quests.SingleOrDefault(pq => pq.Details.ID == quest.ID);

            if(playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }
    
        //Movement
        public void MoveTo(Location newLocation)
        {

            if (!HasRequiredItemToEnterThisLocation(newLocation))
            {
                RaiseMessage("You must have a " + newLocation.ItemRequiredfToEnter.Name + " to enter this location.");
                return;
            }

            //update the player's current location
            CurrentLocation = newLocation;

            //Completely heal the player
            CurrentHitPoints = MaximumHitPoints;

            if (newLocation.HasAQuest)
            {
                // See if the player already has the quest
                if (!HasThisQuest(newLocation.QuestAvailableHere))
                {
                    GiveQuestToPlayer(newLocation);
                }
                else
                {
                    //If the player has not completed the quest yet
                    if (!(bool)CompletedThisQuest(newLocation.QuestAvailableHere))
                    {
                        //See if the player has all the items needed to complete the quest
                        if (HasAllQuestCompletionItems(newLocation.QuestAvailableHere))
                        {
                            CompleteQuest(newLocation);
                        }
                    }
                }
            }

            SetCurrentMonsters(newLocation);
        }

        private void SetCurrentMonsters(Location newLocation)
        {
            // Does the location have a monster?
            if (newLocation.MonsterLivingHere != null)
            {

                RaiseMessage("You see a " + newLocation.MonsterLivingHere.Name);

                // Make a new monster, using the values from the standard monster in the World.Monster list
                Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

                _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage, standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    _currentMonster.LootTable.Add(lootItem);
                }
            }

            else _currentMonster = null;
        }

        private void CompleteQuest(Location newLocation)
        {
            //Display Message
            RaiseMessage("");
            RaiseMessage("You completed the " +
            newLocation.QuestAvailableHere.Name +
            " quest.");

            // Remove quest items from inventory
            RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

            //give quest rewards
            RaiseMessage("You receive: ");
            RaiseMessage(newLocation.QuestAvailableHere.RewardExperiencePoints +
                " experience points");
            RaiseMessage(newLocation.QuestAvailableHere.RewardGold +
                 " gold");
            RaiseMessage(newLocation.QuestAvailableHere.RewardItem.Name);
            RaiseMessage("");

            AddExperiencePoints(newLocation.QuestAvailableHere.RewardExperiencePoints);
            Gold += newLocation.QuestAvailableHere.RewardGold;

            // Add the reward item to the player's inventory
            AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

            //Mark the quest as completed
            MarkQuestCompleted(newLocation.QuestAvailableHere);
        }
        private void GiveQuestToPlayer(Location newLocation)
        {
            // Display the messages
            RaiseMessage("You receive the " + newLocation.QuestAvailableHere.Name + " quest.");
            RaiseMessage(newLocation.QuestAvailableHere.Description);
            RaiseMessage("To complete it, return with:");
            foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
            {
                if (qci.Quantity == 1)
                {
                    RaiseMessage(qci.Quantity.ToString() + " " + qci.Details.Name);
                }

                else
                {
                    RaiseMessage(qci.Quantity.ToString() + " " + qci.Details.NamePlural);
                }
            }

            RaiseMessage("");

            //Add the quest to the player's quest list
            Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
        }

        public void MoveNorth()
        {
            if (CurrentLocation.LocationToNorth != null)
            {
                MoveTo(CurrentLocation.LocationToNorth);
            }
        }
        public void MoveEast()
        {
            if (CurrentLocation.LocationToEast != null)
            {
                MoveTo(CurrentLocation.LocationToEast);
            }
        }
        public void MoveSouth()
        {
            if (CurrentLocation.LocationToSouth != null)
            {
                MoveTo(CurrentLocation.LocationToSouth);
            }
        }
        public void MoveWest()
        {
            if (CurrentLocation.LocationToWest != null)
            {
                MoveTo(CurrentLocation.LocationToWest);
            }
        }
        public void MoveCurrentLocation()
        {
           MoveTo(CurrentLocation);
        }
        private void MoveHome()
        {
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredfToEnter == null)
            {
                //There is no required item for this location, so return "true"
                return true;
            }

            //See if the player has the required item in their inventory
            return Inventory.Any(ii => ii.Details.ID == location.ItemRequiredfToEnter.ID);

        }

        //Actions
        public void UseWeapon(Weapon weapon)
        {
         
            //Determine the amount of damage to do to the monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(weapon.MinimumDamage, weapon.MaximumDamage);

            //Apply the damage to the monster's CurrentHitPoints
            _currentMonster.CurrentHitPoints -= damageToMonster;

            //Display message
            RaiseMessage("You hit the " + _currentMonster.Name + " for " + damageToMonster + " points.");

            //Check if the monster is dead
            if (_currentMonster.CurrentHitPoints <= 0)
            {
                //Monster is dead
                RaiseMessage("");
                RaiseMessage("You defeated the " + _currentMonster.Name);

                //Give player experience points for killing the monster
                AddExperiencePoints(_currentMonster.RewardExperiencePoints);
                RaiseMessage("You receive " + _currentMonster.RewardExperiencePoints + " experience points");

                //Give player gold for killing the monster
                Gold += _currentMonster.RewardGold;
                RaiseMessage("You receive " + _currentMonster.RewardGold + " gold");

                //get random loot items from the monster
                List<InventoryItem> lootedItems = new List<InventoryItem>();

                //Add items to the lootedItems list, comparing a random number to the drop percentage
                foreach (LootItem lootItem in _currentMonster.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                //If no items were randomly selected, then add the default loot item(s).
                if (lootedItems.Count == 0)
                {
                    foreach (LootItem lootItem in _currentMonster.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                //Add the looted items to the player's inventory
                foreach (InventoryItem inventoryItem in lootedItems)
                {
                    AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        RaiseMessage("You loot " + inventoryItem.Quantity + " " + inventoryItem.Details.Name);
                    }
                    else
                    {
                        RaiseMessage("You loot" + inventoryItem.Quantity + " " + inventoryItem.Details.NamePlural);
                    }
                }


                //Add a blank line to the messages box, just for appearance.
                RaiseMessage("");

                //Move player to current location to heal and create new monster
                MoveTo(CurrentLocation);
            }

            else
            {
                //Monster is still alive

                //Determine the amount of damage the monster does to the player
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

                //Display message
                RaiseMessage("The " + _currentMonster.Name + " dealt " + damageToPlayer + " points of damage.");

                //Subtract damage from the player
                CurrentHitPoints -= damageToPlayer;

                if (CurrentHitPoints <= 0)
                {
                    //Display Message
                    RaiseMessage("The " + _currentMonster.Name + " killed you.");

                    //move player to Home
                    MoveHome();

                }
            }
        }
        public void UsePotion(HealingPotion potion)
        {
           
            //Add healing amount to the player's current hit points
            CurrentHitPoints = (CurrentHitPoints + potion.AmountToHeal);

            //CuurrentHitPoints cannot exceed player's maximumHitPoints
            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }

            //Remove the potion from the player's inventory
            RemoveItemFromInventory(potion, 1);

            //Display message
            RaiseMessage("You drink a " + potion.Name);

            //Monster gets their turn to attack

            //Determine the amount of damage the monster does to the player
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMonster.MaximumDamage);

            //Display message
            RaiseMessage("The " + _currentMonster.Name + " dealt " + damageToPlayer + " points of damage.");

            //Subtract damage from the player
            CurrentHitPoints -= damageToPlayer;

            if (CurrentHitPoints <= 0)
            {
                //Display Message
                RaiseMessage("The " + _currentMonster.Name + " killed you.");

                //move player to Home
                MoveHome();
            }
        }

        //Messages
        public event EventHandler<MessageEventArgs> OnMessage;
        private void RaiseMessage( string message, bool addExtraNewLine = false)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(message, addExtraNewLine));
        }
    }
}
