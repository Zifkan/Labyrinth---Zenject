using System.Collections.Generic;
using Assets.Scripts.GameInterface;
using Assets.Scripts.InteractiveObjects;
using Zenject;

namespace Assets.Scripts.Character.Inventory
{
    public class GameInventory
    {
        private List<InventoryItem> _items = new List<InventoryItem>();

        private const int InventoryCapasity = 3;

        [Inject]
        private GameInterfacePresenter InterfacePresenter { get; set; }

        public List<InventoryItem> Items
        {
            get { return _items; }
        }

        public bool AddItem(int linkedItemX,int linkedItemY, string name)
        {
            if (Items.Count >= InventoryCapasity) return false;
            Items.Add(new InventoryItem
            {
                Name = name,
                LinkedItemX = linkedItemX,
                LinkedItemY = linkedItemY
            });
            InterfacePresenter.ShowInventory(_items);
            return true;
        }

        public void RemoveItem(InventoryItem item)
        {
            _items.Remove(item);
            InterfacePresenter.ShowInventory(_items);
        }
    }
}
