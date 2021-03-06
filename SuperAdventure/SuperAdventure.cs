﻿using Engine;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player _player;

        //Constant
        private string PLAYER_DATA_FILE_NAME = "PlayerData.xml";
        private static int saveNumber;
        private bool IsSQLRunning;
        //Load game file
        public SuperAdventure(Player _player2, int saveNum, bool _isSQLRunning)
        {
            InitializeComponent();

            IsSQLRunning = _isSQLRunning;

            if (saveNum == 1)
            {
                PLAYER_DATA_FILE_NAME = "PlayerData.xml";
            }
            else if (saveNum == 2)
            {
                PLAYER_DATA_FILE_NAME = "PlayerData2.xml";
            }
            else PLAYER_DATA_FILE_NAME = "PlayerData3.xml";

            //remove
            _player = _player2;
            saveNumber = saveNum;

            if(_player == null)
            {
                _player = Player.CreateDefaultPlayer();
            }

            _player.MoveCurrentLocation();

            lblHitPoints.DataBindings.Add("Text", _player, "CurrentHitPoints");
            lblGold.DataBindings.Add("Text", _player, "Gold");
            lblExperience.DataBindings.Add("Text", _player, "ExperiencePoints");
            lblLevel.DataBindings.Add("Text", _player, "Level");

            dgvInventory.RowHeadersVisible = false;
            dgvInventory.AutoGenerateColumns = false;

            dgvInventory.DataSource = _player.Inventory;

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 197,
                DataPropertyName = "Description"
            });

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity",
                DataPropertyName = "Quantity"
            });

            dgvQuests.RowHeadersVisible = false;
            dgvQuests.AutoGenerateColumns = false;

            dgvQuests.DataSource = _player.Quests;

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 197,
                DataPropertyName = "Name"
            });

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Done?",
                DataPropertyName = "IsCompleted"
            });

            cboWeapons.DataSource = _player.Weapons;
            cboWeapons.DisplayMember = "Name";
            cboWeapons.ValueMember = "Id";

            if(_player.CurrentWeapon != null)
            {
                cboWeapons.SelectedItem = _player.CurrentWeapon;
            }

            cboPotions.DataSource = _player.Potions;
            cboPotions.DisplayMember = "Name";
            cboPotions.ValueMember = "Id";

            _player.OnMessage += DisplayMessage;

            _player.PropertyChanged += PlayerOnPropertyChanged;

            _player.MoveCurrentLocation();
            cboPotions.ValueMember = "Id";
        }

        //Buttons
        private void btnNorth_Click(object sender, EventArgs e)
        {
            _player.MoveNorth();
        }
        private void btnSouth_Click(object sender, EventArgs e)
        {
            _player.MoveSouth();
        }
        private void btnEast_Click(object sender, EventArgs e)
        {
            _player.MoveEast();
        }
        private void btnWest_Click(object sender, EventArgs e)
        {
            _player.MoveWest();
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            _player.UseWeapon(currentWeapon);
        }
        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            _player.UsePotion(potion);
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            TradingScreen tradingScreen = new TradingScreen(_player);
            tradingScreen.StartPosition = FormStartPosition.CenterParent;
            tradingScreen.ShowDialog(this);
        }

        //Saves selected Weapon
        private void cboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cboWeapons.SelectedItem;
        }

        //Updates combobox data when inventory changes
        private void PlayerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Weapons")
            {
                cboWeapons.DataSource = _player.Weapons;

                if(!_player.Weapons.Any())
                {
                    cboWeapons.Visible = _player.Weapons.Any();
                    btnUseWeapon.Visible = _player.Weapons.Any();
                }
            }

            if(propertyChangedEventArgs.PropertyName == "Potions")
            {
                cboPotions.DataSource = _player.Potions;

                if(!_player.Potions.Any())
                {
                    cboPotions.Visible = _player.Potions.Any();
                    btnUsePotion.Visible = _player.Potions.Any();
                }
            }

            if(propertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                //Trade screen
                btnTrade.Visible = (_player.CurrentLocation.VendorWorkingHere != null);

                //Show/hide available movement buttons

                btnNorth.Visible = (_player.CurrentLocation.LocationToNorth != null);
                btnSouth.Visible = (_player.CurrentLocation.LocationToSouth != null);
                btnEast.Visible = (_player.CurrentLocation.LocationToEast != null);
                btnWest.Visible = (_player.CurrentLocation.LocationToWest != null);

                //Display current location name and description
                rtbLocation.Text = _player.CurrentLocation.Name + Environment.NewLine;
                rtbLocation.Text += _player.CurrentLocation.Description + Environment.NewLine;

                if(_player.CurrentLocation.MonsterLivingHere == null)
                {
                    cboWeapons.Visible = false;
                    cboPotions.Visible = false;
                    btnUseWeapon.Visible = false;
                    btnUsePotion.Visible = false;
                }

                else
                {
                    cboWeapons.Visible = _player.Weapons.Any();
                    cboPotions.Visible = _player.Potions.Any();
                    btnUseWeapon.Visible = _player.Weapons.Any();
                    btnUsePotion.Visible = _player.Potions.Any();
                }
            }
        }

        private void DisplayMessage(object sender, MessageEventArgs messageEventArgs)
        {
            rtbMessages.Text += messageEventArgs.Message + Environment.NewLine;

            if(messageEventArgs.AddExtraNewLine)
            {
                rtbMessages.Text += Environment.NewLine;
            }

            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
        private void SuperAdventure_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(IsSQLRunning == true)
            {
                PlayerDataMapper.SaveToDatabase(_player, saveNumber);
            }
            File.WriteAllText(PLAYER_DATA_FILE_NAME, _player.ToXmlString());
            Application.Exit();
        }
    }
   
}
