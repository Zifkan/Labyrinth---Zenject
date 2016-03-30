using Assets.Scripts.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class CellOption : MonoBehaviour
    {
        [SerializeField]
        private int _linkedItemX;

        [SerializeField]
        private int _linkedItemY;

        public int X { get; set; }
        public int Y { get; set; }
        public int LinkedItemX
        {
            get { return _linkedItemX; }
            set { _linkedItemX = value; }
        }
       
        public int LinkedItemY
        {
            get { return _linkedItemY; }
            set { _linkedItemY = value; }
        }
        public SceneObject LevelObject { get; set; }
       
        public Quaternion ObjRotation { get; set; }
    }
}
