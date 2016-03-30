using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [Serializable]
    public sealed class CellConfig
    {
        [SerializeField] 
        private int _x;

        [SerializeField] 
        private int _y;

        [SerializeField] 
        private string _sceneObjectType;

        [SerializeField] 
        private string _sceneObjectName;

        [SerializeField] 
        private int _linkedItemColumn;

        [SerializeField] 
        private int _linkedItemRow;

        [SerializeField]
        private Quaternion _objRotation;

        [XmlElement("Row")]
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        [XmlElement("Column")]
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        [XmlElement("Type")]
        public String SceneObjectType
        {
            get { return _sceneObjectType; }
            set { _sceneObjectType = value; }
        }

        [XmlElement("Name")]
        public String SceneObjectName
        {
            get { return _sceneObjectName; }
            set { _sceneObjectName = value; }
        }

        [XmlElement("LinkedItemColumn")]
        public int LinkedItemColumn
        {
            get { return _linkedItemColumn; }
            set { _linkedItemColumn = value; }
        }

        [XmlElement("LinkedItemRow")]
        public int LinkedItemRow
        {
            get { return _linkedItemRow; }
            set { _linkedItemRow = value; }
        }

        [XmlElement("ObjectRotation")]
        public Quaternion ObjRotation
        {
            get { return _objRotation; }
            set { _objRotation = value; }
        }
    }
}
