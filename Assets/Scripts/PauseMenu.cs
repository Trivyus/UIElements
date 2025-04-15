using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private SceneNames _sceneNames;
    [SerializeField] private GameObject _gameInterface;
    [SerializeField] private GameObject _pauseMenuPanel;

    public void PauseGame()
    {
        Time.timeScale = 0f;

        _pauseMenuPanel.SetActive(true);
        _gameInterface.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;

        _pauseMenuPanel.SetActive(false);
        _gameInterface.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_sceneNames.ToString());
    }
}