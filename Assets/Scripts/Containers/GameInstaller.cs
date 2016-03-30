using System;
using Assets.Scripts.Character;
using Assets.Scripts.Character.Inventory;
using Assets.Scripts.GameInterface;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Containers
{
    public class GameInstaller : MonoInstaller
    {
        public Settings GameSettings;
        
        public override void InstallBindings()
        {
            InstallSettings();
            
            Container.Bind<CharacterFactory>().ToSingle();
            Container.Bind<GameInventory>().ToSingle().WhenInjectedInto<GameCharacter>();
            Container.Bind<GameController>().ToSingle();
            Container.Bind<GameInterfacePresenter>().ToSingle();
            Container.Bind<IInitializable>().ToSingle<GameController>();
            Container.Bind<IInitializable>().ToSingle<GameInterfacePresenter>();
        }

        private void InstallSettings()
        {
            Container.BindInstance(GameSettings.GameCharacter).WhenInjectedInto<CharacterFactory>();
            Container.Bind<InterfaceView>().ToSingleInstance(GameSettings.InterfaceView);
            Container.Bind<ProjectConfig>().ToSingleInstance(GameSettings.ProjectConfig);
        }

        [Serializable]
        public class Settings
        {
            public ProjectConfig ProjectConfig;
            public InterfaceView InterfaceView;
            public GameObject GameCharacter;
        }
    }
}
