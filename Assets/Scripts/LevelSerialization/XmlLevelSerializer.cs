using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Serialization;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.LevelSerialization
{
    public sealed class XmlLevelSerializer : LevelSerializer
    {
        public XmlLevelSerializer(ProjectConfig projectConfig): base(projectConfig)
        {
            
        }

        public override void Save(LevelConfig levelConfig, string lvlName)
        {
            Debug.Log(Path.GetFullPath(Application.dataPath + ProjectConfig.XmlSaveLevelPath + lvlName + ".xml"));
            Serialize(levelConfig, Path.GetFullPath(Application.dataPath + ProjectConfig.XmlSaveLevelPath + lvlName + ".xml"));
        }

        public override LevelConfig LoadByPath(string path)
        {
            return Deserialize(path);
        }

        public override LevelConfig LoadByName(string lvlName)
        {
            return Deserialize(Path.GetFullPath(Application.dataPath + ProjectConfig.XmlSaveLevelPath + lvlName + ".xml"));
        }

        public override List<LevelConfig> GetLevelList()
        {
            string[] fullfilesPath = Directory.GetFiles(Path.GetFullPath(Application.dataPath + ProjectConfig.XmlSaveLevelPath), "*.xml");
            var levelConfigs = new List<LevelConfig>();
            foreach (var path in fullfilesPath)
            {
                levelConfigs.Add(LoadByPath(path));
            }
            return levelConfigs;
        }

        private void Serialize(LevelConfig levelConfig, String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LevelConfig));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, levelConfig);
            }
            Debug.Log("Level Saved");
        }

        private LevelConfig Deserialize(String path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LevelConfig));
            var stream = new FileStream(path, FileMode.Open);
            LevelConfig container = serializer.Deserialize(stream) as LevelConfig;
            stream.Close();
            return container;
        }
    }
}
