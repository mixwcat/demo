using UnityEngine;

public class RaycastBase : MonoBehaviour
{
    //过滤射线检测的层
    protected string checkLayer;
    
    //射线检测的网格
    protected Vector2 checkGrid;
    
    protected Collider2D hit;
    public Vector2 CheckGrid{get{return checkGrid;}}
    
    public Vector2[] checkDirections={Vector2.up,Vector2.down,Vector2.left,Vector2.right};
    
    [Header("事件广播")]
    public Vector2EventSO DetectedUpEvent;
    public Vector2EventSO DetectedDownEvent;
    public Vector2EventSO DetectedLeftEvent;
    public Vector2EventSO DetectedRightEvent;
    
    public void CheckSurrounding()
    {
        if(PlayerManager.Instance.CurrentPlayer==null)return;
        
        var direction = checkDirections[0];
        
        RaycastHit2D raycastHit;
        
        raycastHit =Physics2D.Raycast(PlayerManager.Instance.CurrentPlayer.vector2Pos, direction, 1f,LayerMask.GetMask("AirWall"));
        if (raycastHit.collider == null)
        {
            checkGrid = PlayerManager.Instance.CurrentPlayer.GridPosition + direction + new Vector2(0.5f, 0.5f);
            hit = Physics2D.OverlapPoint(checkGrid, LayerMask.GetMask(checkLayer));
            if (hit != null)
            {
                DetectedUpEvent.RaiseEvent(checkGrid - new Vector2(0.5f, 0.5f), this);
            }
        }


        direction = checkDirections[1];
        raycastHit =Physics2D.Raycast(PlayerManager.Instance.CurrentPlayer.vector2Pos, direction, 1f,LayerMask.GetMask("AirWall"));
        if (raycastHit.collider == null)
        {
            checkGrid = PlayerManager.Instance.CurrentPlayer.GridPosition + direction+new Vector2(0.5f,0.5f);
            hit = Physics2D.OverlapPoint(checkGrid, LayerMask.GetMask(checkLayer));
            if (hit != null)
            {
                DetectedDownEvent.RaiseEvent(checkGrid-new Vector2(0.5f,0.5f), this);
            }
        }

        
        direction = checkDirections[2];
        raycastHit =Physics2D.Raycast(PlayerManager.Instance.CurrentPlayer.vector2Pos, direction, 1f,LayerMask.GetMask("AirWall"));
        if (raycastHit.collider == null)
        {
            checkGrid = PlayerManager.Instance.CurrentPlayer.GridPosition + direction+new Vector2(0.5f,0.5f) ;
            hit = Physics2D.OverlapPoint(checkGrid, LayerMask.GetMask(checkLayer));
            if (hit != null)
            {
                DetectedLeftEvent.RaiseEvent(checkGrid-new Vector2(0.5f,0.5f), this);
            }
        }

        
        direction = checkDirections[3];
        raycastHit =Physics2D.Raycast(PlayerManager.Instance.CurrentPlayer.vector2Pos, direction, 1f,LayerMask.GetMask("AirWall"));
        if (raycastHit.collider == null)
        {
            checkGrid = PlayerManager.Instance.CurrentPlayer.GridPosition + direction+new Vector2(0.5f,0.5f); 
            hit = Physics2D.OverlapPoint(checkGrid, LayerMask.GetMask(checkLayer));
            if (hit != null)
            {
                DetectedRightEvent.RaiseEvent(checkGrid-new Vector2(0.5f,0.5f), this);
            }
        }
    }
  
}
