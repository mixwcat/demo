using System;
using System.Collections;
using UnityEngine;

public class Player:MonoBehaviour
{
    public Vector2Int gridPosition;//网格坐标
    public NPoleDirection nPoleDirection=NPoleDirection.Up;//N极方向
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    
    public bool isTurning=false;//是否正在转动
    public bool isMoving=false;//是否正在移动
    public bool isCoroutine=false;
    public bool isInvincible=false;//是否无敌,无敌状态下不受陷阱影响，当跳跃时无敌

    private float targetAngle;

    [Header("事件广播")]
    public ObjectEventSO PlayerGridPosChangedEvent;

    void FixedUpdate()
    {
        if(gridPosition!=WorldToGrid(transform.position))
        {
            gridPosition=WorldToGrid(transform.position);
            PlayerGridPosChangedEvent.RaiseEvent(null,this);
        }
        else gridPosition=WorldToGrid(transform.position);
    }

    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y));
    }

    public Vector2 directionToVector2(int isN)//根据N极方向返回向量，参数为1则返回N极方向，-1返回S极方向
    {
        switch (nPoleDirection)
        {
            case NPoleDirection.Up:
                return new Vector2(0, 1*isN);
            case NPoleDirection.Down:
                return new Vector2(0, -1*isN);
            case NPoleDirection.Left:
                return new Vector2(-1*isN, 0);
            case NPoleDirection.Right:
                return new Vector2(1*isN, 0);
                default:
                    return Vector2Int.zero;
        }
    }


    IEnumerator  turnToNPoleDirection(Action onComplete)//转动方向
    {
        switch(nPoleDirection)
        {
            case NPoleDirection.Up:
            targetAngle = Mathf.Atan2(0, 1)*Mathf.Rad2Deg;
                break;
            case NPoleDirection.Down:
            targetAngle = Mathf.Atan2(0, -1)*Mathf.Rad2Deg;
                break;
            case NPoleDirection.Left:
            targetAngle = Mathf.Atan2(1, 0)*Mathf.Rad2Deg;
                break;
            case NPoleDirection.Right:
            targetAngle = Mathf.Atan2(-1, 0)*Mathf.Rad2Deg;
                break;
        }
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        while (spriteRenderer.transform.rotation != targetRotation)
        {
            spriteRenderer.transform.rotation = Quaternion.RotateTowards(
                spriteRenderer.transform.rotation, 
                targetRotation, 
                900 * Time.deltaTime
            );
            yield return null;
        }
        onComplete();
    }

       public void startTurnToNPoleDirection()
    {
        isTurning = true;
        isCoroutine = true;
        StartCoroutine(turnToNPoleDirection(()=>{
            isTurning = false;
            isCoroutine = false;
        }));
    }

}
