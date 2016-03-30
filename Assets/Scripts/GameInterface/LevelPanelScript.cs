using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameInterface
{
    public class LevelPanelScript : MonoBehaviour
    {
        [SerializeField]
        private Image _levelIcon;

        [SerializeField]
        private Text _levelName;

        [SerializeField]
        private Text _levelDescription;

        private int _levelId;

        public int LevelId
        {
            get { return _levelId; }
        }

        public void SetPanelInfo(string lvlName, string lvlDescription, int lvlId)
        {
            _levelName.text = lvlName;
            _levelDescription.text = lvlDescription;
            _levelId = lvlId;
        }
    }
}
