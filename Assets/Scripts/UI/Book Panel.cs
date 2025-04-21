using UnityEngine;
using UnityEngine.UIElements;

public class BookPanel : MonoBehaviour
{
    private VisualElement rootElement;
    private Button BackButton;
    
    
    private void OnEnable()
    {
        rootElement=GetComponent<UIDocument>().rootVisualElement;

        BackButton=rootElement.Q<Button>("BackButton");

        BackButton.clicked += OnBackButtonClicked;
    }

    private void OnBackButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        InputManager.Instance.canInput = true;
    }
}
