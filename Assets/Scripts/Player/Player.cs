using System;
using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public bool canFixedUpdate=false;
    
    public GameObject thisIcon;
    public GameObject attractedEffect;
    public int PlayerIndex{get{return playerData.playerIndex;}}
    public PoleType MagnetType{get{return playerData.magnetType;}}
    
    public Vector2Int GridPosition
    {
        get
        {
            return WorldToGrid(transform.position);
        }
    }
    public Vector2 vector2Pos
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
        set
        {
            transform.position = value;
        }
    }
    public Rigidbody2D rb;
    public Animator animator;

    //网格坐标
    public GameObject N, S;
    
    private void Awake()
    {
        GetComponent<PlayerMove>().isMoving = false;
        animator.SetInteger("Dir",0);
        animator.SetInteger("Status",0);
    }
    private void FixedUpdate()
    {
        if(!canFixedUpdate)return;

        playerData.position = GridPosition+new Vector2(0.5f,0.5f);
        switch (PlayerManager.Instance.allPlayerData.playerDatas[PlayerIndex].magnetType)
        {
            case PoleType.N:
                N.SetActive(true);
                S.SetActive(false);
                break;
            case PoleType.S:
                S.SetActive(true);
                N.SetActive(false);
                break;
            case PoleType.None:
                N.SetActive(false);
                S.SetActive(false);
                break;
        }
    }

    public void ShowOrHideThisIcon(bool show)
    {
        thisIcon.SetActive(show) ;
    }
    
    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        return new Vector2Int(Mathf.FloorToInt(worldPosition.x),Mathf.FloorToInt(worldPosition.y));
    }

}
