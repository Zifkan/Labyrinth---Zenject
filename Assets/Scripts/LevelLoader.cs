using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Serialization;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class LevelCreator //: MonoBehaviour
    {
        private readonly CellGrid _cellGrid = new CellGrid();

        private GameObject _cellsParent;
        private GameObject _objParent;

        private PrefabCreator _prefabCreator;
        private List<CellOption> _cellOptions;

        public List<CellOption> CreateLevel(LevelConfig levelConfig, out GameObject parentLevelObject)
        {
            _cellOptions = CreateCells(levelConfig.Rows, levelConfig.Columns, levelConfig);
            var cellOptions = CreateGameObjects(_cellOptions, levelConfig);
            parentLevelObject = new GameObject("Parent level object");
            _objParent.transform.parent = parentLevelObject.transform;
            _cellsParent.transform.parent = parentLevelObject.transform;
            return _cellOptions = cellOptions;
        }

        public List<CellOption> CreateCells(int cellsRow, int cellsColumn,LevelConfig levelConfig)
        {
            if (_cellsParent != null) UnityEngine.Object.DestroyImmediate(_cellsParent);
            _cellsParent = new GameObject("Cells Parent");
            return _cellGrid.OnCreateCells(_cellsParent, cellsRow, cellsColumn, levelConfig.CellConfigs);
        }

        private List<CellOption> CreateGameObjects(List<CellOption> cellOptions, LevelConfig levelConfig)
        {
            _prefabCreator=new PrefabCreator();
            if (_objParent != null) UnityEngine.Object.DestroyImmediate(_objParent);
            _objParent = new GameObject("Objects Parent");
            foreach (var cellConfig in levelConfig.CellConfigs)
            {
                var cell = cellOptions.First(option => option.X == cellConfig.X && option.Y == cellConfig.Y);
                cell.X = cellConfig.X;
                cell.Y = cellConfig.Y;
                cell.ObjRotation = cellConfig.ObjRotation;
                if (cellConfig.SceneObjectType != null)
                {
                    var obj=_prefabCreator.CreateGameObjectPrefab(_cellOptions, cellConfig.Y, cellConfig.X, cellConfig.SceneObjectName, Type.GetType("Assets.Scripts.InteractiveObjects."+ cellConfig.SceneObjectType));
                    obj.transform.parent = _objParent.transform;
                    cell.LevelObject = obj;
                    
                }
            }
            return cellOptions;
        }
    }
}
