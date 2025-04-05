using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<SceneLoadManager>
{
    //AssetReference通用资源标志类
    private AssetReference currentScene;
    public AssetReference menu;
    public FadePanel fadePanel;
    public AssetReference puzzle1;
    [Header(header:"广播")]
    public ObjectEventSO afterPuzzle1LoadEvent;

    private void Awake()
    {
        loadMenu();
    }

    private async Awaitable LoadSceneTask()
    {
        //Addressable下异步加载 
        var s= currentScene.LoadSceneAsync(LoadSceneMode.Additive);
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
    
    public async void loadMenu()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = menu;;
        await LoadSceneTask();
    }

     public async void LoadScene1()
    {
        if(currentScene!=null) await UnloadSceneTask();
        currentScene = puzzle1;;
        afterPuzzle1LoadEvent.RaiseEvent(null,this);
        await LoadSceneTask();
    }
}
