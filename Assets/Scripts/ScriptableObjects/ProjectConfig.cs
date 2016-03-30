using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    public class ProjectConfig : ScriptableObject
    {
        [SerializeField]
        private DataType _dataType;

        [SerializeField] 
        private string _xmlSaveLevelPath;

        [SerializeField] 
        private LevelScriptableObject _levelScriptableObject;

        public DataType DataType
        {
            get { return _dataType; }
        }

        public string XmlSaveLevelPath
        {
            get { return _xmlSaveLevelPath; }
        }

        public LevelScriptableObject LevelScriptableObject
        {
            get { return _levelScriptableObject; }
        }

#if UNITY_EDITOR
       // [MenuItem("Assets/ProjectConfig")]
        private static void CreateAsset()
        {
            var projectConfig = CreateInstance<ProjectConfig>();

            AssetDatabase.CreateAsset(projectConfig, "Assets/ProjectConfig.asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }


    public enum DataType
    {
        XmlSerialization,
        ScriptableObject,
    }
}
