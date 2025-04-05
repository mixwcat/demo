using UnityEngine;

public class Raycast : SingletonPatternMonoAutoBase_DontDestroyOnLoad<Raycast>
{
    //过滤射线检测的层
    private int magnetLayer;
    public Player player;
    public Magnet targetMagnet;

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
        CheckSurroundingMagnets();
    }

   

    void CheckSurroundingMagnets()
    {
        
        Vector2[] checkDirections={player.directionToVector2(1),player.directionToVector2(-1)};
 
 //TODO:检测N极和S极,并执行后续逻辑
 //N极检测
        var direction=checkDirections[0];
            checkGrid = player.gridPosition +direction+new Vector2(0.5f,0.5f);
            hit = Physics2D.OverlapPoint(checkGrid,magnetLayer);
            if (hit != null)
            {
                targetMagnet=hit.GetComponent<Magnet>();
                nDirectionMagnetDetectedEvent.RaiseEvent(null,this);
            }
//S极检测
            direction=checkDirections[1];
            checkGrid = player.gridPosition +direction+new Vector2(0.5f,0.5f);
            hit = Physics2D.OverlapPoint(checkGrid,magnetLayer);
            if (hit != null)
            {
                targetMagnet=hit.GetComponent<Magnet>();
                sDirectionMagnetDetectedEvent.RaiseEvent(null,this);
            }
        

    }

}
