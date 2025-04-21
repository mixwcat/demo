using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : SingletonPatternMonoAutoBase_DontDestroyOnLoad<PlayerManager>
{
    public int playerCount = 0;

    private int currentPlayerIndex;

    public int CurrentPlayerIndex
    {
        get { return currentPlayerIndex; }
    }

    public AllPlayerStepData allPlayerStepData = new AllPlayerStepData();
    public AllPlayerData allPlayerData;
    public List<Player> Players;

    public Player CurrentPlayer
    {
        get
        {
            if (currentPlayerIndex >= 0 && currentPlayerIndex < playerCount)
                return Players[currentPlayerIndex];
            return null;
        }
    }

    private PlayerStepDataSO playerStepDataSO;

    [Header("事件广播")] public GameObjectEventSO arriveTargetPointEvent;

    public GameObjectEventSO posChangedEvent;

    public void AfterAfterPuzzleLoadedEvent()
    {
        currentPlayerIndex = 0;
        foreach (var player in Players)
        {
            player.GetComponent<PlayerMove>().enabled = false;
            player.transform.position = allPlayerData.playerDatas[currentPlayerIndex].position;
            currentPlayerIndex++;
            player.canFixedUpdate = true;
        }

        currentPlayerIndex = 0;
        Players[0].GetComponent<PlayerMove>().enabled = true;
        Players[0].ShowOrHideThisIcon(true);

        playerStepDataSO = new PlayerStepDataSO();
    }

    public void SwitchCharacter(int newPlayerIndex)
    {
        CurrentPlayer.GetComponent<PlayerMove>().enabled = false;
        CurrentPlayer.ShowOrHideThisIcon(false);
        CurrentPlayer.GetComponentInChildren<Animator>().SetInteger("Status", 0);



        currentPlayerIndex = newPlayerIndex;
        CurrentPlayer.GetComponent<PlayerMove>().enabled = true;
        CurrentPlayer.ShowOrHideThisIcon(true);
    }



    public PoleType GetPoleType(Vector2 Pos)
    {
        foreach (var t in allPlayerData.playerDatas)
        {
            if (Vector2.Distance(t.position, Pos) <= 0.4f)
                return t.magnetType;
        }

        return PoleType.None;
    }

    public void PosChanged()
    {
        if (CurrentPlayer != null)
            posChangedEvent.RaiseEvent(CurrentPlayer.gameObject, this);
    }

    public void CheckArriveTargetPoint()
    {
        if (Vector2.Distance(CurrentPlayer.vector2Pos, PuzzleManager.Instance.CurrentPuzzle.puzzleTargetPointSO
                .puzzleTargetPoints[currentPlayerIndex].position) < 0.3f)
        {
            arriveTargetPointEvent.RaiseEvent(CurrentPlayer.gameObject, this);
        }
    }

    public void UpdatePlayerStepData()
    {
        List<PlayerStepData> list = new List<PlayerStepData>();

        foreach (var t in allPlayerData.playerDatas)
        {
            PlayerStepData PSD = new PlayerStepData();
            PSD.playerIndex = t.playerIndex;
            PSD.magnetType = t.magnetType;
            PSD.position = t.position;
            list.Add(PSD);
        }

        if (playerStepDataSO.PlayerStepDataStack == null)
        {
            playerStepDataSO.PlayerStepDataStack = new Stack<List<PlayerStepData>>();

            playerStepDataSO.PlayerStepDataStack.Push(list);
        }
        else
        {
            if (allPlayerStepData.Equals(list, playerStepDataSO.PlayerStepDataStack.Peek()))
            {
                
            }
            else
            {
                playerStepDataSO.PlayerStepDataStack.Push(list);

            }
        }
    }

    public void DownloadPlayerStepData()
    {
        if (playerStepDataSO.PlayerStepDataStack != null)
        {
            if (playerStepDataSO.PlayerStepDataStack.Count >= 2)
            {
                playerStepDataSO.PlayerStepDataStack.Pop();
                List<PlayerStepData> list = playerStepDataSO.PlayerStepDataStack.Peek();

                int index = 0;
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player").OrderBy(obj => obj.name).ToArray())
                {
                    Player player = go.GetComponent<Player>();
                    PlayerData playerData = player.playerData;
                    playerData.playerIndex = list[index].playerIndex;
                    playerData.magnetType = list[index].magnetType;
                    playerData.position = list[index].position;

                    go.transform.position = playerData.position;
                    
                    go.GetComponent<PlayerMove>().OnNewTurnBegin();
                    
                    index++;
                }
                
                
                
                UpdatePlayerStepData();
            }
            else ;
        }
        else ;
    }
}

