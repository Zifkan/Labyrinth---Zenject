using UnityEngine;

namespace Assets.Scripts.Serialization
{
    public abstract class SceneObject : MonoBehaviour
    {
        public int ParentX { get; set; }
        public int ParentY { get; set; }

        public string Name
        {
            get { return name; }
        }

        public bool CanMove { get; protected set; }

        public bool IsUsable { get; protected set; }
    }
}
