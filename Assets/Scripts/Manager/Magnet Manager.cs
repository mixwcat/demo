using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MagnetManager :SingletonPatternMonoAutoBase_DontDestroyOnLoad<MagnetManager>
{
    private Tilemap tilemap;
    public MagnetStateDataSO magnetStateData;
    public PoleType currentMagnetType;


    public void AfterPuzzleLoad()
    {
        magnetStateData.magnetStateDatas.Clear();
        tilemap=GameObject.Find("Magnets").GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.Log("tilemap==null");
            return;
        }

        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) != null)
            {
                if (tilemap.GetTile(pos) is NMagnetTile)
                {
                    magnetStateData.magnetStateDatas.Add(new MagnetStateData((Vector2Int)pos,PoleType.N));
                } 
                else if (tilemap.GetTile(pos) is SMagnetTile)
                {
                    magnetStateData.magnetStateDatas.Add(new MagnetStateData((Vector2Int)pos,PoleType.S));
                }
                else
                {
                    magnetStateData.magnetStateDatas.Add(new MagnetStateData((Vector2Int)pos,PoleType.None));
                }
            }
            tilemap.RefreshAllTiles();
        }
    }
    public PoleType GetMagnetState(Vector2 position)
    {
        foreach (MagnetStateData magnetState in magnetStateData.magnetStateDatas)
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
