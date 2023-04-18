using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    public Tile redSquare;

    public Tilemap tilemap;

    public Vector3Int position;

    [ContextMenu("Paint")]
    void Paint()
    {
       // tilemap.SetTile(position, redSquare);
        tilemap.SetTileFlags(position,TileFlags.None);
        tilemap.SetColor(position, Color.red);
    }
}
