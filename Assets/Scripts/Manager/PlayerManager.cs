using UnityEngine;

public class PlayerManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<PlayerManager>
{
    public GameObject player;

    [Header("事件广播")]
    public ObjectEventSO TimeToMoveEvent;

    public void AfterPuzzle1Load()
    {
        player.SetActive(true);
    }
}
