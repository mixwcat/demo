using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Player player;
    private bool canMove=false;
    private bool isMoving=false;
    private bool isCooldown=false;
    private float moveX;
    private float moveY;    
    

    public float speed;
    private Action currentMoveAction;

    void FixedUpdate()
    {
        //只保存一种动作
        moveX = Input.GetAxis("Horizontal");
        if (moveX == 0)
        {
            moveY = Input.GetAxis("Vertical");
            if (moveY == 0)
            {
                if(!Input.GetKey(KeyCode.J))
                {
                    if(!Input.GetKey(KeyCode.K))
                    {
                        if(!Input.GetKey(KeyCode.L))
                        {
                            if(!Input.GetKey(KeyCode.I))
                            {

                            }
                            else currentMoveAction=turnNPoleDirectionToUp;
                        }
                       else currentMoveAction=turnNPoleDirectionToRight;
                    }
                    else currentMoveAction=turnNPoleDirectionToDown;
                }
                else currentMoveAction=turnNPoleDirectionToLeft;
            }
            else if(moveY>0)currentMoveAction=moveUp;
            else currentMoveAction=moveDown;
        }
        else if(moveX>0)currentMoveAction=moveRight;
        else currentMoveAction=moveLeft;


        if(!isMoving)
        if(Input.GetKey(KeyCode.Space))TurnManager.Instance.PlayerTurnEndEvent.RaiseEvent(null,this);


        if(canMove)
        {
            if(currentMoveAction != null)
            {
                isMoving = true;
                currentMoveAction.Invoke();
                currentMoveAction = null;
                canMove = false;
            }
        }

        if(isMoving&&player.rb.linearVelocity.magnitude < Mathf.Epsilon)
        {
            player.rb.linearVelocity = Vector2.zero;
            SnapToGrid();
            isMoving = false;
        }
    }

    public void SnapToGrid()
    {
        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        player.transform.position = new Vector3(gridPosition.x, gridPosition.y, player.transform.position.z);
    }

    public async void OnPlayerTurnEnd()
    {
        if(isCooldown)return;
        canMove = true;
        isCooldown = true;
        //等待200毫秒
        await Task.Delay(200);
        isCooldown = false;
    }

//TODO:移动预示
    public void moveUp()
    {
        player.rb.linearVelocity=Vector2.up * speed;
    }
    public void moveDown()
    {
        player.rb.linearVelocity=Vector2.down * speed;
    }
    public void moveLeft()
    {
        player.rb.linearVelocity=Vector2.left * speed;
    }
    public void moveRight()
    {
        player.rb.linearVelocity=Vector2.right * speed;
    }

//TODO:转向并添加过渡，转向预示
    public void turnNPoleDirectionToUp()
    {
        player.nPoleDirection = NPoleDirection.Up;
        player.startTurnToNPoleDirection();
    }

    public void turnNPoleDirectionToDown()
    {
        player.nPoleDirection = NPoleDirection.Down;
        player.startTurnToNPoleDirection();
    }

    public void turnNPoleDirectionToLeft()
    {
        player.nPoleDirection = NPoleDirection.Left;
        player.startTurnToNPoleDirection();
    }

    public void turnNPoleDirectionToRight()
    {
        player.nPoleDirection = NPoleDirection.Right;
        player.startTurnToNPoleDirection();
    }

}
