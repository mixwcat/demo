using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<PuzzleManager>
{
    public int currentPuzzleIndex;
    private Puzzle currentPuzzle;
    public Puzzle CurrentPuzzle{get{return currentPuzzle;}}

    private bool isWin=false;
    
    [Header("事件广播")]
    public ObjectEventSO GameWinEvent;
    public ObjectEventSO GameOverEvent;
    
    public void AfterPuzzleLoaded()
    {
        isWin = false;
        currentPuzzle=GameObject.FindGameObjectWithTag("Puzzle")?.GetComponent<Puzzle>();
    }


    public void OnArriveTrapPoint()
    {
        GameOverEvent.RaiseEvent(null,this);
    }
    
    public void CheckWin()
    {
        isWin = true;
        foreach (var targetPoint in currentPuzzle.puzzleTargetPointSO.puzzleTargetPoints)
        {

            if (Vector2.Distance( currentPuzzle.allPlayerData.playerDatas[targetPoint.playerIndex].position , CurrentPuzzle
                    .puzzleTargetPointSO.puzzleTargetPoints[targetPoint.playerIndex].position)<0.35f)
            {
                //这些是在终点的
                
            }
            else isWin = false;
        }
        if (isWin) GameWinEvent.RaiseEvent(null, this);
    }
    
}
