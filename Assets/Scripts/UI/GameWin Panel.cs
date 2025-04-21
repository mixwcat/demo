using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class GameWinPanel : MonoBehaviour
{
    public VisualElement root;
    public Button BackToMenuButton;
    public Button PickPuzzleButton;
    public Button NextPuzzleButton;
    public VisualElement book;
    [Header("事件广播")]
    public ObjectEventSO LoadPickPuzzlePanelEvent;

    void OnEnable()
    {
        // MusicManager.Instance.ClearSoundList();
        // MusicManager.Instance.PlaySound("GameWinSound");
        root = GetComponent<UIDocument>().rootVisualElement;
        BackToMenuButton = root.Q<Button>("BackToMenuButton");
        PickPuzzleButton = root.Q<Button>("PickPuzzleButton");
        NextPuzzleButton = root.Q<Button>("NextPuzzleButton");
        book = root.Q<VisualElement>("Book");
        //注册按钮点击事件
        BackToMenuButton.clicked += OnBackToMenuButtonClick;
        PickPuzzleButton.clicked += OnPickPuzzleButtonClick;
        NextPuzzleButton.clicked += OnNextPuzzleButtonClick;

        NextPuzzleButton.style.display = DisplayStyle.Flex;

        if (PuzzleManager.Instance.currentPuzzleIndex >= 5)
        {
            //说明已经没有关卡了，隐藏下一关按钮
            NextPuzzleButton.style.display = DisplayStyle.None;
        }

        if (PuzzleManager.Instance.currentPuzzleIndex == 1)
        {
            Addressables.LoadAssetAsync<Texture2D>("book1").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    book.style.backgroundImage = handle.Result;
                }
            };
        }
        else if (PuzzleManager.Instance.currentPuzzleIndex == 2)
        {
            Addressables.LoadAssetAsync<Texture2D>("book2").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    book.style.backgroundImage = handle.Result;
                }
            };
        }
        else if (PuzzleManager.Instance.currentPuzzleIndex == 5)
        {
            Addressables.LoadAssetAsync<Texture2D>("book3").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    book.style.backgroundImage = handle.Result;
                }
            };
        }
        else
        {
            book.style.backgroundImage = null;
        }
    }

    private void OnNextPuzzleButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        //加载下一关卡
        PuzzleManager.Instance.currentPuzzleIndex++;
        SceneLoadManager.Instance.LoadPuzzle();
    }

    private void OnPickPuzzleButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        LoadPickPuzzlePanelEvent.RaiseEvent(null,this);
    }

    private void OnBackToMenuButtonClick()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        SceneLoadManager.Instance.loadMenu();
    }

}
