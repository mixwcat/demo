using UnityEngine;

public class TurnManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<TurnManager>
{

    [Header("事件广播")]
    public ObjectEventSO TimeToMoveEvent;

    
}
