using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public bool canScroll;
    private float scroll;
    private CinemachineCamera CC;
    private CinemachineConfiner2D CC2D;
    private PolygonCollider2D PC2D;
    private void Awake()
    {
        canScroll = false;
        CC=GetComponent<CinemachineCamera>();
        CC2D=GetComponent<CinemachineConfiner2D>();
    }
    private void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0) 
            if(CC.Lens.OrthographicSize-scroll*2f>2.5f&&CC.Lens.OrthographicSize-scroll*2f<7f) 
                GetComponent<CinemachineCamera>().Lens.OrthographicSize -= scroll*2f;
    }

    private void FixedUpdate()
    {
        if(PlayerManager.Instance.CurrentPlayer!=null)
            CC.Follow = PlayerManager.Instance.CurrentPlayer.gameObject.transform;
    }

    public void AfterLoadPuzzle()
    {
        GetComponent<CinemachineCamera>().Lens.OrthographicSize = 3.5f;
        canScroll = true;
        
        PC2D=GameObject.Find("Grid").GetComponent<PolygonCollider2D>();
        if (PC2D == null)
        {
        }
        else
        {
            CC2D.BoundingShape2D = PC2D;
        }
    }

    public void OnPuzzleEnd()
    {
        canScroll=false;
    }
}
