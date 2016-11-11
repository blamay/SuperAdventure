using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class Vendor : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public BindingList<InventoryItem> Inventory { get; private set; }

        public Vendor (string name)
        {
            Name = name;
            Inventory = new BindingList<InventoryItem>();
        }

        public void AddItemToInventory(Item itemToAdd, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if(item == null)
            {
                //They didn't have the item so add it
                Inventory.Add(new InventoryItem(itemToAdd, quantity));
            }

            else
            {
                item.Quantity += quantity;
            }

            OnPropertyChanged("Inventory");
        }

        public void RemoveItemFromInventory(Item itemToRemove, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);

            if (item == null)
            {
                //The item is not in the player's inventory so ignore it.
     
                //May want to raise an error in the future
            }

            else
            {
                //They have the item in the inventory so decrease quantity
                item.Quantity -= quantity;

                //Din't allow negatives...maybe raise error
                if (item.Quantity < 0)
                {
                    item.Quantity = 0;
                }

                //If the quantity is zero, remove the item from the lsit
                if (item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }

                //Notify the UI that the inventory has changed
                OnPropertyChanged("Inventory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
