using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class ComicPanel : MonoBehaviour
{
 
    private VisualElement rootElement;
    private VisualElement comic;

    public AssetReference persistent;
    private float i;
    private bool canInput;
    private void OnEnable()
    {
        canInput = true;
        rootElement = GetComponent<UIDocument>().rootVisualElement;
        comic = rootElement.Q<VisualElement>("Comic");

        i = 0;
        Addressables.LoadAssetAsync<Texture2D>("comic1").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                comic.style.backgroundImage = null;
                comic.style.backgroundImage = handle.Result;
            }
        };
        
    }

    private void Update()
    {
        i+=Time.deltaTime;
        
        
        if (!canInput)
        {
            if (i > 2)
            {
                i = -9999;
                Addressables.LoadSceneAsync(persistent);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            canInput=false;
        }
        
        if (i > 1.5f&&i<3f)
        {
            i = 3.1f;
            Addressables.LoadAssetAsync<Texture2D>("comic2").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    comic.style.backgroundImage = null;
                    comic.style.backgroundImage = handle.Result;
                }
            };
        }
        else if (i>=5f&&i <7f)
        {
            i = 0;
            Addressables.LoadAssetAsync<Texture2D>("comic3").Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    
                    canInput=false;
                    comic.style.backgroundImage = null;
                    comic.style.backgroundImage = handle.Result;
                }
            };
        }   

    }
}
