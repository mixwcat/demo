using UnityEngine;

public class Raycast : MonoBehaviour
{
    //过滤射线检测的层
    private int magnetLayer;
    public Player player;
    private Magnet checkedMagnet;

    public Magnet CheckedMagnet
    {
        get { return checkedMagnet; }
    }
    //射线检测的网格
    private Vector2 checkGrid;
    private Collider2D hit;

    [Header("事件广播")]
    //玩家的N极方向上检测到磁铁
    public ObjectEventSO nDirectionMagnetDetectedEvent;
    //玩家的S极方向上检测到磁铁
    public ObjectEventSO sDirectionMagnetDetectedEvent;
    void Awake()
    {
        magnetLayer=LayerMask.GetMask("Magnet");
    }

    void FixedUpdate()
    {
        if(!player.isMoving) return;
        CheckSurroundingMagnets();
    }

   

    void CheckSurroundingMagnets()
    {
        
        Vector2[] checkDirections={player.directionToVector2(1),player.directionToVector2(-1)};
 
 //N极方向的检测
        var direction=checkDirections[0];
            checkGrid = player.WorldToGrid(player.transform.position) +direction+new Vector2(0.5f,0.5f);
            hit = Physics2D.OverlapPoint(checkGrid,magnetLayer);
            if (hit != null)
            {
                checkedMagnet=hit.GetComponent<Magnet>();
                nDirectionMagnetDetectedEvent.RaiseEvent(null,this);
            }
//S极方向的检测
            direction=checkDirections[1];
            checkGrid = player.WorldToGrid(player.transform.position) +direction+new Vector2(0.5f,0.5f);
            hit = Physics2D.OverlapPoint(checkGrid,magnetLayer);
            if (hit != null)
            {
                checkedMagnet=hit.GetComponent<Magnet>();
                sDirectionMagnetDetectedEvent.RaiseEvent(null,this);
            }
        

    }

}
