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

        //空格表示确认
        if(!isMoving)
        if(Input.GetKey(KeyCode.Space))TurnManager.Instance.TimeToMoveEvent.RaiseEvent(null,this);


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

        //如果玩家在移动,且不是在转动中,并且速度小于一个很小的值,则停止移动,并且将玩家位置对齐到网格上,不然全是bug
        if(isMoving&&!player.isTurning&&player.rb.linearVelocity.magnitude < Mathf.Epsilon)
        {
            player.rb.linearVelocity = Vector2.zero;
            SnapToGrid();
            isMoving = false;
        }
    }


    public void OnNDirectionMagnetDetected()
    {
        Magnet checkedMagnet =Raycast.Instance.targetMagnet as Magnet;
        if (checkedMagnet != null)
        {
            if (checkedMagnet.poleType == PoleType.N)
            {
               //排斥
               Debug.Log("N N排斥");
            }
            else if (checkedMagnet.poleType == PoleType.S)
            {
                //吸引
                Debug.Log("N S吸引");
            }
        }
        else
        {
            Debug.Log("检测到的物体不是磁铁");
        }
    }

    public void OnSDirectionMagnetDetected()
    {
        Magnet checkedMagnet = Raycast.Instance.targetMagnet as Magnet;
        if (checkedMagnet != null)
        {
            if (checkedMagnet.poleType == PoleType.N)
            {
                //吸引
                Debug.Log("S N吸引");
            }
            else if (checkedMagnet.poleType == PoleType.S)
            {
                //排斥
                Debug.Log("S S排斥");
            }
        }
        else
        {
            Debug.Log("检测到的物体不是磁铁");
        }
    }

    public void SnapToGrid()
    {
        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        player.transform.position = new Vector3(gridPosition.x, gridPosition.y, player.transform.position.z);
    }

    public async void OnTimeToMove()
    {
        if(isCooldown||currentMoveAction==null)return;
        canMove = true;
        isCooldown = true;
        //等待200毫秒,防止短时间内多次触发
        await Task.Delay(200);
        isCooldown = false;
    }

//TODO:移动意图显示
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

//TODO:转向意图显示
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
