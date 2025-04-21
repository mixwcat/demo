using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public AllPlayerData allPlayerData;
    
    public PuzzleStartPointSO puzzleStartPointSO;
    public PuzzleTargetPointSO puzzleTargetPointSO;
    private int playerCounts;

    public int PlayerCounts
    {
        get { return playerCounts; }
    }

    [Header("事件广播")] public ObjectEventSO AfterAfterPuzzleLoadedEvent;

    private void OnEnable()
    {
        PlayerManager.Instance.Players.Clear();
        allPlayerData.playerDatas.Clear();
        
        playerCounts = 0;


        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player").OrderBy(obj => obj.name).ToArray())
        {
            Player player = go.GetComponent<Player>();
            PlayerData playerData = player.playerData;
            playerData.playerIndex=puzzleStartPointSO.puzzleStartPoints[playerCounts].playerIndex;
            playerData.magnetType=puzzleStartPointSO.puzzleStartPoints[playerCounts].poleType;
            playerData.position=puzzleStartPointSO.puzzleStartPoints[playerCounts].position;
            

            
            allPlayerData.playerDatas.Add(playerData);
            
            
            PlayerManager.Instance.Players.Add(player);
            playerCounts++;
        }

        PlayerManager.Instance.playerCount = playerCounts;

        AfterAfterPuzzleLoadedEvent.RaiseEvent(null, this);
    }
}
