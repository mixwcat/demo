using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("移动")]
    public float speed; 
    private Vector2 moveDirection = Vector2.zero; // 当前移动方向

    [Header("移动参数")]
    private Rigidbody2D rb; 
    private float xInput;
    private float yInput;
    public bool isMoving = false; 
    [Header("指南针")]
    public int compassDirection = 0; // 玩家内置磁体的方向，0表示上，1表示右，2表示下，3表示左
    #endregion

    #region Unity Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        Move();
        CompassDirection();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = false;
        }
    }
    #endregion

    #region Input Handling
    void HandleInput()
    {
        // 获取输入方向
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        // 如果有输入，更新移动方向
        if (xInput != 0 && !isMoving)
        {
            moveDirection = new Vector2(xInput, 0).normalized;
            isMoving = true;
        }
        else if (yInput != 0 && !isMoving)
        {
            moveDirection = new Vector2(0, yInput).normalized;
            isMoving = true;
        }
    }
    #endregion

    #region Movement
    void Move()
    {
        // 如果正在移动，则持续更新位置
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime); // 移动物体
        }
        else
        {
            rb.MovePosition(rb.position); // 停止移动时保持当前位置
        }
    }
    #endregion

    #region Compass
    void CompassDirection()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            compassDirection = (compassDirection + 1) % 4; // 顺时针旋转90度
        }
    }
    #endregion
}