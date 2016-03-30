using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.DecorateObjects;
using Assets.Scripts.EventDispatcher;
using Assets.Scripts.InteractiveObjects;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    public class PrefabCreator
    {
        private readonly ObjectEventDispatcher _objectEventDispatcher;
      
        public PrefabCreator()
        {
            _objectEventDispatcher = new ObjectEventDispatcher();
        }

        public SceneObject CreateGameObjectPrefab(List<CellOption> cellOptions, int row, int column, string objectName, Type type = null)
        {
            MonoBehaviour prefab;
            if (type != null && type != typeof(DecorateObject))
            {
                var prefabs = GameResourses.Instance.GetUsableObjectPrefab(type);
                prefab = prefabs.First(objPrefab => objPrefab.name.Equals(objectName));
                
            }
            else
            {
                prefab = GameResourses.Instance.GetDecoratePrefab(objectName);
            }
            var obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(true);
            var cell = cellOptions.First(option => option.X == column && option.Y == row);
            obj.transform.position = cell.transform.position;
            obj.gameObject.name = objectName;
            obj.GetComponent<SceneObject>().ParentX = cell.X;
            obj.GetComponent<SceneObject>().ParentY = cell.Y;
            obj.transform.rotation = cell.ObjRotation;

            var distanceUsableItem = obj.GetComponent<IDistanceUsableItem>();
            if (distanceUsableItem != null)
            {
                distanceUsableItem.LinkedItemX = cell.LinkedItemX;
                distanceUsableItem.LinkedItemY = cell.LinkedItemY;
                distanceUsableItem.ObjectEventDispatcher = _objectEventDispatcher;
            }

            var pickableItem = obj.GetComponent<IPickableItem>();
            if (pickableItem != null)
            {
                pickableItem.LinkedItemX = cell.LinkedItemX;
                pickableItem.LinkedItemY = cell.LinkedItemY;
            }
            return (SceneObject)obj;
        }
    }
}
