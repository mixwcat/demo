using UnityEngine;

public class AnimationManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<AnimationManager>
{

    public void SetDir(int i)
    {
        //  0，1，2，3为前，后，左,右
        //即下，上，左，右
        if (PlayerManager.Instance.CurrentPlayer != null)
        {
            PlayerManager.Instance.CurrentPlayer.animator?.SetInteger("Dir",i);
        }
        
    }
    
    public void SetStatus(int i)
    {
        //0，1，2为待机，奔跑，收集
        if (PlayerManager.Instance.CurrentPlayer != null)
        {
            PlayerManager.Instance.CurrentPlayer.animator?.SetInteger("Status",i);
        }
    }
}
