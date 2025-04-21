 using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<SceneLoadManager>
{
     public FadePanel fadePanel;
    //AssetReference通用资源标志类
    private AssetReference currentScene;
    public AssetReference menu;

    [Header(header:"关卡")]
    public AssetReference puzzle1;
    public AssetReference puzzle2;
    public AssetReference puzzle3;
    public AssetReference study1;
    public AssetReference study2;
    
    public AssetReference intro;
    [Header(header:"广播")]
    public ObjectEventSO afterPuzzleLoadEvent;

    public AssetReference CurrentScene{get{return currentScene;}}
    
    private void Awake()
    {
        loadMenu();
    }
    
    
    public async void loadMenu()
    {
        if(currentScene!=null) await UnloadSceneTask();
        
        if(!MusicManager.Instance.HaveBKM()) MusicManager.Instance.PlayBKMusic("BKM1");
        MusicManager.Instance.ClearSoundList();
        PuzzleManager.Instance.currentPuzzleIndex = 0;
        currentScene = menu;
        await LoadSceneTask();
    }
    public  void LoadPuzzle()
    {
        int index = PuzzleManager.Instance.currentPuzzleIndex;
        if (index == 1)
        {
            LoadStudy1();
        }
        else if (index == 2)
        {
            LoadStudy2();
        }
        else if (index == 3)
        {
            LoadPuzzle1();
        }
        else if (index == 4)
        {
            LoadPuzzle2();
        }
        else if(index == 5)
        {
            LoadPuzzle3();
        }
        else Debug.LogError("暂时没有这个关卡");
    }

    private async void LoadStudy1()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = study1;
        await LoadSceneTask();
        afterPuzzleLoadEvent.RaiseEvent(null,this);
    }
    private async void LoadStudy2()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = study2;
        await LoadSceneTask();
        afterPuzzleLoadEvent.RaiseEvent(null,this);
    }
    private async void LoadPuzzle1()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = puzzle1;
        await LoadSceneTask();
        afterPuzzleLoadEvent.RaiseEvent(null,this);
    }
    private async void LoadPuzzle2()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = puzzle2;
        await LoadSceneTask();
        afterPuzzleLoadEvent.RaiseEvent(null,this);
    }
    private async void LoadPuzzle3()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = puzzle3;
        await LoadSceneTask();
        afterPuzzleLoadEvent.RaiseEvent(null,this);
    }
    private async Awaitable LoadSceneTask()
    {
        //Addressable下异步加载 
        var s = currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        //Task
        await s.Task;

        //Status
        if (s.Status == AsyncOperationStatus.Succeeded)
        {
            fadePanel.FadeOut(0.2f);
            //Result
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }

    private async Awaitable UnloadSceneTask()
    {
        fadePanel.FadeIn(0.4f);
        await Awaitable.WaitForSecondsAsync(0.45f);
        
        await Awaitable.FromAsyncOperation(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
    }

}
