
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverPanel : MonoBehaviour
{
    public VisualElement root;
    public Button TryAgainButton;
    public Button BackToMenuButton;


    void OnEnable()
    {
        MusicManager.Instance.ClearSoundList();
        MusicManager.Instance.PlaySound("GameOverSound");
        root = GetComponent<UIDocument>().rootVisualElement;
        TryAgainButton = root.Q<Button>("TryAgainButton");
        BackToMenuButton = root.Q<Button>("BackToMenuButton");
        //注册按钮点击事件
        TryAgainButton.clicked+= OnTryAgainButtonClick;
        BackToMenuButton.clicked+= OnBackToMenuButtonClick;
    }

    private void OnBackToMenuButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        SceneLoadManager.Instance.loadMenu();
    }

    private void OnTryAgainButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        SceneLoadManager.Instance.LoadPuzzle();
    }
}
