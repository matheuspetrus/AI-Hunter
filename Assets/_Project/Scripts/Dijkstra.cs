using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
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

     //IEnumerator Search(TileLogic start);
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

    
    
    List<Vector3Int> listPositions;
    IEnumerator Search(TileLogic start)
    {

        listPositions = new List<Vector3Int>();
        int interationCount = 0;

        Board.Instance.ClearSearch();
       tilesSearch = new List<TileLogic>();
       
       tilesSearch.Add(start);
       
       Queue<TileLogic> CheckNow = new Queue<TileLogic>();
       Queue<TileLogic> CheckNext = new Queue<TileLogic>();

       start.CostFromOrigin = 0;
       
       CheckNow.Enqueue(start);
       while (CheckNow.Count>0)
       {
           TileLogic current = CheckNow.Dequeue();
            Board.Instance.PaintTile(current.Position,Color.green);
            listPositions.Add(current.Position);
            
           for (int i = 0; i < Board.Directions.Length; i++)
           {
               TileLogic next = Board.GetTile(current.Position + Board.Directions[i]);
               yield return  new WaitForSeconds(0.001f);
               interationCount++;
               if (next == null || next.CostFromOrigin<=current.CostFromOrigin+next.MoveCost)
               {
                   continue;
               }

               if (current.CostFromOrigin+next.MoveCost<SearchLength)
               {
                   next.CostFromOrigin = current.CostFromOrigin + next.MoveCost;
                   next.Previous = current;

                   if (!tilesSearch.Contains(next))
                   {
                       CheckNext.Enqueue(next);
                   }
                   tilesSearch.Add(next);
                   Board.Instance.PaintTile(next.Position,Color.yellow);
               }
           }

           if (CheckNow.Count==0)
           {
               SwapReference(ref CheckNow, ref CheckNext);
           }
       }
       isComplete = true;

       ObjectivePosition = listPositions[Random.Range(0, listPositions.Count)];
       
      // Debug.Log("interationCount: "+interationCount);
     
    }

    private void SwapReference(ref Queue<TileLogic> checkNow, ref Queue<TileLogic> checkNext)
    {
        Queue<TileLogic> temp = checkNow;
        checkNow = checkNext;
        checkNext = temp;
    }


}
