using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerMove : MonoBehaviour
{
    private Player player;
    private GameObject playerObj;
    private Animator animator;
    
    public bool isMoving;
    public float moveSpeed;

    private bool isAttracted=false;
    
    public bool nextFixedUpdateCanInput=false;

    public List<bool> isEnable;
    
    private bool canUp;
    private bool canDown;
    private bool canLeft;
    private bool canRight;
    
    public PlayerRaycast playerRaycast;
    public MagnetRaycast magnetRaycast;
    
    private Vector2 targetPosition=new Vector2(99999,99999);
    private Vector2 targetDistance;

    private bool shouldSkipRaycast=false;
    private Vector2 lastTargetPosition;
    
    private Vector2 targetDirection;
    
    private Action currentMoveAction;

    public Action CurrentMoveAction
    {
        get { return currentMoveAction; }
    }
    
    private void Awake()
    {
        isEnable =new List<bool>() { false, false };
        player=GetComponent<Player>();
        playerObj=gameObject;
        animator=GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isEnable[player.playerData.playerIndex]=true;
        Init();
    }
    private void OnDisable()
    {
        isEnable[player.playerData.playerIndex]=false;
    }

    private void FixedUpdate()
    {
        if(!isEnable[player.playerData.playerIndex])return;
        
        if (nextFixedUpdateCanInput)
        {
            OnNewTurnBegin();
        }
        if (currentMoveAction != null)
        {
             isMoving = true;
             InputManager.Instance.canInput = false;
             currentMoveAction();
        }
        
        if (currentMoveAction==MoveTo&&Vector2.Distance(player.vector2Pos, targetPosition) <= 0.2f)
        {
            if(Vector2.Distance(targetPosition, lastTargetPosition) <= 0.3f)
            {
                shouldSkipRaycast=true;
            }
            else shouldSkipRaycast=false;
            lastTargetPosition=targetPosition;
            nextFixedUpdateCanInput = true;
        }
    }

    public void OnNewTurnBegin()
    {
        Init();
        SnapToGrid();
        
        InputManager.Instance.canInput = true;
 
        nextFixedUpdateCanInput=false;
            
        PlayerManager.Instance.CheckArriveTargetPoint();
            
        PlayerManager.Instance.PosChanged();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        
        nextFixedUpdateCanInput = true;
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.linearVelocity = Vector2.zero;
            collision.gameObject.GetComponent<PlayerMove>().SnapToGrid();
            player.rb.linearVelocity = Vector2.zero;
            SnapToGrid();
            
            if (currentMoveAction == MoveUp)
            {
                OnHitPlayerUp(player.GridPosition + Vector2.up);
            }
            else if (currentMoveAction == MoveDown)
            {
                OnHitPlayerDown(player.GridPosition + Vector2.down);
            }
            else if (currentMoveAction == MoveLeft)
            {
                OnHitPlayerLeft(player.GridPosition + Vector2.left);
            }
            else if (currentMoveAction == MoveRight)
            {
                OnHitPlayerRight(player.GridPosition + Vector2.right);
            }
        }

        else if (collision.gameObject.CompareTag("Magnet"))
        {
            if (currentMoveAction == MoveUp)
            {
                OnHitMagnetUp(player.GridPosition + Vector2.up);
            }
            else if (currentMoveAction == MoveDown)
            { 
                OnHitMagnetDown(player.GridPosition+Vector2.down);
            }
            else if (currentMoveAction == MoveLeft)
            { 
                OnHitMagnetLeft(player.GridPosition+Vector2.left);
            }
            else if (currentMoveAction == MoveRight)
            { 
                OnHitMagnetRight(player.GridPosition+Vector2.right);
            }
        }
    }
    
    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x),Mathf.FloorToInt(worldPosition.y));
    }

    public void Init()
    {
        SnapToGrid();
        currentMoveAction=null;
        targetPosition=new Vector2(99999,99999);
        isMoving = false;
        AnimationManager.Instance.SetStatus(0);
        canUp = true;
        canDown = true;
        canLeft = true;
        canRight = true;
        isAttracted=false;

        magnetRaycast.CheckSurrounding();
        playerRaycast.CheckSurrounding();
    }
    
        public void OnHitPlayerUp(Vector2 tempTargetPos)
        {
            Vector2 targetPos=tempTargetPos+new Vector2(0.5f,0.5f);
            if (player.MagnetType ==PlayerManager.Instance.GetPoleType(targetPos))
            {
                nextFixedUpdateCanInput =false;
                targetPosition = targetPos-2*Vector2.up;
                ActionIsMoveTo();
            }
            else shouldSkipRaycast=false;
        }
        public void OnHitPlayerDown(Vector2 tempTargetPos)
        {
            Vector2 targetPos=tempTargetPos+new Vector2(0.5f,0.5f);
            if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
            {
                nextFixedUpdateCanInput =false;
                targetPosition = targetPos-2*Vector2.down;
                ActionIsMoveTo();
            }
            else shouldSkipRaycast=false;
        }
        public void OnHitPlayerLeft(Vector2 tempTargetPos)
        {
            Vector2 targetPos=tempTargetPos+new Vector2(0.5f,0.5f);
            if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
            {
                nextFixedUpdateCanInput =false;
                targetPosition = targetPos-2*Vector2.left;
                ActionIsMoveTo();
            }
            else shouldSkipRaycast=false;
        }
        public void OnHitPlayerRight(Vector2 tempTargetPos)
        {
            Vector2 targetPos=tempTargetPos+new Vector2(0.5f,0.5f);
            if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
            {
                nextFixedUpdateCanInput =false;
                targetPosition = targetPos-2*Vector2.right;
                ActionIsMoveTo();
            }
            else shouldSkipRaycast=false;
        }
        
        
    
    public void OnHitMagnetUp(Vector2 targetPos)
    {
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            nextFixedUpdateCanInput =false;
            targetPosition = targetPos-2*Vector2.up+new Vector2(0.5f,0.5f);
            ActionIsMoveTo();
        }
        else shouldSkipRaycast=false;
    }
    public void OnHitMagnetDown(Vector2 targetPos)
    {
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            nextFixedUpdateCanInput =false;
            targetPosition = targetPos-2*Vector2.down+new Vector2(0.5f,0.5f);
            ActionIsMoveTo();
        }
        else shouldSkipRaycast=false;
    }
    public void OnHitMagnetLeft(Vector2 targetPos)
    {
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            nextFixedUpdateCanInput =false;
            targetPosition = targetPos-2*Vector2.left+new Vector2(0.5f,0.5f);
            ActionIsMoveTo();
        }
        else shouldSkipRaycast=false;
    }
    public void OnHitMagnetRight(Vector2 targetPos)
    {
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            nextFixedUpdateCanInput =false;
            targetPosition = targetPos-2*Vector2.right+new Vector2(0.5f,0.5f);
            ActionIsMoveTo();
        }
        else shouldSkipRaycast=false;
    }
    
    
    public void OnPlayerDetectedUp(Vector2 tempTargetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        Vector2 targetPos = tempTargetPos+new Vector2(0.5f,0.5f);
        if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.up;
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if (player.MagnetType != PoleType.None && PlayerManager.Instance.GetPoleType(targetPos) != PoleType.None)
        {
            canDown = false;
            canUp = false;
            isAttracted = true;
        }
        else ;
    }
    public void OnPlayerDetectedDown(Vector2 tempTargetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        Vector2 targetPos = tempTargetPos+new Vector2(0.5f,0.5f);
        if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.down;
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&PlayerManager.Instance.GetPoleType(targetPos)!=PoleType.None)
        {
            canUp = false;
            canDown = false;
            isAttracted = true;
        }
        else ;
    }
    public void OnPlayerDetectedLeft(Vector2 tempTargetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        Vector2 targetPos = tempTargetPos+new Vector2(0.5f,0.5f);
        if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.left;
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if (player.MagnetType != PoleType.None && PlayerManager.Instance.GetPoleType(targetPos) != PoleType.None)
        {
            canRight = false;
            canLeft = false;
            isAttracted = true;
        }
        else ;
    }
    public void OnPlayerDetectedRight(Vector2 tempTargetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        Vector2 targetPos = tempTargetPos+new Vector2(0.5f,0.5f);
        if (player.MagnetType == PlayerManager.Instance.GetPoleType(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.right;
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&PlayerManager.Instance.GetPoleType(targetPos)!=PoleType.None)
        {
            canLeft = false;
            canRight = false;
            isAttracted = true;
        }
        else ;
    }
    
    
    
     public void OnMagnetDetectedUp(Vector2 targetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.up+new Vector2(0.5f,0.5f);
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&MagnetManager.Instance.GetMagnetState(targetPos)!=PoleType.None)
        {
            canDown = false;
            canUp = false;
            isAttracted = true;
        }
        else
        {
            Debug.Log("有东西无磁性");
        }
    }
    public void OnMagnetDetectedDown(Vector2 targetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.down+new Vector2(0.5f,0.5f);
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&MagnetManager.Instance.GetMagnetState(targetPos)!=PoleType.None)
        {
            canUp = false;
            canDown = false;
            isAttracted = true;
        }
        else
        {
            Debug.Log("有东西无磁性");
        }
    }
    public void OnMagnetDetectedLeft(Vector2 targetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.left+new Vector2(0.5f,0.5f);
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&MagnetManager.Instance.GetMagnetState(targetPos)!=PoleType.None)
        {
            canRight = false;
            canLeft = false;
            isAttracted = true;
        }
        else
        {
            Debug.Log("有东西无磁性");
        }
    }
    public void OnMagnetDetectedRight(Vector2 targetPos)
    {
        if(!isEnable[player.playerData.playerIndex])return;
        if (player.MagnetType == MagnetManager.Instance.GetMagnetState(targetPos))
        {
            if(shouldSkipRaycast)return;
            targetPosition = targetPos-2*Vector2.right+new Vector2(0.5f,0.5f);
            //MusicManager.Instance.PlaySound("magnetSound");
            ActionIsMoveTo();
        }
        else if(player.MagnetType!=PoleType.None&&MagnetManager.Instance.GetMagnetState(targetPos)!=PoleType.None)
        {
            canLeft = false;
            canRight = false;
            isAttracted = true;
        }
        else
        {
            Debug.Log("有东西无磁性");
        }
    }
    
    
    
    IEnumerator  AttracedEffectCoroutine()
    {
        player.attractedEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.attractedEffect.SetActive(false);
    }

    public void StartShowAttracedEffectCoroutine()
    {
        StartCoroutine(AttracedEffectCoroutine());
    }
    
    public void OnInputUp()
    {
        if (!canUp||!isEnable[player.playerData.playerIndex])
        {
            //动画
            //MusicManager.Instance.PlaySound("magnetSound");
            return;
        }
        AnimationManager.Instance.SetDir(1); 
        if (isAttracted)
        {
            if(isEnable[player.playerData.playerIndex]) StartShowAttracedEffectCoroutine();
            //PlayerManager.Instance.CurrentPlayer?.gameObject.GetComponent<PlayerMove>().StartShowAttracedEffectCoroutine();
            MusicManager.Instance.PlaySound("magnetSound");
            AnimationManager.Instance.SetStatus(1);
            MusicManager.Instance.PlaySound("walkSound");
            targetPosition = player.vector2Pos + Vector2.up;
            ActionIsMoveTo();
            return;
        }
        targetDirection =  Vector2.up;
        MusicManager.Instance.PlaySound("walkSound");
        currentMoveAction = MoveUp;
    }

    public void OnInputDown()
    {
        if (!canDown||!isEnable[player.playerData.playerIndex])
        {
            //MusicManager.Instance.PlaySound("magnetSound");
            return;
        }
        AnimationManager.Instance.SetDir(0);
        if (isAttracted)
        {
            if(isEnable[player.playerData.playerIndex]) StartShowAttracedEffectCoroutine();
            //PlayerManager.Instance.CurrentPlayer?.gameObject.GetComponent<PlayerMove>().StartShowAttracedEffectCoroutine();
            MusicManager.Instance.PlaySound("magnetSound");
            AnimationManager.Instance.SetStatus(1);
            MusicManager.Instance.PlaySound("walkSound");
            targetPosition = player.vector2Pos + Vector2.down;
            ActionIsMoveTo();
            return;
        }
        targetDirection = Vector2.down;
        MusicManager.Instance.PlaySound("walkSound");
        currentMoveAction = MoveDown;
    }

    public void OnInputRight()
    {
        if (!canRight||!isEnable[player.playerData.playerIndex])
        {
            //MusicManager.Instance.PlaySound("magnetSound");
            return;
        }
        AnimationManager.Instance.SetDir(3);
        if (isAttracted)
        {
            if(isEnable[player.playerData.playerIndex]) StartShowAttracedEffectCoroutine();
            //PlayerManager.Instance.CurrentPlayer?.gameObject.GetComponent<PlayerMove>().StartShowAttracedEffectCoroutine();
            MusicManager.Instance.PlaySound("magnetSound");
            AnimationManager.Instance.SetStatus(1);
            MusicManager.Instance.PlaySound("walkSound");
            targetPosition = player.vector2Pos + Vector2.right;
            ActionIsMoveTo();
            return;
        }
        targetDirection = Vector2.right;
        MusicManager.Instance.PlaySound("walkSound");
        currentMoveAction = MoveRight;
    }

    public void OnInputLeft()
    {
        if (!canLeft||!isEnable[player.playerData.playerIndex])
        {
            //MusicManager.Instance.PlaySound("magnetSound");
            return;
        }
        AnimationManager.Instance.SetDir(2);
        if (isAttracted)
        {
            if(isEnable[player.playerData.playerIndex]) StartShowAttracedEffectCoroutine();
            //PlayerManager.Instance.CurrentPlayer?.gameObject.GetComponent<PlayerMove>().StartShowAttracedEffectCoroutine();
            MusicManager.Instance.PlaySound("magnetSound");
            AnimationManager.Instance.SetStatus(1);
            MusicManager.Instance.PlaySound("walkSound");
            targetPosition = player.vector2Pos + Vector2.left;
            ActionIsMoveTo();
            return;
        }
        targetDirection = Vector2.left;
        MusicManager.Instance.PlaySound("walkSound");
        currentMoveAction = MoveLeft;
    }


    //对齐到网格
    public void SnapToGrid()
    {
        player.transform.position = new Vector3(player.GridPosition.x + 0.5f, player.GridPosition.y + 0.5f, 0);
    }


    public void MoveTo()
    {
        player.rb.MovePosition(player.vector2Pos+targetDistance.normalized * (moveSpeed * Time.fixedDeltaTime*0.7f));
    }

    
    public void ActionIsMoveTo()
    {
        targetDistance = (targetPosition - player.vector2Pos);
        Vector2 direction = targetDistance.normalized;
        
        // targetDistance+=direction/2f;
        // //多半个单元格大小
        
        RaycastHit2D hit;
        do
        {
            hit =Physics2D.Raycast(player.vector2Pos, direction, targetDistance.magnitude,LayerMask.GetMask("AirWall"));
            //targetDistance-=direction/2f;
            targetDistance-=direction;
        }
        while (hit.collider != null && Vector2.Angle(targetDistance, direction) ==0);
        targetDistance+=direction;
        
        do
        {
            hit =Physics2D.Raycast(player.vector2Pos+direction/2, direction, targetDistance.magnitude-0.5f,LayerMask.GetMask("Player"));
            targetDistance-=direction;
        }
        while (hit.collider != null && Vector2.Angle(targetDistance, direction) ==0);
        targetDistance+=direction;

        
        targetPosition=player.vector2Pos+targetDistance;
        currentMoveAction=MoveTo;
    }
    public void MoveUp()
    {
        AnimationManager.Instance.SetStatus(1);
        Vector2 targetPos = player.vector2Pos+targetDirection * (moveSpeed * Time.fixedDeltaTime);
        player.rb.MovePosition(targetPos);
    }

    public void MoveDown()
    {
        AnimationManager.Instance.SetStatus(1);
        Vector2 targetPos  =player.vector2Pos+targetDirection * (moveSpeed * Time.fixedDeltaTime);
        player.rb.MovePosition(targetPos);
    }

    public void MoveLeft()
    {
        AnimationManager.Instance.SetStatus(1);
        Vector2 targetPos = player.vector2Pos+targetDirection * (moveSpeed * Time.fixedDeltaTime);
        player.rb.MovePosition(targetPos);
    }
    
    public void MoveRight()
    {
        AnimationManager.Instance.SetStatus(1);
        Vector2 targetPos = player.vector2Pos+targetDirection * (moveSpeed * Time.fixedDeltaTime);
        player.rb.MovePosition(targetPos);
    }
    
}

    

