
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayPanel : MonoBehaviour
{

    public VisualElement root;


    public Button TryAgainButton;
    public Button SettingButton;
    public Button SwitchCharacterButton;
    public Button BackButton;

    public void OnEnable()
    {
        root= GetComponent<UIDocument>().rootVisualElement;
        
        
        TryAgainButton= root.Q<Button>("TryAgainButton");
        SettingButton= root.Q<Button>("SettingButton");
        SwitchCharacterButton= root.Q<Button>("SwitchCharacterButton");
        BackButton= root.Q<Button>("BackButton");
        
        TryAgainButton.clicked += OnTryAgainButtonClicked;
        SettingButton.clicked += OnSettingButtonClicked;
        
        SwitchCharacterButton.clicked += OnSwitchCharacterButtonClicked;
        BackButton.clicked += OnBackButtonClicked;

    }

    private void FixedUpdate()
    {
        if (!InputManager.Instance.canInput)
        {
            SwitchCharacterButton.SetEnabled(false);
            BackButton.SetEnabled(false);
        }
        else
        {
            SwitchCharacterButton.SetEnabled(true);
            BackButton.SetEnabled(true);
        }
    }

    private void OnSwitchCharacterButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        if (PlayerManager.Instance.CurrentPlayerIndex < PlayerManager.Instance.playerCount - 1)
            PlayerManager.Instance.SwitchCharacter(PlayerManager.Instance.CurrentPlayerIndex + 1);

        else PlayerManager.Instance.SwitchCharacter(0);
    }

 
    private void OnSettingButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        UIManager.Instance.OnTimeOut();
    }

    private void OnTryAgainButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        gameObject.SetActive(false);
        SceneLoadManager.Instance.LoadPuzzle();
    }

    private void OnBackButtonClicked()
    { 
        MusicManager.Instance.PlaySound("Click"); 
        PlayerManager.Instance.DownloadPlayerStepData();    
    }

}
