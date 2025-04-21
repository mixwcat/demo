using UnityEngine;
using UnityEngine.UIElements;

public class TimeOutPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button ContinueButton,BackToMenuButton,PickPuzzleButton;
    
    [Header("事件广播")]
    public ObjectEventSO LoadPickPuzzlePanelEvent;
    
    private void OnEnable()
    {
        rootElement=GetComponent<UIDocument>().rootVisualElement;

        ContinueButton=rootElement.Q<Button>("ContinueButton");
        PickPuzzleButton=rootElement.Q<Button>("PickPuzzleButton");
        BackToMenuButton=rootElement.Q<Button>("BackToMenuButton");
        
        ContinueButton.clicked+=OnContinue;
        PickPuzzleButton.clicked+=OnPickPuzzleButtonClick;
        BackToMenuButton.clicked +=BackToMenu;

    }
    
    
    private void OnContinue()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        UIManager.Instance.gameplayPanel.SetActive(true);
        InputManager.Instance.gameIsRunning = true;
    }

    private void BackToMenu()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        SceneLoadManager.Instance.loadMenu();
    }
    
    private void OnPickPuzzleButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        LoadPickPuzzlePanelEvent.RaiseEvent(null,this);
    }
    
}
