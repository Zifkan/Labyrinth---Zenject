using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.LevelSerialization
{
    public sealed class ScriptableObjectLevelSerializer : LevelSerializer
    {
        public ScriptableObjectLevelSerializer(ProjectConfig projectConfig): base(projectConfig)
        {
        }
        
        public override void Save(LevelConfig levelConfig, string lvlName)
        {
            var lvlSerializer = ProjectConfig.LevelScriptableObject;
            lvlSerializer.AddLevel(levelConfig);
        }

        public override LevelConfig LoadByPath(string path)
        {
            throw new System.NotImplementedException();
        }

        public override LevelConfig LoadByName(string lvlName)
        {
            return ProjectConfig.LevelScriptableObject.LevelConfigs.First(config => config.LevelName.Equals(lvlName));
        }

        public override List<LevelConfig> GetLevelList()
        {
            return ProjectConfig.LevelScriptableObject.LevelConfigs;
        }

        
    }
}
