using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PickPuzzlePanel : MonoBehaviour
{
    public VisualElement root;
    public Button PickPuzzle1Button;
    public Button PickPuzzle2Button;
    public Button PickPuzzle3Button;
    
    public Button PickStudy1Button;
    public Button PickStudy2Button;
    //
    
    public Button BackToMenuButton;
    [Header("事件广播")]
    public ObjectEventSO LoadPuzzle1Event;
    public ObjectEventSO LoadPuzzle2Event;
    public ObjectEventSO LoadPuzzle3Event;
    
    public ObjectEventSO LoadStudy1Event;
    public ObjectEventSO LoadStudy2Event;
    //
    
    void OnEnable()
    {
        root=GetComponent<UIDocument>().rootVisualElement;
        BackToMenuButton=root.Q<Button>("BackToMenuButton");
        BackToMenuButton.clicked +=OnBackButtonClicked;
        
        PickPuzzle1Button=root.Q<Button>("PickPuzzle1Button");
        PickPuzzle2Button=root.Q<Button>("PickPuzzle2Button");
        PickPuzzle3Button=root.Q<Button>("PickPuzzle3Button");
        
        PickStudy1Button=root.Q<Button>("PickStudy1Button");
        PickStudy2Button=root.Q<Button>("PickStudy2Button");
        //
        
        PickPuzzle1Button.clicked+=LoadPuzzle1;
        PickPuzzle2Button.clicked+=LoadPuzzle2;
        PickPuzzle3Button.clicked+=LoadPuzzle3;

        PickStudy1Button.clicked += LoadStudy1;
        PickStudy2Button.clicked += LoadStudy2;
        //
    }

    private void LoadStudy2()
    {
        MusicManager.Instance.PlaySound("Click");
        PuzzleManager.Instance.currentPuzzleIndex=2;
        LoadPuzzle1Event.RaiseEvent(null,this);
    }

    private void LoadStudy1()
    {
        MusicManager.Instance.PlaySound("Click");
        PuzzleManager.Instance.currentPuzzleIndex=1;
        LoadPuzzle1Event.RaiseEvent(null,this);
    }

    private void OnBackButtonClicked()
    {
        MusicManager.Instance.PlaySound("Click");
        if(SceneLoadManager.Instance.CurrentScene==SceneLoadManager.Instance.menu)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(false);
            SceneLoadManager.Instance.loadMenu();
        } 
    }

    public void LoadPuzzle1()
    {
        MusicManager.Instance.PlaySound("Click");
        PuzzleManager.Instance.currentPuzzleIndex=3;
        LoadPuzzle1Event.RaiseEvent(null,this);
    }
    public void LoadPuzzle2()
    {
        MusicManager.Instance.PlaySound("Click");
        PuzzleManager.Instance.currentPuzzleIndex = 4;
        LoadPuzzle2Event.RaiseEvent(null,this);
    }
    public void LoadPuzzle3()
    {
        MusicManager.Instance.PlaySound("Click");
        PuzzleManager.Instance.currentPuzzleIndex=5;
        LoadPuzzle3Event.RaiseEvent(null,this);
    }
    public void AfterPuzzleLoadEvent()
    {
        gameObject.SetActive(false);
    }
}
