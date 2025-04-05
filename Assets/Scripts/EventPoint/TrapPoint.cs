using UnityEngine;

public class TrapPoint : EventPoint
{
    void Awake()
    {
        gridPosition=WorldToGrid(transform.position);
    }
    public void OnPlayerGridPosChanged()
    {
        if(PlayerManager.Instance.playerScript.gridPosition == gridPosition)
        {
            Debug.Log("到达了陷阱");
            ArrivePointEvent.RaiseEvent(null,this);
        }
    }

    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
    }

}
