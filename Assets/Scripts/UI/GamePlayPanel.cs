using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : MonoBehaviour
{
    public VisualElement rootElement;
    public Button ConfirmActionButton;

    void OnEnable()
    {
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        ConfirmActionButton = rootElement.Q<Button>("ConfirmButton");
        ConfirmActionButton.clicked+=()=>
        {
            TurnManager.Instance.TimeToMoveEvent.RaiseEvent(null,this);
        };
    }
}
