using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{  
    public static Board Instance;

    public Tilemap Tilemap;

    public Vector3Int Size;

    public Dictionary<Vector3Int, TileLogic> Tiles;

    // Start is called before the first frame update

    private void Awake()
    {
        
        Instance = this;
        Tiles = new Dictionary<Vector3Int, TileLogic>();
        CreateTileLogics();
        Debug.Log(Tiles.Count);
    }
    [ContextMenu("Paint")]
    public void PaintTile(Vector3Int position,Color color)
    {
        // tilemap.SetTile(position, redSquare);
        Tilemap.SetTileFlags(position, TileFlags.None);
        Tilemap.SetColor(position, color);
    }
    void CreateTileLogics()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for(int y = 0; y < Size.y; y++)
            {
                TileLogic tile  = new TileLogic();  
                tile.Position = new Vector3Int(x, y,0);
                Tiles.Add(tile.Position,tile);
            }
        }
    }
}
