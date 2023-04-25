using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public int heuristicWeight = 2;
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
        /// Debug.Log("Objective : "+objective);
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

    //protected abstract IEnumerator Search(TileLogic start);
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
     IEnumerator Search(TileLogic start)
    {
        int interationCout = 0;
        tilesSearch =new List<TileLogic>();
        Board.Instance.ClearSearch();
        TileLogic objective = Board.GetTile(ObjectivePosition);
        TileLogic current;
        
        List<TileLogic> openSet = new List<TileLogic>();
        openSet.Add(start);
        start.CostFromOrigin = 0;

        while (openSet.Count>0)
        {
            openSet.Sort((x,y)=> x.Score.CompareTo(y.Score));
            current = openSet[0];
            Board.Instance.PaintTile(current.Position,Color.green);

            if (current== objective)
            {
                Debug.Log("Found the objective");
                break;
            }
            openSet.RemoveAt(0);
            tilesSearch.Add(current);
            for (int i = 0; i < Board.Directions.Length; i++)
            {
                TileLogic next = Board.GetTile(current.Position + Board.Directions[i]);
                yield return  new WaitForSeconds(0.001f);
                interationCout++;
                
                if (next == null || next.CostFromOrigin<=current.CostFromOrigin+next.MoveCost)
                {
                    continue;
                }

                next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
               
                if (current.CostFromOrigin+next.MoveCost<SearchLength)
                {
                    next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
                    next.Previous = current;
                    next.CostToObjective = Vector3Int.Distance(next.Position, objective.Position) *heuristicWeight;
                    next.Score = next.CostToObjective + next.CostFromOrigin;

                    if (!tilesSearch.Contains(next))
                    {
                        openSet.Add(next);
                    }
                    tilesSearch.Add(next);
                    Board.Instance.PaintTile(next.Position,Color.yellow);
                }
                
            }
        }

        isComplete = true;
    }
}
