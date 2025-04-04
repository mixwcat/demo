using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Player player;
    private bool canMove=false;
    private bool isCooldown=false;
    private float moveX;
    private float moveY;    
    
    private Action currentMoveAction;
    private Vector2 moveDirection;

    void Update()
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

        
        
        if(Input.GetKey(KeyCode.Space))TurnManager.Instance.PlayerTurnEndEvent.RaiseEvent(null,this);

        if(canMove)
        {
            if(currentMoveAction != null)
            {
                currentMoveAction.Invoke();
                currentMoveAction = null;
                canMove = false;
            }
        }
    }

    public async void OnPlayerTurnEnd()
    {
        if(isCooldown)return;
        isCooldown = true;
        //等待200毫秒
        await Task.Delay(200);
        isCooldown = false;
        canMove = true;
    }

//TODO:移动添加过渡，移动逻辑，移动预示动画
    public void moveUp()
    {
        player.rb.MovePosition(player.rb.position + Vector2.up);
    }
    public void moveDown()
    {
        player.rb.MovePosition(player.rb.position + Vector2.down * 1);
    }
    public void moveLeft()
    {
        player.rb.MovePosition(player.rb.position + Vector2.left * 1);
    }
    public void moveRight()
    {
        player.rb.MovePosition(player.rb.position + Vector2.right * 1);
    }

//TODO:转向并添加过渡，转向预示动画
    public void turnNPoleDirectionToUp()
    {
        player.nPoleDirection = NPoleDirection.Up;
    }

    public void turnNPoleDirectionToDown()
    {
        player.nPoleDirection = NPoleDirection.Down;
    }

    public void turnNPoleDirectionToLeft()
    {
        player.nPoleDirection = NPoleDirection.Left;
    }

    public void turnNPoleDirectionToRight()
    {
        player.nPoleDirection = NPoleDirection.Right;
    }

}
