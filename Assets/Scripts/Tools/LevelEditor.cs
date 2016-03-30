using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts.DecorateObjects;
using Assets.Scripts.Extensions;
using Assets.Scripts.InteractiveObjects.Interfaces;
using Assets.Scripts.LevelSerialization;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Serialization;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class LevelEditor : EditorWindow 
    {
        private static int _cellsRow = 20;
        private static int _cellsColumn = 20;

        private static LevelCreator _levelCreator;

        private static PrefabCreator _prefabCreator;
        private static Type _gameObjectType;
        private static List<CellOption> _cellOptions;
        private static GameObject _objectsParent;
        private static GameObject _parentLevelObject;
        private static BuildType _buildType;

        private static Dictionary<string, Type> _usableObjectsDictionary = new Dictionary<string, Type>();
        private int _typeIndex;
        private int _prefabNameIndex;
        private int _indexLevelForLoad;
        private static int _serializationTypeIndex;
        private string _levelName = "DefaultName";
        private string _levelDescription = "";
        private static string _selectPrefabName;

        private static List<Type> _serializationTypes;

        private static ProjectConfig _projectConfig;
        private int _levelId;

        

        private LevelSerializer Levelserializer
        {
            get
            {
                return (LevelSerializer)Activator.CreateInstance(_serializationTypes[_serializationTypeIndex], new object[] { _projectConfig }); 
            }
        }

        public void InitEditor()
        {
            Init();
        }


        [MenuItem("Window/Level Editor")]
        static void Init()
        {
            LevelEditor window = (LevelEditor)GetWindow(typeof(LevelEditor));
            
            SceneView.onSceneGUIDelegate = EditorUpdate;
            
            var usablePrefabs = GameResourses.Instance.ScriptableObject.UsableObjectPrefabs;

            foreach (var prefab in usablePrefabs)
            {
                var component = prefab.GetComponent<IUsableObject>();
                if (!_usableObjectsDictionary.ContainsValue(component.GetType()))
                    _usableObjectsDictionary.Add(component.GetAttrDescription(), component.GetType());
            }

            _levelCreator = new LevelCreator();
            _serializationTypes = GetListOfType<LevelSerializer>().ToList();
            _projectConfig = AssetDatabase.LoadAssetAtPath<ProjectConfig>("Assets/ProjectConfig.asset");
            window.Show();
        }

        private void OnGUI()
        {
            DrawCellOptions();

            DrawBuildObjectsOptions();

            switch (_buildType)
            {
                case BuildType.DecorateObjects:
                    DecorationPrefabsPopup();
                    break;
                case BuildType.GameObjects:
                    UsablePrefabsPopup();
                    break;
            }


            DrawSerializationOptions();

            _levelId = EditorGUILayout.IntField("Level Id:", _levelId, GUILayout.ExpandWidth(true));
            _levelName = EditorGUILayout.TextField("Level Name:", _levelName, GUILayout.ExpandWidth(true));

            EditorGUILayout.LabelField("Level Description:");
            _levelDescription = EditorGUILayout.TextArea(_levelDescription, GUI.skin.GetStyle("HelpBox"));
           
            DrawSaveButton();
            DrawLoadContent();
        }

        private void OnDestroy()
        {
            //DestroyImmediate(GameObject.Find("Cells Parent"));
            DestroyImmediate(_objectsParent);
            DestroyImmediate(_parentLevelObject);
            _usableObjectsDictionary = new Dictionary<string, Type>();
        }

        private void DrawCellOptions()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Level Size");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _cellsRow = EditorGUILayout.IntField("Cells Row Count", _cellsRow, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            _cellsColumn = EditorGUILayout.IntField("Cells Column Count", _cellsColumn, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Create New Level", GUILayout.ExpandWidth(true)))
            {
                CreateLevel();
            }

            GUILayout.Space(14);
        }

        private void DrawBuildObjectsOptions()
        {
            EditorGUILayout.LabelField("Select type for building:");
            _buildType = (BuildType) EditorGUILayout.EnumPopup(_buildType, GUILayout.ExpandWidth(true));
            GUILayout.Space(10);
        }

        private void UsablePrefabsPopup()
        {
            string[] prefabTypes = _usableObjectsDictionary.Keys.ToArray();
            EditorGUILayout.LabelField("Select type of Interactive Object:");
            GUILayout.Space(2);
            _typeIndex = EditorGUILayout.Popup(_typeIndex, prefabTypes, GUILayout.ExpandWidth(true));
            _gameObjectType = _usableObjectsDictionary[prefabTypes[_typeIndex]];
            var listPrefabs = GameResourses.Instance.ScriptableObject.UsableObjectPrefabs.Where(behaviour => behaviour.GetType()==_gameObjectType).ToList();
            string[] names = listPrefabs.Select(prefab => prefab.GetComponent<SceneObject>().Name).ToArray();
            EditorGUILayout.LabelField("Select Name of Interactive Object:");
            GUILayout.Space(2);
            if (names.Count() > 1)
            {
                _prefabNameIndex = EditorGUILayout.Popup(_prefabNameIndex, names, GUILayout.ExpandWidth(true));
            }
            else
            {
                _prefabNameIndex = 0;
            }
            _selectPrefabName = names[_prefabNameIndex];
        }

        private void DrawSaveButton()
        {
            if (GUILayout.Button("Save level", GUILayout.ExpandWidth(true)))
            {
                Levelserializer.Save(new LevelConfig(_cellOptions, _levelName, _levelDescription, _levelId), _levelName);
            }
        }

        private void DrawLoadContent()
        {
            var lvlList = Levelserializer.GetLevelList();
            if (lvlList == null) return;

            var lvlNames = lvlList.Select(config => config.LevelName).ToArray();

            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Choose Level for load:");

                _indexLevelForLoad = EditorGUILayout.Popup(_indexLevelForLoad, lvlNames, GUILayout.ExpandWidth(true));
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Open level", GUILayout.ExpandWidth(true)))
            {
                LoadLevel(lvlNames);
            }
        }

        private void DrawSerializationOptions()
        {
            GUILayout.Space(25);
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Choose Serialization type:");
                _serializationTypeIndex = EditorGUILayout.Popup(_serializationTypeIndex, _serializationTypes.Select(type => type.Name).ToArray(), GUILayout.ExpandWidth(true));
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
        }

        private void DecorationPrefabsPopup()
        {
            var index = 0;
            var prefabs = GameResourses.Instance.ScriptableObject.DecorateObjectPrefabs;
            string[] names = prefabs.Select(prefab => prefab.GetComponent<DecorateObject>().Name).ToArray();
            index = EditorGUILayout.Popup(index, names, GUILayout.ExpandWidth(true));
            _gameObjectType = null;
            _selectPrefabName = names[index];
        }

        private static void EditorUpdate(SceneView sceneview)
        {
            Event e = Event.current;

            if (e.clickCount != 2) return;

            Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            var cell = hit.transform.GetComponent<CellOption>();

            if (cell == null || cell.LevelObject != null) return;
            
            var obj = _prefabCreator.CreateGameObjectPrefab(_cellOptions,cell.Y, cell.X, _selectPrefabName, _gameObjectType);

            obj.transform.parent = _objectsParent.transform;
            cell.LevelObject = obj;
        }

        private static void CreateLevel()
        {
            DataReset();
            _cellOptions = _levelCreator.CreateCells(_cellsColumn, _cellsRow, new LevelConfig());
            _prefabCreator = new PrefabCreator();
        }

        private static void DataReset()
        {
            DestroyImmediate(_parentLevelObject);
            if (_objectsParent != null) DestroyImmediate(_objectsParent);
            _objectsParent = new GameObject("Objects Parent");
            _cellOptions = new List<CellOption>();
        }

        private void LoadLevel(string[] lvlNames)
        {
            DataReset();
            var lvlConfig = Levelserializer.LoadByName(lvlNames[_indexLevelForLoad]);
            _cellOptions = _levelCreator.CreateLevel(lvlConfig, out _parentLevelObject);
            _levelId = lvlConfig.LevelId;
            _levelName = lvlConfig.LevelName;
            _levelDescription = lvlConfig.LevelDescription;
        }

        private static IEnumerable<Type> GetListOfType<T>() where T : class
        {
            return Assembly.GetAssembly(typeof (T)).GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof (T)));
        }
    }

    public enum BuildType
    {
        DecorateObjects,
        GameObjects,
    }
}
