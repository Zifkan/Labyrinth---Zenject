using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scripts.ScriptableObjects
{
    public class PrefabScriptableObject : ScriptableObject
    {
        [SerializeField]
        private CellOption _cellPrefab;

        [SerializeField]
        private List<MonoBehaviour> _usableObjectPrefabs;

        [SerializeField]
        private List<MonoBehaviour> _decorateObjectPrefabs;

        [SerializeField]
        private GameObject _characterPrefab;

        public List<MonoBehaviour> UsableObjectPrefabs
        {
            get { return _usableObjectPrefabs; }
        }

        public CellOption CellPrefab
        {
            get { return _cellPrefab; }
        }

        public List<MonoBehaviour> DecorateObjectPrefabs
        {
            get { return _decorateObjectPrefabs; }
        }

        public GameObject CharacterPrefab
        {
            get { return _characterPrefab; }
        }


#if UNITY_EDITOR
        //[MenuItem("Assets/PrefabOptionsInfoList")]
        private static void CreateAsset()
        {
            var prefabOptionsInfoList = CreateInstance<PrefabScriptableObject>();

            AssetDatabase.CreateAsset(prefabOptionsInfoList, "Assets/PrefabOptionsInfoList.asset");
            AssetDatabase.SaveAssets();
        }
#endif
        
    }
}
