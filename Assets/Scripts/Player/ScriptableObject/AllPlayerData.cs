using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "AllPlayerData", menuName = "Player/AllPlayerData")]
[System.Serializable]
public class AllPlayerData : ScriptableObject
{
    public List<PlayerData> playerDatas;
}

public class AllPlayerStepData :IEqualityComparer<List<PlayerStepData>>
{
    public List<PlayerStepData> PlayerStepDatas;

    public AllPlayerStepData()
    {
        PlayerStepDatas = new List<PlayerStepData>();
    }
    public bool Equals(List<PlayerStepData> x, List<PlayerStepData> y)
    {
        if (x==null||y==null||x.Count != y.Count) return false;
        return x.SequenceEqual(y);  
    }

    public int GetHashCode(List<PlayerStepData> obj)
    {
        return obj.GetHashCode();
    }
}