using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameInterface
{
    public class ItemPanelScript : MonoBehaviour
    {
        [SerializeField]
        private Image _levelIcon;

        [SerializeField]
        private Text _itemDescription;

        public void SetPanelInfo(string itemDescription)
        {
            _itemDescription.text = itemDescription;
        }
    }
}
