using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Player player;
    public Raycast raycast;
    private bool canMove=false;
    private bool isCooldown=false;
    private float moveX;
    private float moveY;    
    
    
    public float speed;
    private Action currentMoveAction;

    void FixedUpdate()
    {
        GetInput();

        if(canMove)
        {
            if(currentMoveAction != null)
            {
                player.isMoving = true;
                currentMoveAction.Invoke();
                currentMoveAction = null;
                canMove = false;
            }
        }
        //如果玩家在移动,且不是在转动中,并且速度小于一个很小的值,则停止移动,并且将玩家位置对齐到网格上,不然全是bug
        if(player.isMoving&&!player.isTurning&&player.rb.linearVelocity.magnitude < Mathf.Epsilon)
        {
            player.rb.linearVelocity = Vector2.zero;
            SnapToGrid();
            player.isMoving = false;
        }
    }


    public void OnNDirectionMagnetDetected()
    {
        Magnet checkedMagnet =raycast.CheckedMagnet as Magnet;
        if (checkedMagnet != null)
        {
            if (checkedMagnet.poleType == PoleType.N)
            {
               //排斥   N N排斥
                RepelByMagnet(player.directionToVector2(-1));
            }
            else if (checkedMagnet.poleType == PoleType.S)
            {
                //吸引  N S吸引
                AttractedByMagnet();
            }
        }
        //else Debug.Log("检测到的物体不是磁铁");

    }

    public void OnSDirectionMagnetDetected()
    {
        Magnet checkedMagnet = raycast.CheckedMagnet as Magnet;
        if (checkedMagnet != null)
        {
            if (checkedMagnet.poleType == PoleType.N)
            {
                //吸引  S N吸引
                AttractedByMagnet();
            }
            else if (checkedMagnet.poleType == PoleType.S)
            {
                //排斥  S S排斥
                RepelByMagnet(player.directionToVector2(1));
            }
        }
        //else Debug.Log("检测到的物体不是磁铁");

    }

    //吸引效果实现
    public void AttractedByMagnet()
    {
        player.rb.linearVelocity=Vector2.zero;
    }

    //排斥效果实现
    public void RepelByMagnet(Vector2 direction)
    {
        int mode;
        Vector2 targetGrid=player.WorldToGrid(player.transform.position)+direction;  
        if(player.isTurning)//强排斥力
        {
            mode = 2;
        }
        else //弱排斥力
        {
            mode = 1;
        }
        player.isCoroutine=true;
        StartCoroutine(GridJump(targetGrid,direction,mode,()=>{
            player.isCoroutine= false;
        }));
    }
    IEnumerator GridJump(Vector2 targetGrid,Vector2 direction,int mode,Action onComplete)
    {
        Vector2 trueTargetGrid;
        Vector2 mode2TargetGrid=targetGrid+direction;
        if(mode==2)trueTargetGrid=mode2TargetGrid;
        else trueTargetGrid=targetGrid;

        float moveSpeed = 10f;
        try
        {
            while (Vector2.Distance(player.transform.position, trueTargetGrid) > 0.05f)
            {
                //当前速度与排斥方向相同,速度清0；
                Vector2 currentVeclocity=player.rb.linearVelocity;
                if(Vector2.Dot(currentVeclocity.normalized, -direction) > 0.99f)
                {
                    player.rb.linearVelocity=Vector2.zero;
                    player.transform.position = targetGrid;
                    yield break;
                }

                //如果目标格子是空气墙，不施加排斥力
                if (Physics2D.OverlapPoint(targetGrid+new Vector2(0.5f,0.5f), LayerMask.GetMask("AirWall"))) yield break;

                //这里是应该飞跃但是有墙壁，只能移动一格的情况
                if (mode==2&&Physics2D.OverlapPoint(mode2TargetGrid + new Vector2(0.5f, 0.5f), LayerMask.GetMask("AirWall"))) 
                {
                     player.transform.position = targetGrid;
                     yield break;
                }
                //飞跃情况
                if(trueTargetGrid==mode2TargetGrid)
                {
                    player.isInvincible=true;
                    if(player.gridPosition==mode2TargetGrid)player.isInvincible=false;
                }
                //正常情况
                else player.isInvincible=false;
                player.transform.position = Vector2.MoveTowards(player.transform.position, trueTargetGrid, moveSpeed * Time.deltaTime);

                yield return null;
            }
        }
        finally
        {
            onComplete();
        }
    }

    //对齐到网格
    public void SnapToGrid()
    {
        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(player.transform.position.x), Mathf.RoundToInt(player.transform.position.y));
        player.transform.position = new Vector2(gridPosition.x, gridPosition.y);
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
    public void MoveUp()
    {
        player.rb.linearVelocity=Vector2.up * speed;
    }
    public void MoveDown()
    {
        player.rb.linearVelocity=Vector2.down * speed;
    }
    public void MoveLeft()
    {
        player.rb.linearVelocity=Vector2.left * speed;
    }
    public void MoveRight()
    {
        player.rb.linearVelocity=Vector2.right * speed;
    }

//TODO:转向意图显示
    public void TurnNPoleDirectionToUp()
    {
        player.nPoleDirection = NPoleDirection.Up;
        player.startTurnToNPoleDirection();
    }

    public void TurnNPoleDirectionToDown()
    {
        player.nPoleDirection = NPoleDirection.Down;
        player.startTurnToNPoleDirection();
    }
    public void TurnNPoleDirectionToLeft()
    {
        player.nPoleDirection = NPoleDirection.Left;
        player.startTurnToNPoleDirection();
    }
    public void TurnNPoleDirectionToRight()
    {
        player.nPoleDirection = NPoleDirection.Right;
        player.startTurnToNPoleDirection();
    }


    public void GetInput()
    {
        if(player.isCoroutine)return;
        //获取输入,并且只保存一种动作
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
                            else currentMoveAction=TurnNPoleDirectionToUp;
                        }
                       else currentMoveAction=TurnNPoleDirectionToRight;
                    }
                    else currentMoveAction=TurnNPoleDirectionToDown;
                }
                else currentMoveAction=TurnNPoleDirectionToLeft;
            }
            else if(moveY>0)currentMoveAction=MoveUp;
            else currentMoveAction=MoveDown;
        }
        else if(moveX>0)currentMoveAction=MoveRight;
        else currentMoveAction=MoveLeft;

                //空格表示确认
        if(!player.isMoving)
        if(Input.GetKey(KeyCode.Space))PlayerManager.Instance.TimeToMoveEvent.RaiseEvent(null,this);
    }
}
