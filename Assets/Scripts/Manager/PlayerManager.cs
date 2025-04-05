using UnityEngine;

public class PlayerManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<PlayerManager>
{
    public GameObject player;
    public Player playerScript;


    [Header("事件广播")]
    public ObjectEventSO TimeToMoveEvent;


    public void AfterPuzzle1Load()
    {
        player.SetActive(true);
    }
    public void ArriveTrapPoint()
    {
        playerScript.isMoving = false;
        TimeToMoveEvent.RaiseEvent(null, this);
    }
}
