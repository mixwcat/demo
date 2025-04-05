using UnityEngine;
using UnityEngine.UIElements;

public class MenuPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button StartGameButton,quitGameButton;
    
    public ObjectEventSO StartGameEvent;

    private void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        StartGameButton = rootElement.Q<Button>("StartButton");
        quitGameButton = rootElement.Q<Button>("ExitButton");

        StartGameButton.clicked += OnNewGameButtonClicked;
        quitGameButton.clicked += OnQuitGameButtonClicked;
    }

    private void OnQuitGameButtonClicked()
    {
        Application.Quit();
    }
    
    private void OnNewGameButtonClicked()
    {
        StartGameEvent.RaiseEvent(null,this);
    }
}
