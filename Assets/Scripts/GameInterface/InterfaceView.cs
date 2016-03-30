using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Character.Inventory;
using Assets.Scripts.EventHandlers;
using Assets.Scripts.InteractiveObjects;
using Assets.Scripts.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameInterface
{
    public class InterfaceView : MonoBehaviour, IGameInterface
    {
        public event EventHandler<MenuButtonEventArgs> MenuButtonClick;
        public event EventHandler<LevelButtonEventArgs> LevelButtonClick;
        public event EventHandler RestartLevelEvent;
        public event EventHandler ReturnMainMenuEvent;

        [SerializeField] 
        private GameObject _mainMenuPanel;

        [SerializeField]
        private GameObject _gameMenuPanel;

        [SerializeField] 
        private GameObject _mainOptionsPanel;

        [SerializeField] 
        private GameObject _dialogWindowPanel;

        [SerializeField]
        private LevelPanelScript _levelPanel;

        [SerializeField]
        private GameObject _scrollLevelsPanel;

        [SerializeField]
        private InventoryInterfaceScript _inventoryPanel;

        [SerializeField]
        private GameObject _levelsPanel;

        [SerializeField]
        private float _topOffset;

        [SerializeField]
        private float _leftOffset;

        [SerializeField]
        private float _panelsOffset;

        private Animator _animator;

        private void Start()
        {
            _animator = _mainMenuPanel.GetComponent<Animator>();
            _animator.SetBool("IsOpen", true);
        }

        public void OnStartGame()
        {
            _animator.SetBool("IsOpen", false);
            _mainMenuPanel.SetActive(false);
            OnMenuButtonClick(MainMenuItems.NewGame);
        }

        public void OnResumeGame()
        {
            _gameMenuPanel.SetActive(false);
        }

        public void OnLevelLoad()
        {
            _animator.SetBool("IsOpen", false);
            OnMenuButtonClick(MainMenuItems.LoadLevel);
        }

        public void OnRestartLevel()
        {
            OnRestartLevelEvent();
        }

        public void OnMainMenu()
        {
            _mainMenuPanel.SetActive(true);
            _animator.SetBool("IsOpen", true);
            OnReturnMainMenuEvent();
            _gameMenuPanel.SetActive(false);
        }

        public void RefreshInventory(List<InventoryItem> items)
        {
            _inventoryPanel.RefreshInventory(items);
        }

        public void OnExitGame()
        {
            OnMenuButtonClick(MainMenuItems.Exit);
        }

        public void DialogWindowFormation(string title)
        {
            _dialogWindowPanel.GetComponentInChildren<Text>().text = title;
        }

        public void SetLevelPanel(List<LevelConfig> lvlList)
        {
            _levelsPanel.SetActive(true);
            var scrollPanelRect = _scrollLevelsPanel.GetComponent<RectTransform>();

            float last = 0;

            int max = 100;

            for (int i = 0; i < max; i++)
            {
                var lvlPanel = Instantiate(_levelPanel);
                lvlPanel.SetPanelInfo(i.ToString(), i.ToString(), i/*lvlList[i].LevelName, lvlList[i].LevelDescription, lvlList[i].LevelId*/);
                lvlPanel.transform.SetParent(_scrollLevelsPanel.transform);
                lvlPanel.GetComponent<Button>().onClick.AddListener(() => OnLoadLevel(lvlPanel));
                var rectTransform = lvlPanel.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(_leftOffset, _topOffset + i * (_panelsOffset - rectTransform.rect.height));
                last = rectTransform.anchoredPosition.y;
            }

            float scrollHeight = last + scrollPanelRect.rect.height;
            scrollPanelRect.offsetMax = new Vector2(scrollPanelRect.offsetMax.x, scrollPanelRect.offsetMax.y);
            scrollPanelRect.offsetMin = new Vector2(scrollPanelRect.offsetMin.x, scrollHeight);
        }

        private void OnLoadLevel(LevelPanelScript levelPanelScript)
        {
            OnLevelButtonClick(levelPanelScript.LevelId);
            _levelsPanel.SetActive(false);
        }

        private void OnMenuButtonClick(MainMenuItems mainMenuItem)
        {
            if (MenuButtonClick != null)
                MenuButtonClick(this, new MenuButtonEventArgs(mainMenuItem));
        }

        private void OnLevelButtonClick(int lvlId)
        {
            if(LevelButtonClick != null)
                LevelButtonClick(this, new LevelButtonEventArgs(lvlId));
        }

        private void OnRestartLevelEvent()
        {
            if (RestartLevelEvent != null)
                RestartLevelEvent(this, EventArgs.Empty);
        }

        private void OnReturnMainMenuEvent()
        {
            if (ReturnMainMenuEvent != null)
                ReturnMainMenuEvent(this, EventArgs.Empty);
        }
    }
}
