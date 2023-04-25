using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    public Vector3Int InitialPosition;

    public Vector3Int ObjectivePosition;
    
    public int SearchLength;

    protected List<TileLogic> tilesSearch;

    [ContextMenu("Print Path")]
    void TriggerPrintPath()
    {
        TileLogic objective = Board.GetTile(ObjectivePosition);

        if (tilesSearch.Contains(objective))
        {
            List<TileLogic> path = BuildPath(objective);
            PrintPath(path);
        }
        else
        {
            Debug.Log("Objective not found");
        }

    }

    protected abstract IEnumerator Search(TileLogic start);
    private List<TileLogic> BuildPath(TileLogic lastTile)
    {
        List<TileLogic>path = new List<TileLogic>();
        TileLogic temp = lastTile;
        while (temp.Previous != null)
        {
            path.Add(temp);
            temp = temp.Previous;
        }
        path.Add(temp);
        path.Reverse();
        return path;
    }

    private void PrintPath(List<TileLogic> path)
    {
        foreach (TileLogic t in path)
        {
            Debug.Log(t.Position);
        }
    }

    [ContextMenu("TriggerSearch")]
    // Start is called before the first frame update
    void TriggerSearch()
    {
        StartCoroutine(Search(Board.GetTile(InitialPosition)));
    }

}
