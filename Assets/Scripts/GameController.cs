using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Character;
using Assets.Scripts.LevelSerialization;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Serialization;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class GameController : IInitializable
    {
        [Inject]
        private ProjectConfig _projectConfig;

        private CellOption[,] _cellOptions;
        private LevelCreator _levelCreator;
        private LevelSerializer _levelserializer;
        private GameCharacter _gameCharacter;

        private GameObject _parentLevelObject;

        private int _currentLvlId;

        readonly CharacterFactory _characterFactory;
         

        public List<LevelConfig> LvlList { get; private set; }

        private CellOption[,] CellOptions
        {
            get { return _cellOptions; }
        }

        public void StartGame(int lvlId)
        {
            CreateLevel(lvlId);
            CharacterInit();
            Camera.main.transform.position = _gameCharacter.transform.position + new Vector3(0, 10f, 0);
            _currentLvlId = lvlId;
        }

        public void RestartGame()
        {
            DestroyLevel();
            StartGame(_currentLvlId);
        }

        public void DestroyLevel()
        {
            Object.Destroy(_parentLevelObject.gameObject);
            Object.Destroy(_gameCharacter.gameObject);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public GameController(CharacterFactory characterFactory)
        {
            _characterFactory = characterFactory;
        }

        public void Initialize()
        {
            LevelSerializationInit();
        }


        private void LevelSerializationInit()
        {
            _levelCreator = new LevelCreator();
            _levelserializer = new XmlLevelSerializer(_projectConfig);
            LvlList = _levelserializer.GetLevelList();
        }

        private void CharacterInit()
        {
            _gameCharacter = _characterFactory.CreatePlayer();
            _gameCharacter.gameObject.SetActive(true);
            _gameCharacter.Initialize(CellOptions, 2, 2, CharacterDirection.Up);
        }

        private void CreateLevel(int lvlId)
        {
            var level = LvlList.First(config => config.LevelId == lvlId);
            _cellOptions = new CellOption[level.Columns, level.Rows];

            var cellOptions = _levelCreator.CreateLevel(level, out _parentLevelObject);
            for (int i = 0; i < level.Columns; i++)
            {
                for (int j = 0; j < level.Rows; j++)
                {
                    CellOptions[i, j] = cellOptions.First(option => option.X == i && option.Y == j);
                }
            }
        }
    }
}
