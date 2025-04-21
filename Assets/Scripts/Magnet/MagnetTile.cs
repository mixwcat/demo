using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Tiles/MagnetTile")]
public class MagnetTile:TileBase
{
    public Sprite NMagnetSprite;
    public Sprite SMagnetSprite;
    public Sprite NoneMagnetSprite;
    
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (tilemap.GetTile(position) is NMagnetTile)
        {
            tileData.sprite = NMagnetSprite;
        }
        else if (tilemap.GetTile(position) is SMagnetTile)
        {
            tileData.sprite = SMagnetSprite;
        }
        else
        {
            tileData.sprite = NoneMagnetSprite;
        }
        tileData.colliderType = Tile.ColliderType.Grid;
    }
    
}

