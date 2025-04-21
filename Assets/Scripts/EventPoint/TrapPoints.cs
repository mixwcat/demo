using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapPoints : EventPoints
{
    private Tilemap tilemap;
    private HashSet<Vector3Int> activeTrapPoints;
    private void OnEnable()
    {
        if(tilemap == null)tilemap = GetComponent<Tilemap>();
        activeTrapPoints = new HashSet<Vector3Int>();
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) != null)
            {
                activeTrapPoints.Add(pos);
            }
        }
    }
    public void OnPlayerGridPosChanged()
    {
        if (tilemap == null)Debug.LogError("tilemap is null");
        else
        {
            foreach (Vector3Int pos in activeTrapPoints)
            {
                if (PlayerManager.Instance.CurrentPlayer.GridPosition ==(Vector2Int)pos)
                {
                    ArrivePointEvent.RaiseEvent(null, this);
                }
            }
        }
    }
}
