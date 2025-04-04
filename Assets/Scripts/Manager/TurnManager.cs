using UnityEngine;

public class TurnManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<TurnManager>
{

    [Header("事件广播")]
    public ObjectEventSO PlayerTurnBeginEvent;
    public ObjectEventSO PlayerTurnEndEvent;

    //测试用
    void Awake()
    {
        PlayerTurnBeginEvent.RaiseEvent(null,this);
    }
    
}
