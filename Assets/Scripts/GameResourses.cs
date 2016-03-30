using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DecorateObjects;
using Assets.Scripts.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class GameResourses
    {
        private static GameResourses _instance;
        private readonly PrefabScriptableObject _prefabScriptableObject;

        private GameResourses()
        {
            _prefabScriptableObject = AssetDatabase.LoadAssetAtPath<PrefabScriptableObject>("Assets/PrefabOptionsInfoList.asset");
        }

        public static GameResourses Instance
        {
            get { return _instance ?? (_instance = new GameResourses()); }
        }

        public PrefabScriptableObject ScriptableObject
        {
            get { return _prefabScriptableObject; }
        }

        public CellOption GetCellPrefab()
        {
            return ScriptableObject.CellPrefab;
        }
        public MonoBehaviour GetDecoratePrefab(string objectName)
        {
            return ScriptableObject.DecorateObjectPrefabs.First(prefab => prefab.GetComponent<DecorateObject>().Name.Equals(objectName) );
        }

        public List<MonoBehaviour> GetUsableObjectPrefab(Type prefabType)
        {
            return ScriptableObject.UsableObjectPrefabs.Where(prefab => prefab.GetType()==prefabType).ToList();
        }
    }
}
