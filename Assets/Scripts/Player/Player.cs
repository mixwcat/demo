using System;
using System.Collections;
using UnityEngine;

public class Player:MonoBehaviour
{
    public NPoleDirection nPoleDirection;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    private float targetAngle;

    IEnumerator  turnToNPoleDirection()
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
            yield return null; // 每帧暂停一次
        }
    }

       public void startTurnToNPoleDirection()
    {
        StartCoroutine(turnToNPoleDirection());
    }

}
