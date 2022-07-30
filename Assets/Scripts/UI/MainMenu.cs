using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _howToPlayButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _educationPanel;

    private void OnEnable()
    {
        Time.timeScale = 0;
        _playButton.onClick.AddListener(OnClickPlayButton);
        _howToPlayButton.onClick.AddListener(OnClickHowToPlayButton);
        _exitButton.onClick.AddListener(OnClickOnExitButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        _playButton.onClick.RemoveListener(OnClickPlayButton);
        _howToPlayButton.onClick.RemoveListener(OnClickHowToPlayButton);
        _exitButton.onClick.RemoveListener(OnClickOnExitButton);
    }

    private void OnClickPlayButton()
    {
        gameObject.SetActive(false);
    }

    private void OnClickHowToPlayButton()
    {
        _educationPanel.SetActive(true);
    }

    private void OnClickOnExitButton()
    {
        Application.Quit();
    }
}
