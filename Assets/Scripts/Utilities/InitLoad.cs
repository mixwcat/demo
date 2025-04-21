using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitLoad : MonoBehaviour
{
    public AssetReference intro;

    private void Awake()
    {
        Addressables.LoadSceneAsync(intro);
    }
}
