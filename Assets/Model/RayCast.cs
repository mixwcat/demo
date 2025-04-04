using UnityEngine;

public class RayCast : MonoBehaviour
{
    bool hasMoved = false;
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 3f);
        if (hit.collider != null)
        {
            //获取Player脚本
            _Player player = hit.collider.gameObject.GetComponent<_Player>();
            player.isMoving = false;
            //磁铁排斥，进行位移
            if (player.compassDirection == 3 && !hasMoved)
            {
                Vector3 newPosition = player.transform.position;
                newPosition.x += 6f;
                player.transform.position = newPosition;
                hasMoved = true;
            }
        }

    }
}
