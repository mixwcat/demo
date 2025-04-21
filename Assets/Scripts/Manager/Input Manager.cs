using UnityEngine;

public class InputManager :SingletonPatternMonoAutoBase_DontDestroyOnLoad<InputManager>
{
    public bool gameIsRunning = false;
    public bool canInput = false;

    private float moveX;
    private float moveY;
    
    
    [Header("事件广播")]
    public ObjectEventSO InputUpEvent;
    public ObjectEventSO InputDownEvent;
    public ObjectEventSO InputLeftEvent;
    public ObjectEventSO InputRightEvent;
    void FixedUpdate()
    {
        if (!gameIsRunning) return;
        GetInput();
    }
    
    public void AfterPuzzleLoad()
    {
        gameIsRunning = true;
        canInput = true;
    }
    

    public void GetInput()
    {
        if (!canInput) return;
        //获取输入,并且只保存一种动作
        moveX = Input.GetAxis("Horizontal");
        if (moveX == 0)
        {
            moveY = Input.GetAxis("Vertical");
            if (moveY == 0)
            {

            }
            else
            {
                if (moveY > 0)
                {
                    InputUpEvent.RaiseEvent( null,this);
                }
                else
                {
                    InputDownEvent.RaiseEvent(null,this);
                }
            }
        }
        else
        {
            if (moveX > 0)
            {
                InputRightEvent.RaiseEvent( null,this);
            }
            else
            {
                InputLeftEvent.RaiseEvent(null,this);
            }
        }
    }
}
