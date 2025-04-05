using UnityEngine;

public class EventPoint : MonoBehaviour
{
    //网格坐标
    public Vector2Int gridPosition;

    [Header("事件广播")]
    public ObjectEventSO ArrivePointEvent;

}
