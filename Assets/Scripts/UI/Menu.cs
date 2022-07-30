using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private List<PlayerControler> _controlers;
    [SerializeField] private List<GameObject> _menuPanels;
    [SerializeField] private GameObject _mainMenuPanel;

    private void OnEnable()
    {
        for(int i = 0; i < _controlers.Count; i++)
        {
            _controlers[i].MenuOpened += OpenMenu;
            _controlers[i].MainMenuOpened += OpenMainMenu;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _controlers.Count; i++)
        {
            _controlers[i].MenuOpened -= OpenMenu;
            _controlers[i].MainMenuOpened -= OpenMainMenu;
        }
    }

    public void CloseMenu()
    {
        for (int i = 0; i < _menuPanels.Count; i++)
        {
            _menuPanels[i].SetActive(false);
        }

        Time.timeScale = 1;
    }

    private void OpenMenu()
    {
        Time.timeScale = 0;

        for(int i = 0; i < _menuPanels.Count; i++)
        {
            _menuPanels[i].SetActive(true);
        }
    }

    public void OpenMainMenu()
    {
        _mainMenuPanel.SetActive(true);
    }
}
