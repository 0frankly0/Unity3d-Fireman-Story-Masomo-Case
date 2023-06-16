using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PanelManager : MonoBehaviour
{
    // oyun içi panellerin yönetimini saðlayan kod dosyasýdýr


    public GameObject _pausePanel,_gamePanel,_mainMenuPanel, _settingsPanel;
    void Start()
    {
        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(false); 
        _gamePanel.SetActive(true); 
        _mainMenuPanel.SetActive(true); 
    }

    public void PausePanelOn()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void PausePanelOff()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void SettingsPanelOn()
    {
        _settingsPanel.SetActive(true);
    }

    public void SettingsPanelOff()
    {
        _settingsPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
