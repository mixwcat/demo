using UnityEngine;

public class Raycast : MonoBehaviour
{
    //过滤射线检测的层
    private int magnetLayer;
    public Player player;
    private Magnet targetMagnet;
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
            Vector2 checkGrid = player.gridPosition +direction+new Vector2(0.5f,0.5f);
            Debug.Log(checkGrid);
            Collider2D hit = Physics2D.OverlapPoint(checkGrid,magnetLayer);
            if (hit != null)
            {
                targetMagnet=hit.GetComponent<Magnet>();
                if(targetMagnet is NPoleMagnet)
                {
                    Debug.Log("N极磁铁");
                }
                else if(targetMagnet is SPoleMagnet)
                {
                    Debug.Log("S极磁铁");
                }
                else
                {
                    Debug.Log("无极磁铁");
                }
            }
        

    }

}
