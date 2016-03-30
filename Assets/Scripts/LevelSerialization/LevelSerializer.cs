using System.Collections.Generic;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Serialization;

namespace Assets.Scripts.LevelSerialization
{
    public abstract class LevelSerializer
    {
        protected readonly ProjectConfig ProjectConfig;

        protected LevelSerializer(ProjectConfig projectConfig)
        {
            ProjectConfig = projectConfig;
        }

        public abstract void Save(LevelConfig levelConfig, string levelName);

        public abstract LevelConfig LoadByPath(string path);
        public abstract LevelConfig LoadByName(string lvlName);

        public abstract List<LevelConfig> GetLevelList();
    }
}
