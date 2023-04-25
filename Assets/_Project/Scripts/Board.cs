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

    public static Vector3Int [] Directions = new Vector3Int[4]
    {
        Vector3Int.up, Vector3Int.right, Vector3Int.down, Vector3Int.left
    };
    
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        Tiles = new Dictionary<Vector3Int, TileLogic>();
        CreateTileLogics();
        Debug.Log(Tiles.Count);
    }

    public static TileLogic GetTile(Vector3Int position)
    {
        TileLogic tile;
        if (Instance.Tiles.TryGetValue(position, out tile))
        {
            return tile;
        }
        return null;
    }

    public void ClearSearch()
    {
        foreach (TileLogic t in Tiles.Values)
        {
            t.CostFromOrigin = int.MaxValue;
            t.CostToObjective = int.MaxValue;
            t.Score = int.MaxValue;
            t.Previous = null;
        }
    }
    
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
                SetTile(tile);
            }
        }
    }

    void SetTile(TileLogic tileLogic)
    {
        string tileType = Tilemap.GetTile(tileLogic.Position).name;
        switch (tileType)
        {
            case "BlockedTile":
                tileLogic.MoveCost = int.MaxValue;
                break;
            case "2":
                tileLogic.MoveCost = 2;
                break;
            case "3":
                tileLogic.MoveCost = 3;
                break;
            default:
                tileLogic.MoveCost = 1;
                break;
        }
    }
}
