using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]

[System.Serializable]
public class PlayerData : ScriptableObject
{
    public int playerIndex;
    public PoleType magnetType;
    public Vector2 position;
    
    
}

public class PlayerStepData
{
    public int playerIndex;
    public PoleType magnetType;
    public Vector2 position;

    public override bool Equals(object obj)
    {
        return obj is PlayerStepData stepData
               && playerIndex == stepData.playerIndex 
               && magnetType == stepData.magnetType
               && Vector2.Distance(position, stepData.position) < 0.01f;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(playerIndex, magnetType, position);
    }
}