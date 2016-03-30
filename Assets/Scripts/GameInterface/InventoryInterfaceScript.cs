using System.Collections.Generic;
using Assets.Scripts.Character.Inventory;
using Assets.Scripts.InteractiveObjects;
using UnityEngine;

namespace Assets.Scripts.GameInterface
{
    public class InventoryInterfaceScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _inventoryPanel;

        [SerializeField]
        private ItemPanelScript _itemPanelPrefab;

        [SerializeField] 
        private float _topOffset;

        [SerializeField]
        private float _leftOffset;

        [SerializeField]
        private float _panelsOffset;

        private List<ItemPanelScript> _itemPanels = new List<ItemPanelScript>();
        public void RefreshInventory(List<InventoryItem> items)
        {
            //RectTransform rowRectTransform = _itemPanelPrefab.GetComponent<RectTransform>();
            //RectTransform containerRectTransform = _inventoryPanel.gameObject.GetComponent<RectTransform>();

            var count = 0;
            foreach (var panel in _itemPanels)
            {
                Destroy(panel.gameObject);
            }
            _itemPanels.Clear();
            foreach (var item in items)
            {
                
                var itemPanel = Instantiate(_itemPanelPrefab);
                _itemPanels.Add(itemPanel);
                itemPanel.SetPanelInfo(item.Name);
                itemPanel.transform.SetParent(_inventoryPanel.transform);
                var rectTransform = itemPanel.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(_leftOffset, _topOffset - count * _panelsOffset - rectTransform.rect.height);
                count++;
            }
        }
    }
}
