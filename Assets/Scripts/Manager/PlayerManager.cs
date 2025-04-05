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
        if(playerScript.isInvincible) Debug.Log("玩家无敌，无法触发陷阱");
        else
        {
            Debug.Log("到达了陷阱点，游戏结束");
        }
    }
}
