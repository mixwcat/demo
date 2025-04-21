using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button startGameButton,quitGameButton,bookButton;
    
    [Header("事件广播")]
    public ObjectEventSO StartGameEvent;
    public ObjectEventSO LoadPickPuzzlePanelEvent;
    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        startGameButton = rootElement.Q<Button>("StartButton");
        quitGameButton = rootElement.Q<Button>("ExitButton");
        bookButton = rootElement.Q<Button>("BookButton");

        startGameButton.clicked += OnNewGameButtonClicked;
        quitGameButton.clicked += OnQuitGameButtonClicked;
        bookButton.clicked += OnBookButtonClicked;
    }

    private void OnBookButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        UIManager.Instance.OnBook();
    }

    private void OnQuitGameButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        Application.Quit();
    }
    
    private void OnNewGameButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        //测试
        LoadPickPuzzlePanelEvent.RaiseEvent(null,this);
        //StartGameEvent.RaiseEvent(null,this);
    }
}
