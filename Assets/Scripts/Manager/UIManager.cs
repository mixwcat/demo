using Unity.VisualScripting;
using UnityEngine;

public class UIManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<UIManager>
{
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;
    public GameObject pickPuzzlePanel;
    public GameObject gameplayPanel;
    public GameObject timeOutPanel;
    public GameObject bookPanel;
    public void OnGameOver()
    {
        HideAllPanel();
        gameOverPanel.SetActive(true);
        InputManager.Instance.gameIsRunning = false;
    }
    public void LoadPickPuzzlePanel()
    {
        HideAllPanel();
        pickPuzzlePanel.SetActive(true);
    }

    public void OnBook()
    {
        bookPanel.SetActive(true);
        InputManager.Instance.canInput = false;
    }
    public void OnTimeOut()
    {
        HideAllPanel();
        timeOutPanel.SetActive(true);
        InputManager.Instance.gameIsRunning = false;
    }
    public void OnGameWin()
    {
        HideAllPanel();
        gameWinPanel.SetActive(true);
        InputManager.Instance.gameIsRunning = false;
    }

    public void AfterPuzzleLoad()
    {
        HideAllPanel();
        gameplayPanel.SetActive(true);
    }

    public void HideAllPanel()
    {
        //待添加
        gameOverPanel.SetActive(false);
        pickPuzzlePanel.SetActive(false);
        gameWinPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        timeOutPanel.SetActive(false);
        bookPanel.SetActive(false);
    }
}
