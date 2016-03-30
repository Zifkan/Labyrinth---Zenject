using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Serialization;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class LevelScriptableObject : ScriptableObject
    {
        [SerializeField] 
        private List<LevelConfig> _levelConfigs;

        public List<LevelConfig> LevelConfigs
        {
            get { return _levelConfigs; }
        }

        public void AddLevel(LevelConfig levelConfig)
        {
            Debug.Log(levelConfig.LevelName);
            if (_levelConfigs.Count == 0)
            {
                _levelConfigs.Add(new LevelConfig
                {
                    CellConfigs = levelConfig.CellConfigs,
                    Columns = levelConfig.Columns,
                    Rows = levelConfig.Rows,
                    LevelName = levelConfig.LevelName,
                });
                return;
            }
            Debug.Log(_levelConfigs[0].LevelName);
            var level = _levelConfigs.FirstOrDefault(config => config.LevelName.Equals(levelConfig.LevelName));
            if (level == null)
                _levelConfigs.Add(new LevelConfig
                {
                    CellConfigs = levelConfig.CellConfigs,
                    Columns = levelConfig.Columns,
                    Rows = levelConfig.Rows,
                    LevelName = levelConfig.LevelName,
                });
            else
            {
                level.CellConfigs = levelConfig.CellConfigs;
                level.Columns = levelConfig.Columns;
                level.Rows = levelConfig.Rows;
                level.LevelName = levelConfig.LevelName;
            }
        }

#if UNITY_EDITOR
        private void CreateAsset(string path)
        {
            var scriptableObjectLevel = CreateInstance<LevelScriptableObject>();
            AssetDatabase.CreateAsset(scriptableObjectLevel, "Assets/LevelScriptableObject.asset");
            AssetDatabase.SaveAssets();
        }

#endif
    }
}
