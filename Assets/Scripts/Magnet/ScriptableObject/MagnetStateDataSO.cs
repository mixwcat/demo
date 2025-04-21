
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magnet/MagnetStateDataSO")]
public class MagnetStateDataSO : ScriptableObject
{
    public List<MagnetStateData> magnetStateDatas;
    
    public PoleType GetMagnetState(Vector2 position)
    {
        foreach (MagnetStateData magnetState in magnetStateDatas)
        {
            if (position == magnetState.position)
            {
                return magnetState.pole;
            }
        }
        Debug.Log("magnetStateData==null");
        return PoleType.None;
    }
}
[System.Serializable]
public struct MagnetStateData
{
    public Vector2 position;
    public PoleType pole;

    public MagnetStateData(Vector2 position, PoleType pole)
    {
        this.position = position;
        this.pole = pole;
    }
}