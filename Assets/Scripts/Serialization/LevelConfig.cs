using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts.InteractiveObjects;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public class LevelConfig
    {
        [SerializeField]
        private int _levelId;

        [SerializeField] 
        private string _levelName;

        [SerializeField]
        private string _levelDescription;

        [SerializeField] 
        private List<CellConfig> _cellConfigs;

        [SerializeField] 
        private int _columns;

        [SerializeField] 
        private int _rows;

        [XmlElement("LevelName")]
        public string LevelName
        {
            get { return _levelName; }
            set { _levelName = value; }
        }

        [XmlElement("CellConfigs")]
        public List<CellConfig> CellConfigs
        {
            get { return _cellConfigs; }
            set { _cellConfigs = value; }
        }

        [XmlElement("Columns")]
        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        [XmlElement("Rows")]
        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        [XmlElement("LevelDescription")]
        public string LevelDescription
        {
            get { return _levelDescription; }
            set { _levelDescription = value; }
        }

        [XmlElement("LevelId")]
        public int LevelId
        {
            get { return _levelId; }
            set { _levelId = value; }
        }

        public LevelConfig()
        {
        }

        public LevelConfig(List<CellOption> cellOptions,string lvlName, string lvlDescription, int levelId)
        {
            LevelId = levelId;
            LevelName = lvlName;
            LevelDescription = lvlDescription;
            Rows = cellOptions.Max(option => option.Y)+1;
            Columns = cellOptions.Max(option => option.X)+1;
            CellConfigs = new List<CellConfig>();
            foreach (var cellOption in cellOptions)
            {
                CellConfigs.Add(new CellConfig
                {
                    X = cellOption.X,
                    Y = cellOption.Y,
                    SceneObjectType = cellOption.LevelObject == null ? null : cellOption.LevelObject.GetComponent<SceneObject>().GetType().Name,
                    SceneObjectName = cellOption.LevelObject == null ? null : cellOption.LevelObject.GetComponent<SceneObject>().Name,
                    LinkedItemColumn = cellOption.LinkedItemX,//cellOption.LevelObject == null || cellOption.LevelObject.GetComponent<IUsableObject>() == null ? (int?)null : cellOption.LevelObject.GetComponent<IUsableObject>().LinkedItemX,
                    LinkedItemRow = cellOption.LinkedItemY, //cellOption.LevelObject == null || cellOption.LevelObject.GetComponent<IUsableObject>() == null ? (int?)null : cellOption.LevelObject.GetComponent<IUsableObject>().LinkedItemY,
                    ObjRotation = cellOption.LevelObject == null ? new Quaternion(0,0,0,0): cellOption.LevelObject.transform.rotation,
                });
            }
        }
    }
}
