using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinder : MonoBehaviour
{
    public Vector3Int InitialPosition;

    public Vector3Int ObjectivePosition;
    
    public int SearchLength;

    protected List<TileLogic> tilesSearch;


    public bool isComplete;

    private List<TileLogic> path;
    [ContextMenu("Print Path")]
    public void TriggerPrintPath()
    {
        TileLogic objective = Board.GetTile(ObjectivePosition);
        Debug.Log("Objective : "+objective);
        if (tilesSearch.Contains(objective))
        {
            path = BuildPath(objective);
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

    public List<TileLogic> GetPath()
    {
        return path;
    }
    [ContextMenu("TriggerSearch")]
    // Start is called before the first frame update
    public void TriggerSearch()
    {
        isComplete = false;
        StartCoroutine(Search(Board.GetTile(InitialPosition)));
    }

}
