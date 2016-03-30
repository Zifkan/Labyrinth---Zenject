using System.Collections.Generic;
using Assets.Scripts.Character.Inventory;
using Assets.Scripts.EventHandlers;
using Zenject;

namespace Assets.Scripts.GameInterface
{
    public class GameInterfacePresenter : IInitializable
    {
        private GameController _gameController;

        private InterfaceView _interfaceView;

        public GameInterfacePresenter(InterfaceView interfaceView, GameController gameController)
        {
            _interfaceView = interfaceView;
            _gameController = gameController;
        }

        public void ShowInventory(List<InventoryItem> items)
        {
            _interfaceView.RefreshInventory(items);
        }

        private void OnMenuItemClicked(object sender, MenuButtonEventArgs e)
        {
            switch (e.Items)
            {
                case MainMenuItems.NewGame:
                    _gameController.StartGame(0);
                    break;
                case MainMenuItems.LoadLevel:
                    _interfaceView.SetLevelPanel(_gameController.LvlList);
                    break;
                case MainMenuItems.Exit:
                    _gameController.ExitGame();
                    break;
            }
        }

        public void Initialize()
        {
            _interfaceView.MenuButtonClick += OnMenuItemClicked;
            _interfaceView.LevelButtonClick += OnLevelButtonClick;
            _interfaceView.RestartLevelEvent += OnRestartLevelEvent;
            _interfaceView.ReturnMainMenuEvent += OnReturnMainMenuEvent;
        }

        private void OnReturnMainMenuEvent(object sender, System.EventArgs e)
        {
            _gameController.DestroyLevel();
        }

        private void OnRestartLevelEvent(object sender, System.EventArgs e)
        {
            _gameController.RestartGame();
        }

        private void OnLevelButtonClick(object sender, LevelButtonEventArgs e)
        {
            _gameController.StartGame(e.LevelId);
        }
    }
}
